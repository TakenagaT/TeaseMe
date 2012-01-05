using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseDelay : TeaseAction
    {
        [XmlAttribute("seconds")]
        public string Seconds { get; set; }

        [XmlAttribute("style")]
        public DelayStyle Style { get; set; }

        public override string ToString()
        {
            return String.Format("{0} sec {1} [target:{2}]", Seconds, Style, Target);
        }
    }

    public enum DelayStyle
    {
        [XmlEnum("normal")]
        Normal,

        [XmlEnum("secret")]
        Secret,

        [XmlEnum("hidden")]
        Hidden
    }
}