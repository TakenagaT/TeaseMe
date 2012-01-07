using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using TeaseMe.Common;

namespace TeaseMe.FlashConversion
{
    public class FlashTeaseConverter
    {
        public Tease Convert(string teaseId, string teaseTitle, string authorId, string authorName, string[] scriptLines, bool useOnlineImages)
        {
            var result = new Tease
            {
                Id = teaseId,
                Title = teaseTitle,
                Url = "http://www.milovana.com/webteases/showflash.php?id=" + teaseId,
                MediaDirectory = teaseId,
                Author = new Author
                {
                    Id = authorId, 
                    Name = authorName, 
                    Url = "http://www.milovana.com/forum/memberlist.php?mode=viewprofile&u=" + authorId
                }
            };

            foreach (var line in scriptLines)
            {
                var page = CreatePage(line);
                if (useOnlineImages)
                {
                    if (page.Image != null)
                    {
                        page.Image.Id = String.Format("http://www.milovana.com/media/get.php?folder={0}/{1}&name={2}", authorId, teaseId, page.Image.Id);
                    }
                    if (page.Audio != null)
                    {
                        page.Audio.Id = String.Format("http://www.milovana.com/media/get.php?folder={0}/{1}&name={2}", authorId, teaseId, page.Audio.Id);
                    }
                }
                result.Pages.Add(page);
            }
            
            return result;
        }

        private TeasePage CreatePage(string line)
        {
            var result = new TeasePage();
            try
            {
                var stream = new ANTLRStringStream(line);
                var lexer = new FlashTeaseScriptLexer(stream);
                var tokens = new CommonTokenStream(lexer);
                var parser = new FlashTeaseScriptParser(tokens);

                IAstRuleReturnScope<CommonTree> teaseReturn = parser.tease();
                if (teaseReturn.Tree != null)
                {
                    var pageNode = teaseReturn.Tree;

                    var idNode = pageNode.GetFirstChildWithType(FlashTeaseScriptLexer.ID) as CommonTree;
                    if (idNode != null)
                    {
                        result.Id = GetPageId(idNode);
                    }

                    var propertiesNode = pageNode.GetFirstChildWithType(FlashTeaseScriptLexer.PROPERTIES) as CommonTree;
                    if (propertiesNode != null)
                    {
                        result.OriginalText = GetText(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.TEXT) as CommonTree);
                        result.Text = StripOriginalText(result.OriginalText);

                        result.Image = GetImage(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.PIC) as CommonTree);
                        result.Audio = GetAudio(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.SOUND) as CommonTree);

                        result.ButtonList.AddRange(GetButtons(propertiesNode));

                        result.Delay = GetDelay(propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.DELAY) as CommonTree);

                        // TODO set/unset
                    }
                }

                if (parser.HasError)
                {
                    result.Text = String.Format("ERROR while converting line {0}\n: ParserError: {1}.\n{2}", line, parser.ErrorMessage, result.Text);
                }
            }
            catch (Exception err)
            {
                result.Text = String.Format("ERROR while converting line {0}\n: [{1}] {2}.\n{3}", line, err.GetType(), err.Message, result.Text);
            }

            if (String.IsNullOrEmpty(result.Id))
            {
                result.Id = Guid.NewGuid().ToString();
            }

            return result;
        }

        private TeaseMedia GetImage(CommonTree picNode)
        {
            return (picNode != null) ? new TeaseMedia { Id =  picNode.GetChild(0).Text.Trim('\'', '"') } : null;
        }

        private TeaseMedia GetAudio(CommonTree soundNode)
        {
            return (soundNode != null) ? new TeaseMedia { Id =  soundNode.GetChild(0).Text.Trim('\'', '"') } : null;
        }

        private IEnumerable<TeaseButton> GetButtons(CommonTree propertiesNode)
        {
            var result = new List<TeaseButton>();

            var goNode = propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.GO) as CommonTree;
            if (goNode != null)
            {
                result.Add(new TeaseButton { Text = "Continue", Target = GetPageId(goNode.GetChild(0) as CommonTree)});
            }
            var ynNode = propertiesNode.GetFirstChildWithType(FlashTeaseScriptLexer.YN) as CommonTree;
            if (ynNode != null)
            {
                result.Add(new TeaseButton { Text = "Yes", Target = GetPageId(ynNode.GetFirstChildWithType(FlashTeaseScriptLexer.YES).GetChild(0) as CommonTree)});
                result.Add(new TeaseButton { Text = "No", Target = GetPageId(ynNode.GetFirstChildWithType(FlashTeaseScriptLexer.NO).GetChild(0) as CommonTree)});
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
                int secs = System.Convert.ToInt32(timeNode.GetChild(0).Text);
                if (timeNode.GetChild(1).Text == "min")
                {
                    secs = secs * 60;
                }
                result.Seconds = secs.ToString();
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

        // TODO Add Formatting.
        private string StripOriginalText(string originalText)
        {
            var result = new StringBuilder();

            if (!String.IsNullOrEmpty(originalText))
            {
                // HACK to strip the text formatting (for now).
                var xml = "<dummyroot>" + originalText + "</dummyroot>";
                using (var sr = new StringReader(xml))
                {
                    using (var reader = new XmlTextReader(sr))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                result.Append(reader.ReadContentAsString()).Append(" ");
                            }
                        }
                    }
                }
            }
            return String.IsNullOrEmpty(result.ToString().Trim()) ? null : result.ToString().Trim();
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
                    return String.Format("({0}..{1})", fromText, toText);
                }
            }
            return String.Concat(node.Children.Select(child => child.Text).ToArray());
        }

        private string GetText(CommonTree textNode)
        {
            return (textNode != null) ? textNode.GetChild(0).Text.Trim('\'', '"') : null;
        }
    }
}
