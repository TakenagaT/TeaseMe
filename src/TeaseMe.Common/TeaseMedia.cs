using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseMedia : TeaseAction
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("start-at")]
        public string StartAt { get; set; }

        [XmlAttribute("stop-at")]
        public string StopAt { get; set; }

        [XmlAttribute("loops")]
        public string Loops { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Id);
        }
    }
}