using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Security.Policy;

namespace serverDrone
{
    [Serializable]
    public class Drone
    {
        public string Name { get; set; }
        public double PositionX { get; set; }
        public double PositionY{ get; set; }
        public double Speed { get; set; }
    }

    // Класс для представления списка дронов и дрона-лидера
    [Serializable]
    public class DroneList
    {
        public List<Drone> Drones { get; set; }
        public Drone Leader { get; set; }
    }
}
