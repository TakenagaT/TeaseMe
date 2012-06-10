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
        public List<TeaseMedia> ImageList { get; set; }

        [XmlElement("Audio")]
        public List<TeaseMedia> AudioList { get; set; }

        [XmlElement("Video")]
        public List<TeaseMedia> VideoList { get; set; }

        [XmlElement("Delay")]
        public List<TeaseDelay> DelayList { get; set; }

        [XmlElement("Metronome")]
        public List<TeaseMetronome> MetronomeList { get; set; }

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
            ImageList = new List<TeaseMedia>();
            AudioList = new List<TeaseMedia>();
            VideoList = new List<TeaseMedia>();
            MetronomeList = new List<TeaseMetronome>();
            DelayList = new List<TeaseDelay>();
        }


        [XmlIgnore]
        public TeaseMedia AvailableImage
        {
            get
            {
                if (ImageList.Count > 0)
                {
                    var result = new List<TeaseMedia>();
                    foreach (var media in ImageList)
                    {
                        if (!String.IsNullOrEmpty(media.IfSetCondition))
                        {
                            if (Tease.MatchesIfSetCondition(media.IfSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else if (!String.IsNullOrEmpty(media.IfNotSetCondition))
                        {
                            if (Tease.MatchesIfNotSetCondition(media.IfNotSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else
                        {
                            result.Add(media);
                        }
                    }
                    return result.Count > 0 ? result[0] : null;
                }
                return null;
            }
        }

        [XmlIgnore]
        public TeaseMedia AvailableAudio
        {
            get
            {
                if (AudioList.Count > 0)
                {
                    var result = new List<TeaseMedia>();
                    foreach (var media in AudioList)
                    {
                        if (!String.IsNullOrEmpty(media.IfSetCondition))
                        {
                            if (Tease.MatchesIfSetCondition(media.IfSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else if (!String.IsNullOrEmpty(media.IfNotSetCondition))
                        {
                            if (Tease.MatchesIfNotSetCondition(media.IfNotSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else
                        {
                            result.Add(media);
                        }
                    }
                    return result.Count > 0 ? result[0] : null;
                }
                return null;
            }
        }

        [XmlIgnore]
        public TeaseMedia AvailableVideo
        {
            get
            {
                if (VideoList.Count > 0)
                {
                    var result = new List<TeaseMedia>();
                    foreach (var media in VideoList)
                    {
                        if (!String.IsNullOrEmpty(media.IfSetCondition))
                        {
                            if (Tease.MatchesIfSetCondition(media.IfSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else if (!String.IsNullOrEmpty(media.IfNotSetCondition))
                        {
                            if (Tease.MatchesIfNotSetCondition(media.IfNotSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else
                        {
                            result.Add(media);
                        }
                    }
                    return result.Count > 0 ? result[0] : null;
                }
                return null;
            }
        }

        [XmlIgnore]
        public TeaseMetronome AvailableMetronome
        {
            get
            {
                if (MetronomeList.Count > 0)
                {
                    var result = new List<TeaseMetronome>();
                    foreach (var media in MetronomeList)
                    {
                        if (!String.IsNullOrEmpty(media.IfSetCondition))
                        {
                            if (Tease.MatchesIfSetCondition(media.IfSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else if (!String.IsNullOrEmpty(media.IfNotSetCondition))
                        {
                            if (Tease.MatchesIfNotSetCondition(media.IfNotSetCondition))
                            {
                                result.Add(media);
                            }
                        }
                        else
                        {
                            result.Add(media);
                        }
                    }
                    return result.Count > 0 ? result[0] : null;
                }
                return null;
            }
        }

        [XmlIgnore]
        public TeaseDelay AvailableDelay
        {
            get
            {
                if (DelayList.Count > 0)
                {
                    var result = new List<TeaseDelay>();
                    foreach (var delay in DelayList)
                    {
                        if (!String.IsNullOrEmpty(delay.IfSetCondition))
                        {
                            if (Tease.MatchesIfSetCondition(delay.IfSetCondition))
                            {
                                result.Add(delay);
                            }
                        }
                        else if (!String.IsNullOrEmpty(delay.IfNotSetCondition))
                        {
                            if (Tease.MatchesIfNotSetCondition(delay.IfNotSetCondition))
                            {
                                result.Add(delay);
                            }
                        }
                        else
                        {
                            result.Add(delay);
                        }
                    }
                    return result.Count > 0 ? result[0] : null;
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