using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TeaseMe.FlashTeases
{
    public class FlashTeaseConverter
    {
        public Tease ConvertToTease(string teaseId, string teaseTitle, string authorId, string authorName, string[] scriptLines)
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
                result.Pages.Add(CreatePage(line));
            }
            
            return result;
        }

        private TeasePage CreatePage(string line)
        {
            var page = new TeasePage
            {
                Id = GetId(line),
                Text = GetText(line),
            };

            page.Image = CreateImage(line);
            page.Audio = CreateAudio(line);
            page.Delay = CreateDelay(line);
            page.ButtonList.AddRange(CreateButtons(line));


            return page;
        }


        private TeaseMedia CreateImage(string line)
        {
            var media = GetFunction(line, "media:pic");
            if (!String.IsNullOrEmpty(media))
            {
                var match = Regex.Match(line, @"media:pic\(id:('|"")(?<id>.*?)('|"")\)");
                if (match.Success)
                {
                    return new TeaseMedia { Id = match.Groups["id"].Value };
                }
            }
            return null;
        }

        private TeaseMedia CreateAudio(string line)
        {
            var media = GetFunction(line, "hidden:sound");
            if (!String.IsNullOrEmpty(media))
            {
                var match = Regex.Match(line, @"hidden:sound\(id:('|"")(?<id>.*?)('|"")\)");
                if (match.Success)
                {
                    return new TeaseMedia { Id = match.Groups["id"].Value };
                }
            }
            return null;
        }

        private TeaseDelay CreateDelay(string line)
        {
            var action = GetFunction(line, ":delay");
            if (!String.IsNullOrEmpty(action))
            {
                var match = Regex.Match(line, @":delay\(time:(?<timeAmount>\d+)(?<timeType>sec|min),target:(?<target>.*?)#,style:(?<style>.*?)\)");
                if (match.Success)
                {
                    int timeAmount = Convert.ToInt32(match.Groups["timeAmount"].Value);
                    var timeType = match.Groups["timeType"].Value;

                    int seconds = "sec".Equals(timeType) ? timeAmount : timeAmount*60;

                    return new TeaseDelay 
                    {
                        Seconds = seconds.ToString(),
                        Target =  match.Groups["target"].Value,
                        Style =  (DelayStyle)Enum.Parse(typeof(DelayStyle), match.Groups["style"].Value, true)
                    };
                }
            }
            return null;
        }
        
        private List<TeaseButton> CreateButtons(string line)
        {
            var result = new List<TeaseButton>();

            var actionGo = GetFunction(line, "action:go");
            if (!String.IsNullOrEmpty(actionGo))
            {
                // action:go(target:page3#)
                var target = actionGo.Replace("action:go(target:", "").Replace("#)", "");

                result.Add(new TeaseButton{ Target = target, Text = "Continue" });
            }

            var actionYN = GetFunction(line, "action:yn");
            if (!String.IsNullOrEmpty(actionYN))
            {
                // action:yn(yes:page14#,no:page13#)
                var match = Regex.Match(actionYN, @"action:yn\(yes:(?<yes>.*?)#,no:(?<no>.*?)#\)");
                if (match.Success)
                {
                    result.Add(new TeaseButton{ Target = match.Groups["yes"].Value, Text = "Yes" });
                    result.Add(new TeaseButton{ Target = match.Groups["no"].Value, Text = "No" });
                }
            }

            if (line.Contains(":buttons"))
            {
                var buttons = line.Substring(line.IndexOf(":buttons")).FirstMatchingBrackets().TrimStart('(').TrimEnd(')');
                // target0:10#,cap0:"Continue",target1:page13#,cap1:"Whimp Out"

                int nr = 0;
                while (buttons.Trim().StartsWith("target" + nr + ":"))
                {
                    buttons = buttons.Trim().Substring(("target" + nr + ":").Length).Trim();
                    // 10#,cap0:"Continue",target1:page13#,cap1:"Whimp Out"

                    var target = buttons.Substring(0, buttons.IndexOf('#'));
                    
                    buttons = buttons.Substring((target + "#").Length).Trim(' ', ',');
                    // cap0:"Continue",target1:page13#,cap1:"Whimp Out"

                    buttons = buttons.Substring(("cap" + nr + ":").Length).Trim();
                    // "Continue",target1:page13#,cap1:"Whimp Out"

                    var text = String.Empty;
                    if (buttons.Contains(",target"+ (nr+1) + ":"))
                    {
                        text = buttons.Substring(0, buttons.IndexOf(",target"+ (nr+1) + ":")).Trim(' ', '\'', '"');

                        buttons = buttons.Substring(buttons.IndexOf(",target"+ (nr+1) + ":")).Trim(',', ' ');
                        // target1:page13#,cap1:"Whimp Out"
                    }
                    else
                    {
                        text = buttons.Trim('\'', '"');
                    }

                    result.Add(new TeaseButton { Target = target, Text = text });
                    nr++;
                }
            }

            return result;
        }

        static string GetId(string text)
        {
            return text.BeforeFirst("#");
        }

        static string GetText(string text)
        {
            var result = new StringBuilder();

            var match = Regex.Match(text, @"text:\s*\'(?<text>.*?)\'");
            if (match.Success)
            {
                // HACK to strip the text formatting (for now).
                var xml = "<dummyroot>" + match.Groups["text"].Value + "</dummyroot>";
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

                return String.IsNullOrEmpty(result.ToString().Trim()) ? null : result.ToString().Trim();
            }
            return null;
        }

        static string GetFunction(string text, string functionName)
        {
            if (text.Contains(functionName))
            {
                return functionName + text.Substring(text.IndexOf(functionName)).FirstMatchingBrackets();
            }
            return null;
        }
    }
}