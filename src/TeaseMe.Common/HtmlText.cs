using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    public class HtmlText : IXmlSerializable
    {
        public string Value { get; set; }

        public static implicit operator HtmlText(string value)
        {
            return (value != null) ? new HtmlText { Value = value } : null;
        }

        public static implicit operator string(HtmlText htmlText)
        {
            return (htmlText != null) ? htmlText.Value : null;
        }

        public override string ToString()
        {
            return Value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Value = reader.ReadInnerXml();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteRaw(Value);
        }
    }
}