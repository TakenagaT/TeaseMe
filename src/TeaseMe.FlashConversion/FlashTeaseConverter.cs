using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using TeaseMe.Common;

namespace TeaseMe.FlashConversion
{
    public class FlashTeaseConverter
    {
        public Tease Convert(string teaseId, string teaseTitle, string authorId, string authorName, string[] scriptLines)
        {
            var result = new Tease
            {
                Id = teaseId,
                Title = teaseTitle,
                Url = "http://www.milovana.com/webteases/showflash.php?id=" + teaseId,
                Author = new Author
                {
                    Id = authorId,
                    Name = authorName,
                    Url = "http://www.milovana.com/forum/memberlist.php?mode=viewprofile&u=" + authorId
                }
            };


            var mustCommands = new List<string>();

            // Do minor corrections first so that the parser can stay a bit simpler.
            foreach (var line in CorrectedLines(scriptLines))
            {
                // The must/mustnot commands will be parsed separately.
                if (line.StartsWith("must(") || line.StartsWith("mustnot("))
                {
                    mustCommands.Add(line);
                }
                else
                {
                    var page = CreatePage(line);

                    if (!String.IsNullOrEmpty(page.Errors) && page.Errors.Contains("mismatched input '<EOF>' expecting ')'"))
                    {
                        page = CreatePage(line + ")");
                    }
                    if (!String.IsNullOrEmpty(page.Errors) && page.Errors.Contains(" expecting QUOTED_STRING"))
                    {
                        // Some scripts don't have quotes when specifying sound, I could make the parser work correctly
                        // so I have to correct it before parsing.
                        var match = Regex.Match(line, @":sound\(id:(?<soundId>[^)]*)\)");
                        if (match.Success)
                        {
                            var soundId = match.Groups["soundId"].Value;
                            page = CreatePage(line.Replace(":sound(id:" + soundId + ")", ":sound(id:'" + soundId + "')"));
                        }
                    }

                    result.Pages.Add(page);
                }
            }

            foreach (var mustCommand in mustCommands)
            {
                var match = Regex.Match(mustCommand, @"(?<cmd>(must|mustnot))\(self:(?<self>(self:|:)?.*?)#,(?<actions>.*)\)");
                if (match.Success)
                {
                    var self = match.Groups["self"].Value;
                    var actions = match.Groups["actions"].Value;

                    var flags = new List<string>();
                    string[] actionArray = actions.Split(',');
                    foreach (var action in actionArray)
                    {
                        string id = action.AfterFirst(":").TrimEnd('#');
                        if (!String.IsNullOrEmpty(id))
                        {
                            flags.Add(id);
                        }
                    }

                    var page = result.Pages.Find(p => p.Id.Equals(self));
                    if (page != null)
                    {
                        if (match.Groups["cmd"].Value.Equals("must"))
                        {
                            page.IfSetCondition = String.Format("{0},{1}", page.IfSetCondition, String.Join(",", flags.ToArray())).Trim(',');
                        }
                        if (match.Groups["cmd"].Value.Equals("mustnot"))
                        {
                            page.IfNotSetCondition = String.Format("{0},{1}", page.IfNotSetCondition, String.Join(",", flags.ToArray())).Trim(',');
                        }
                    }
                }

            }

            return result;
        }



        private IEnumerable<string> CorrectedLines(string[] scriptLines)
        {
            var result = new List<string>();
            for (int i = 0; i < scriptLines.Length; i++)
            {
                string line = scriptLines[i];

                // Skip empty lines
                if (String.IsNullOrEmpty(line.Trim()) || "()".Equals(line.Trim()))
                {
                    continue;
                }

                // Join pages longer than a single line.
                while (!line.Trim().EndsWith(")"))
                {
                    i++;
                    line += scriptLines[i];
                }

                // Replace special quotes.
                line = line.Replace(@"\'", "&quot;");
                line = line.Replace('’', '"');

                // Some scripts have an empty media instruction.
                line = line.Replace(",media:'',", ",");


                result.Add(line);
            }
            return result;
        }

