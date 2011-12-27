using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Xml.Serialization;

namespace TeaseMe
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeasePage
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Text")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Text { get; set; }

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

        [XmlAttribute("if-set")]
        public string IfSetCondition { get; set; }

        [XmlAttribute("if-not-set")]
        public string IfNotSetCondition { get; set; }


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
                        if (Tease.Flags.Contains(Delay.IfSetCondition))
                        {
                            return Delay;
                        }
                    }
                    else if (!String.IsNullOrEmpty(Delay.IfNotSetCondition))
                    {
                        if (!Tease.Flags.Contains(Delay.IfNotSetCondition))
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
                        if (Tease.Flags.Contains(button.IfSetCondition))
                        {
                            result.Add(button);
                        }
                    }
                    else if (!String.IsNullOrEmpty(button.IfNotSetCondition))
                    {
                        if (!Tease.Flags.Contains(button.IfNotSetCondition))
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