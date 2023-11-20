using serverDrone;
using System;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Xml.Serialization;

namespace clientDrone
{
    // Класс клиента (последовательного дрона)
    class FollowerDrone
    {
        public string droneName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double leaderLatitude { get; set; }
        public double leaderLongitude { get; set; }
        public double Speed { get; set; }

        public void ReceiveCoordinates(double latitude, double longitude)
        {
            // Получение координат от лидирующего дрона
            leaderLatitude = latitude;
            leaderLongitude = longitude;
        }
        public void SetCoordinates(double latitude, double longitude, double speed, double targetlatitude, double targetlongitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            leaderLatitude = targetlatitude;
            leaderLongitude = targetlongitude;
            SerialazeXML();
        }
        public void UpdateCoordinates()
        {
            double distance = CalculateDistance(Latitude, Longitude, leaderLatitude, leaderLongitude); // Расстояние до цели
            double timeToReachTarget = distance / Speed; // Время до достижения цели
            double newLatitude = Math.Round(Latitude + (leaderLatitude - Latitude) / timeToReachTarget, 4); // Новая широта
            double newLongitude = Math.Round(Longitude + (leaderLongitude - Longitude) / timeToReachTarget, 4); // Новая долгота 

            SetCoordinates(newLatitude, newLongitude, Speed, leaderLatitude, leaderLongitude);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Формула Гаверсинуса для вычисления расстояния между двумя координатами
            const double R = 6371; // Радиус Земли в километрах
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public void GetLeaderCoordinaties(string message)
        {
            string[]str = message.Split('\n');
            string[] msg= str[0].Split(' ');
            leaderLatitude = Convert.ToDouble(msg[2].Substring(1, msg[2].Length-1));
            leaderLongitude = Convert.ToDouble(msg[3].Substring(0, msg[3].Length-1));
        }
        public void ConnectToLeader(string address, int port, string userName)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {

                    string message = Latitude + " " + Longitude;
                    message = String.Format("{0}: ({1})", userName, message);
                    byte[] data = Encoding.Unicode.GetBytes(message); // преобразуем сообщение в массив байтов
                    stream.Write(data, 0, data.Length); // отправка сообщения

                    //получаем ответ
                    data = new byte[256]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();

                    Console.WriteLine(message);

                    GetLeaderCoordinaties(message);                  
                    UpdateCoordinates();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        public void SerialazeXML()
        {
            // Создаем объект дрона
            Drone drone = new Drone();
            drone.Name = droneName;
            drone.PositionX = Latitude;
            drone.PositionY = Longitude;
            drone.Speed = Speed;

            // Сериализуем данные о дроне в XML файл
            XmlSerializer serializer = new XmlSerializer(typeof(Drone));
            string path = ("drone_"+droneName+".xml");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fileStream, drone);
            }

        }
        public void DeserilizeXML()
        {
            string path = ("drone_" + droneName + ".xml");
            // Сериализуем данные о дроне в XML файл
            XmlSerializer serializer = new XmlSerializer(typeof(Drone));
            // Десериализуем данные о дроне из XML файла
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                Drone deserializedDrone = (Drone)serializer.Deserialize(fileStream);
                Console.WriteLine($"Широта: {deserializedDrone.PositionX}, Долгота: {deserializedDrone.PositionY}, Скорость: {deserializedDrone.Speed}");
            }
        }

    }     
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
 
        static void Main(string[] args)
        {
            FollowerDrone follower = new FollowerDrone();
            Console.Write("Введите имя:"); //  вначале вводить свое имя
            string userName = Console.ReadLine();
            follower.droneName = userName;
            follower.SetCoordinates(34.0522, 118.2437, 60, 34.0522, 118.2437);
            follower.ConnectToLeader(address, port, userName);
            Console.ReadLine();
        }
    }

}