        private TeasePage CreatePage(string line)
        {
            var result = new TeasePage { Comments = line };

            var stream = new ANTLRStringStream(line);
            var lexer = new FlashTeaseScriptLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new FlashTeaseScriptParser(tokens);

            try
            {
                IAstRuleReturnScope<CommonTree> teaseReturn = parser.tease();
                if (teaseReturn.Tree != null)
                {
                    var pageNode = teaseReturn.Tree;
                    if (pageNode.Type != FlashTeaseScriptLexer.PAGE)
                    {
                        pageNode = pageNode.GetFirstChildWithType(FlashTeaseScriptLexer.PAGE) as CommonTree;
                    }

                    if (pageNode != null && pageNode.Type == FlashTeaseScriptLexer.PAGE)
                    {
                        var idNode = pageNode.GetFirstChildWithType(FlashTeaseScriptLexer.ID) as CommonTree;
                        if (idNode != null)
                        {
                            result.Id = GetPageId(idNode);
                        }

                        var propertiesNode = pageNode.GetFirstChildWithType(FlashTeaseScriptLexer.PROPERTIES) as CommonTree;
                        if (propertiesNode != null)
                        {
                            result.Text = GetText(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.TEXT) as CommonTree);

                            result.Image = GetImage(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.PIC) as CommonTree);
                            result.Audio = GetAudio(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.SOUND) as CommonTree);

                            result.ButtonList.AddRange(GetButtons(propertiesNode));

                            result.Delay = GetDelay(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.DELAY) as CommonTree);

                            result.SetFlags = GetFlags(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.SET) as CommonTree);
                            result.UnsetFlags = GetFlags(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.UNSET) as CommonTree);
                        }
                    }
                }

                if (parser.HasError)
                {
                    result.Errors = String.Format("ParserError ({0}): {1}. Please correct by hand.", parser.ErrorPosition, parser.ErrorMessage);
                }
            }
            catch (Exception)
            {
                result.Errors = String.Format("ParserError ({0}): {1}. Please correct by hand.", parser.ErrorPosition, parser.ErrorMessage);
            }

            if (String.IsNullOrEmpty(result.Id))
            {
                result.Id = Guid.NewGuid().ToString();
                result.Errors = String.Format("This page had no id, so one is generated. {0}", result.Errors);
            }

            return result;
        }

        private TeaseMedia GetImage(CommonTree picNode)
        {
            return (picNode != null) ? new TeaseMedia { Id = picNode.GetChild(0).Text.Trim('\'', '"') } : null;
        }

        private TeaseMedia GetAudio(CommonTree soundNode)
        {
            return (soundNode != null) ? new TeaseMedia { Id = soundNode.GetChild(0).Text.Trim('\'', '"') } : null;
        }

        private IEnumerable<TeaseButton> GetButtons(CommonTree propertiesNode)
        {
            var result = new List<TeaseButton>();

            var goNode = propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.GO) as CommonTree;
            if (goNode != null)
            {
                result.Add(new TeaseButton { Text = "Continue", Target = GetPageId(goNode.GetChild(0) as CommonTree) });
            }
            var ynNode = propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.YN) as CommonTree;
            if (ynNode != null)
            {
                result.Add(new TeaseButton { Text = "Yes", Target = GetPageId(ynNode.GetFirstChildWithType(FlashTeaseScriptLexer.YES).GetChild(0) as CommonTree) });
                result.Add(new TeaseButton { Text = "No", Target = GetPageId(ynNode.GetFirstChildWithType(FlashTeaseScriptLexer.NO).GetChild(0) as CommonTree) });
            }
            var buttonsNode = propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.BUTTONS) as CommonTree;
            if (buttonsNode != null)
            {
                foreach (CommonTree buttonNode in buttonsNode.Children.Where(x => x.Type == FlashTeaseScriptLexer.BUTTON))
                {
                    result.Add(new TeaseButton
                    {
                        Text = buttonNode.GetFirstChildWithType(FlashTeaseScriptLexer.CAP).GetChild(0).Text.Trim('\'', '"'),
                        Target = GetPageId(buttonNode.GetFirstChildWithType(FlashTeaseScriptLexer.TARGET).GetChild(0) as CommonTree)
                    });
                }
            }

            result.Reverse();
            return result;
        }

        private TeaseDelay GetDelay(CommonTree delayNode)
        {
            if (delayNode == null)
            {
                return null;
            }

            var result = new TeaseDelay();

            var timeNode = delayNode.GetFirstChildWithType(FlashTeaseScriptLexer.TIME) as CommonTree;
            if (timeNode != null)
            {
                var minNode = (CommonTree)timeNode.GetFirstChildWithType(FlashTeaseScriptLexer.MIN);
                int minSecs = System.Convert.ToInt32(minNode.GetChild(0).Text);
                if (minNode.ChildCount > 1)
                {
                    var minUnit = minNode.GetChild(1).Text;
                    switch (minUnit)
                    {
                        case "hrs": { minSecs = minSecs * 60 * 60; break; }
                        case "min": { minSecs = minSecs * 60; break; }
                        default: break;
                    }
                }

                var maxNode = timeNode.GetFirstChildWithType(FlashTeaseScriptLexer.MAX) as CommonTree;
                int maxSecs = -1;
                if (maxNode != null)
                {
                    maxSecs = System.Convert.ToInt32(maxNode.GetChild(0).Text);
                    if (maxNode.ChildCount > 1)
                    {
                        var maxUnit = maxNode.GetChild(1).Text;
                        switch (maxUnit)
                        {
                            case "hrs": { maxSecs = maxSecs * 60 * 60; break; }
                            case "min": { maxSecs = maxSecs * 60; break; }
                            default: break;
                        }
                    }
                }

                result.Seconds = (maxSecs > minSecs) ? String.Format("({0}..{1})", minSecs, maxSecs) : String.Format("{0}", minSecs);
            }

            result.Target = GetPageId(delayNode.GetFirstChildWithType(FlashTeaseScriptLexer.TARGET).GetChild(0) as CommonTree);

            var styleNode = delayNode.GetFirstChildWithType(FlashTeaseScriptLexer.STYLE) as CommonTree;
            if (styleNode != null && styleNode.ChildCount > 0)
            {
                switch (styleNode.GetChild(0).Text)
                {
                    case "hidden": result.Style = DelayStyle.Hidden; break;
                    case "secret": result.Style = DelayStyle.Secret; break;
                    default: result.Style = DelayStyle.Normal; break;
                }
            }

            return result;
        }



        private string GetFlags(CommonTree node)
        {
            if (node == null)
            {
                return null;
            }
            var flagList = new List<string>();

            flagList.AddRange(node.Children.Where(x => x.Type == FlashTeaseScriptLexer.ID).Select(x => GetPageId(x as CommonTree)));

            return (flagList.Count > 0) ? String.Join(",", flagList.ToArray()) : null;
        }

        private string GetPageId(CommonTree node)
        {
            if (node.Type == FlashTeaseScriptLexer.RANGE)
            {
                var fromNode = node.GetChild(0) as CommonTree;
                var toNode = node.GetChild(1) as CommonTree;
                if (fromNode != null && toNode != null)
                {
                    var fromText = String.Concat(fromNode.Children.Select(child => child.Text).ToArray());
                    var toText = String.Concat(toNode.Children.Select(child => child.Text).ToArray());

                    var prefix = node.GetFirstChildWithType(FlashTeaseScriptLexer.PREFIX) as CommonTree;

                    string prefixText = (prefix != null) ? prefix.GetChild(0).Text.Trim('\'', '"') : null;

                    return String.Format("{0}({1}..{2})", prefixText, fromText, toText);
                }
            }
            return String.Concat(node.Children.Select(child => child.Text).ToArray());
        }

        private string GetText(CommonTree textNode)
        {
            if (textNode == null)
            {
                return null;
            }
            string originalText = textNode.GetChild(0).Text.Trim('\'', '"');

            var result = new StringBuilder();

            if (!String.IsNullOrEmpty(originalText))
            {
                try
                {
                    var xmldoc = new XmlDocument();
                    xmldoc.LoadXml("<dummy>" + originalText + "</dummy>");
                    var pNodes = xmldoc.SelectNodes("//P");
                    if (pNodes != null && pNodes.Count > 0)
                    {
                        foreach (XmlElement element in pNodes)
                        {
                            result.AppendFormat("<p>{0}</p>", HttpUtility.HtmlEncode(element.InnerText));
                        }
                    }
                    else
                    {
                        result.Append(HttpUtility.HtmlEncode(xmldoc.InnerText));
                    }
                }
                catch (Exception)
                {
                    return HttpUtility.HtmlEncode(originalText);
                }
            }

            return (result.Length == 0) ? null : result.ToString();
        }
    }
}
