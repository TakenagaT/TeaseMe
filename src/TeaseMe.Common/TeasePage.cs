using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeasePage
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Text")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public HtmlText Text { get; set; }

        [XmlElement("Image")]
        public TeaseMedia Image { get; set; }

        [XmlElement("Audio")]
        public TeaseMedia Audio { get; set; }

        [XmlElement("Video")]
        public TeaseMedia Video { get; set; }

        [XmlElement("Delay")]
        public TeaseDelay Delay { get; set; }

        [XmlElement("Metronome")]
        public TeaseMetronome Metronome { get; set; }

        [XmlElement("Button")]
        public List<TeaseButton> ButtonList { get; set; }
        
        [XmlAttribute("set")]
        public string SetFlags { get; set; }

        [XmlAttribute("unset")]
        public string UnsetFlags { get; set; }

        [XmlAttribute("if-set")]
        public string IfSetCondition { get; set; }

        [XmlAttribute("if-not-set")]
        public string IfNotSetCondition { get; set; }

        [XmlElement("Comments")]
        public string Comments { get; set; }

        [XmlElement("Errors")]
        public string Errors { get; set; }

        public TeasePage()
        {
            ButtonList = new List<TeaseButton>();
        }


        [XmlIgnore]
        public TeaseDelay AvailableDelay
        {
            get
            {
                if (Delay != null)
                {
                    if (!String.IsNullOrEmpty(Delay.IfSetCondition))
                    {
                        if (Tease.MatchesIfSetCondition(Delay.IfSetCondition))
                        {
                            return Delay;
                        }
                    }
                    else if (!String.IsNullOrEmpty(Delay.IfNotSetCondition))
                    {
                        if (Tease.MatchesIfNotSetCondition(Delay.IfNotSetCondition))
                        {
                            return Delay;
                        }
                    }
                    else
                    {
                        return Delay;
                    }
                }
                return null;
            }
        }

        [XmlIgnore]
        public List<TeaseButton> AvailableButtons
        {
            get
            {
                var result = new List<TeaseButton>();
                foreach (var button in ButtonList)
                {
                    if (!String.IsNullOrEmpty(button.IfSetCondition))
                    {
                        if (Tease.MatchesIfSetCondition(button.IfSetCondition))
                        {
                            result.Add(button);
                        }
                    }
                    else if (!String.IsNullOrEmpty(button.IfNotSetCondition))
                    {
                        if (Tease.MatchesIfNotSetCondition(button.IfNotSetCondition))
                        {
                            result.Add(button);    
                        }
                    }
                    else
                    {
                        result.Add(button);
                    }
                }
                return result;
            }
        }
        

        [XmlIgnore]
        public Tease Tease { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Id);
        }
    }
}