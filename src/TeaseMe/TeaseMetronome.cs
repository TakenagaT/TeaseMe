using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TeaseMe
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TeaseMetronome
    {
        [XmlAttribute("bpm")]
        public string BeatsPerMinute { get; set; }

        public override string ToString()
        {
            return String.Format("{0} bpm", BeatsPerMinute);
        }


        public static int ConvertIntervalToBpm(int intervalInMilliSeconds)
        {
            if (intervalInMilliSeconds <= 0)
            {
                return 0;
            }
            return Convert.ToInt32(1000f * 60f / intervalInMilliSeconds);
        }
    }
}