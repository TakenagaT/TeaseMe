using System;
using System.Collections.Generic;
using System.Xml;
using TeaseMe.Common;

namespace TeaseMe
{
    public class OldScriptConverter
    {
        readonly XmlDocument xmldoc;

        public OldScriptConverter(string fileContents)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.LoadXml(fileContents);  
            }
            catch (Exception)
            {
                // Just swallow the exception.
                xmldoc = null;
            }
            
        }

        public bool CanConvert()
        {
            return xmldoc != null && (xmldoc.SelectSingleNode("/pages[@version='5']") != null);
        }


        public Tease ConvertToTease()
        {
            if (!CanConvert())
            {
                return null;
            }

            var result = new Tease();

            var authorNode = xmldoc.SelectSingleNode("/pages/@author");
            if (authorNode != null)
            {
                result.Author = new Author { Name = authorNode.Value };
            }

            var titleNode = xmldoc.SelectSingleNode("/pages/@title");
            if (titleNode != null)
            {
                result.Title = titleNode.Value;
            }

            var urlNode = xmldoc.SelectSingleNode("/pages/@url");
            if (urlNode != null)
            {
                result.Url = urlNode.Value;
            }

            var mediafolderNode = xmldoc.SelectSingleNode("/pages/@mediafolder");
            if (mediafolderNode != null)
            {
                result.MediaDirectory = mediafolderNode.Value.TrimStart('/', '\\');
            }

            var pageNodes = xmldoc.SelectNodes("/pages/page");
            if (pageNodes != null)
            {
                foreach (XmlNode pageNode in pageNodes)
                {
                    var page = new TeasePage();

                    page.Id = pageNode.SelectSingleNode("pageid").InnerText;
                    
                    var textNode = pageNode.SelectSingleNode("instruction");
                    if (textNode != null)
                    {
                        page.Text = textNode.InnerText;
                    }

                    page.Image = CreateImage(pageNode);
                    page.Video = CreateVideo(pageNode);
                    page.Delay = CreateDelay(pageNode);
                    page.Metronome = CreateMetronome(pageNode);
                    page.ButtonList.AddRange(CreateButtons(pageNode));

                    result.Pages.Add(page);
                }
            }
            return result;
        }

        private TeaseMedia CreateImage(XmlNode pageNode)
        {
            var imageNode = pageNode.SelectSingleNode("media[@isVideo='0']/filename");
            if (imageNode != null)
            {
                return new TeaseMedia { Id = imageNode.InnerText };
            }
            return null;
        }

        private TeaseMedia CreateVideo(XmlNode pageNode)
        {
            var imageNode = pageNode.SelectSingleNode("media[@isVideo='1']/filename");
            if (imageNode != null)
            {
                return new TeaseMedia { Id = imageNode.InnerText };
            }
            return null;
        }

        private TeaseDelay CreateDelay(XmlNode pageNode)
        {
            var delayLengthNode = pageNode.SelectSingleNode("delaylength");
            if (delayLengthNode != null)
            {
                var delayLengthMin = delayLengthNode.Attributes["rdmMin"].Value;
                var delayLengthMax = delayLengthNode.Attributes["rdmMax"].Value;

                var delaySecondsNode = delayLengthNode.SelectSingleNode("delayseconds");
                if (delaySecondsNode != null)
                {
                    int seconds;
                    if (Int32.TryParse(delaySecondsNode.InnerText, out seconds) && seconds > 0)
                    {
                        var result = new TeaseDelay();

                        if (delayLengthMin == "0" && delayLengthMax == "0")
                        {
                            result.Seconds = seconds.ToString();
                        }
                        else
                        {
                            result.Seconds = String.Format("({0}..{1})", delayLengthMin, delayLengthMax);
                        }

                        var delayVisibleNode = pageNode.SelectSingleNode("delayvisible");
                        if (delayVisibleNode != null)
                        {
                            result.Style = delayVisibleNode.InnerText.Equals("0") ? DelayStyle.Secret : DelayStyle.Normal;
                        }

                        var targetNode = pageNode.SelectSingleNode("delaytarget");
                        if (targetNode != null)
                        {
                            var delayTargetMin = targetNode.Attributes["rdmMin"].Value;
                            var delayTargetMax = targetNode.Attributes["rdmMax"].Value;
                            var delayTargetId = targetNode["delaytargetid"].InnerText;

                            if (delayTargetMin == "0" && delayTargetMax == "0")
                            {
                                result.Target = delayTargetId;
                            }
                            else
                            {
                                result.Target = String.Format("{0}({1}..{2})", delayTargetId, delayTargetMin, delayTargetMax);
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        private TeaseMetronome CreateMetronome(XmlNode pageNode)
        {
            var metronomeNode = pageNode.SelectSingleNode("metronome");
            if (metronomeNode != null && metronomeNode["interval"] != null)
            {
                int bpm = TeaseMetronome.ConvertIntervalToBpm(Convert.ToInt32(metronomeNode["interval"].InnerText));

                if (bpm > 0)
                {
                    string bpmString = bpm.ToString();

                    if (metronomeNode.Attributes["rdmMin"] != null && metronomeNode.Attributes["rdmMax"] != null)
                    {
                        int rdmMin = TeaseMetronome.ConvertIntervalToBpm(Convert.ToInt32(metronomeNode.Attributes["rdmMin"].Value));
                        int rdmMax = TeaseMetronome.ConvertIntervalToBpm(Convert.ToInt32(metronomeNode.Attributes["rdmMax"].Value));

                        if (rdmMin > 0 || rdmMax > 0)
                        {
                            bpmString = (String.Format("({0}..{1})", Math.Min(rdmMin, rdmMax), Math.Max(rdmMin, rdmMax)));
                        }
                    }

                    return new TeaseMetronome { BeatsPerMinute = bpmString };
                }
            }
            return null;
        }

        private List<TeaseButton> CreateButtons(XmlNode pageNode)
        {
            var result = new List<TeaseButton>();

            var button1 = CreateButton(pageNode["button1"]);
            if (button1 != null)
            {
                result.Add(button1);
            }
            var button2 = CreateButton(pageNode["button2"]);
            if (button2 != null)
            {
                result.Add(button2);
            }
            var button3 = CreateButton(pageNode["button3"]);
            if (button3 != null)
            {
                result.Add(button3);
            }
            var button4 = CreateButton(pageNode["button4"]);
            if (button4 != null)
            {
                result.Add(button4);
            }
            var button5 = CreateButton(pageNode["button5"]);
            if (button5 != null)
            {
                result.Add(button5);
            }

            return result;
        }

        private TeaseButton CreateButton(XmlNode buttonNode)
        {
            if (buttonNode != null && buttonNode["buttoncaption"] != null && buttonNode["buttontarget"] != null)
            {
                var caption = buttonNode["buttoncaption"].InnerText;
                var target = buttonNode["buttontarget"].InnerText;

                if (String.IsNullOrEmpty(caption) && String.IsNullOrEmpty(target))
                {
                    return null;
                }

                var rdmMin = buttonNode.Attributes["rdmMin"].Value;
                var rdmMax = buttonNode.Attributes["rdmMax"].Value;
                
                if (rdmMin == "0" && rdmMax == "0")
                {
                    return new TeaseButton { Text = caption, Target = target };    
                }
                return new TeaseButton { Text = caption, Target = String.Format("{0}({1}..{2})", target, rdmMin, rdmMax)};
            }
            return null;
        }
    }
}
