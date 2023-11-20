using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace serverDrone
{
    [Serializable]
   public class Drone
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Координата X")]
        public double PositionX { get; set; }
        [XmlElement("Координата Y")]
        public double PositionY { get; set; }
        [XmlElement("Скорость")]
        public double Speed { get; set; }
    }
}
