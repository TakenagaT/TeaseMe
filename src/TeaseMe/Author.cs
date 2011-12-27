using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Author
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Url")]
        public string Url { get; set; }


        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}