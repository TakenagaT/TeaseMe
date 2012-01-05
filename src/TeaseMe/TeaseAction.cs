using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseAction
    {
        [XmlAttribute("target")]
        public string Target { get; set; }

        [XmlAttribute("set")]
        public string SetFlags { get; set; }

        [XmlAttribute("unset")]
        public string UnsetFlags { get; set; }

        [XmlAttribute("if-set")]
        public string IfSetCondition { get; set; }

        [XmlAttribute("if-not-set")]
        public string IfNotSetCondition { get; set; }


        public override string ToString()
        {
            return String.Format("Target:{0}", Target);
        }
    }
}