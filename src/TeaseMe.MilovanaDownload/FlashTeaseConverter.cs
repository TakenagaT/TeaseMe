using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using TeaseMe.Common;

namespace TeaseMe.MilovanaDownload
{
    public class FlashTeaseConverter
    {
        public void AddPages(Tease tease, string script)
        {
            var mustCommands = new List<string>();

            // Do minor corrections first so that the parser can stay a bit simpler.
            foreach (var line in CorrectedLines(script))
            {
                var correctedLine = line;

                // The must/mustnot commands will be parsed separately.
                if (correctedLine.StartsWith("must(") || correctedLine.StartsWith("mustnot("))
                {
                    mustCommands.Add(correctedLine);
                }
                else
                {
                    var page = CreatePage(correctedLine);

                    if (!String.IsNullOrEmpty(page.Errors) && page.Errors.Contains("mismatched input '<EOF>' expecting ')'"))
                    {   
                        correctedLine = correctedLine + ")";
                        page = CreatePage(correctedLine);
                    }

                    if (!String.IsNullOrEmpty(page.Errors) && page.Errors.Contains(" expecting QUOTED_STRING"))
                    {
                        // Some scripts don't have quotes when specifying sound, I could make the parser work correctly
                        // so I have to correct it before parsing.
                        var match = Regex.Match(correctedLine, @":sound\(id:(?<soundId>[^)]*)\)");
                        if (match.Success)
                        {
                            var soundId = match.Groups["soundId"].Value;
                            correctedLine = correctedLine.Replace(":sound(id:" + soundId + ")", ":sound(id:'" + soundId + "')");
                            page = CreatePage(correctedLine);
                        }
                    }

                    if (!String.IsNullOrEmpty(page.Errors) && correctedLine.Contains("text:'<") && correctedLine.Contains(">',"))
                    {
                        // Correct the unescaped quotes in the instruction text.
                        correctedLine = 
                            correctedLine.BeforeFirst("text:'<") + "text:'<" 
                            + correctedLine.AfterFirst("text:'<").BeforeFirst(">',").Replace("'", "&quot;")
                            + ">'," + correctedLine.AfterFirst("text:'<").AfterFirst(">',");
                        page = CreatePage(correctedLine);
                    }

                    if (!String.IsNullOrEmpty(page.Errors) && page.Errors.Contains("mismatched input '#' expecting ')'") && correctedLine.Contains("buttons("))
                    {
                        // There might be HTML in the button captions
                        int i = 0;
                        //e1:buttons(target0:rating1#,cap0:"<FONT COLOR="#B30033" SIZE="14"><b>1</b></FONT>",target1:rating2#,cap1:"<FONT COLOR="#B30033" SIZE="14"><b>2</b></FONT>",target2:rating3#,cap2:"<FONT COLOR="#B30033" SIZE="14"><b>3</b></FONT>",target3:rating4#,cap3:"<FONT COLOR="#B30033" SIZE="14"><b>4</b></FONT>
                        var tmp = correctedLine.BeforeFirst("buttons(") + "buttons(";
                        var rest = correctedLine.AfterFirst("buttons(");
                        var cap = String.Format("cap{0}:\"", i);
                        while (rest.Contains(cap))
                        {
                            tmp += rest.BeforeFirst(cap) + cap;
                            rest = rest.AfterFirst(cap);
                            if (rest.Contains("\",target" + (i+1)))
                            {
                                var caption = rest.BeforeFirst("\",target" + (i+1));
                                tmp += StripHtml(caption) + "\",target" + (i+1) + rest.AfterFirst("\",target" + (i+1));
                            }
                            else if (rest.Contains("\")"))
                            {
                                var caption = rest.BeforeFirst("\")");
                                tmp += StripHtml(caption) + "\")" + rest.AfterFirst("\")");
                                break;
                            }
                            i++;   
                        }
                        correctedLine = tmp;
                        page = CreatePage(correctedLine);
                    }

                    tease.Pages.Add(page);
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

                    var page = tease.Pages.Find(p => p.Id.Equals(self));
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
        }

        private string StripHtml(string text)
        {
            try
                {
                    var xmldoc = new XmlDocument();
                    xmldoc.LoadXml("<dummy>" + text + "</dummy>");
                    return xmldoc.InnerText;
                }
                catch (Exception)
                {
                    return HttpUtility.HtmlEncode(text);
                }
        }

        private IEnumerable<string> CorrectedLines(string script)
        {
            string[] scriptLines = script.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

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

                            var image = GetImage(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.PIC) as CommonTree);
                            if (image != null)
                            {
                                result.ImageList.Add(image);
                            }
                            var audio = GetAudio(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.SOUND) as CommonTree);
                            if (audio != null)
                            {
                                result.AudioList.Add(audio);
                            }
                            var delay =  GetDelay(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.DELAY) as CommonTree);
                            if (delay != null)
                            {
                                result.DelayList.Add(delay);
                            }
                        
                            result.ButtonList.AddRange(GetButtons(propertiesNode));

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
                int minSecs = Convert.ToInt32(minNode.GetChild(0).Text);
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
                    maxSecs = Convert.ToInt32(maxNode.GetChild(0).Text);
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
                switch (styleNode.GetChild(0).Text.ToLowerInvariant())
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
