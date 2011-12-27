using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseMedia
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Id);
        }
    }
}