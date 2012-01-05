using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseButton : TeaseAction
    {
        [XmlText]
        public string Text { get; set; }

        public override string ToString()
        {
            return String.Format("{0} [target:{1}]", Text, Target);
        }
    }
}