using System;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    public class TeaseSettings
    {
        [XmlElement]
        public bool AutoSetPageWhenSeen { get; set; }
    }
}
