using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;

namespace serverDrone
{
    // Класс сервера (лидирующего дрона)
    class LeaderDrone
    {
        public string leaderName = "Дрон лидер";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double targetLatitude { get; set; }
        public double targetLongitude { get; set; }
        public double Speed { get; set; }
        public TcpClient client { get; set; }

        public DroneList droneList = new DroneList();

        public string filePath = "drones.xml";

        //обновляем координаты лидера
        public void SetCoordinates(double latitude, double longitude,double speed,  double targetlatitude,double targetlongitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            targetLatitude = targetlatitude;
            targetLongitude = targetlongitude;
            SerialazeXML();
        }
        public void UpdateCoordinates()
        {
            double distance = CalculateDistance(Latitude, Longitude, targetLatitude, targetLongitude); // Расстояние до цели
            double timeToReachTarget = distance / Speed; // Время до достижения цели

            // Предполагая, что дрон движется по прямой линии к цели
            double newLatitude = Math.Round(Latitude + (targetLatitude - Latitude) / timeToReachTarget,4); // Новая широта
            double newLongitude = Math.Round(Longitude + (targetLongitude - Longitude) / timeToReachTarget,4); // Новая долгота 
            
            SetCoordinates(newLatitude, newLongitude,Speed, targetLatitude,targetLongitude);
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
        public void Process()
        {
            NetworkStream stream = null;
            droneList.Drones = new List<Drone>();
            try
            {
                // У входящих объектов TcpClient получает сетевой поток через метод client.GetStream() и затем его использует для отправки сообщения.
                stream = client.GetStream(); // получаем сетевой поток для чтения и записи
                byte[] data = new byte[256]; // буфер для получаемых данных
                while (true)
                {

                    // получаем сообщение в цикле  
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string message = builder.ToString();
                    Console.WriteLine(message);

                    bool flag = false;
                    string[]str=message.Split(' ');

                    if (droneList.Drones.Count > 0)
                    {
                        foreach (Drone drone in droneList.Drones)
                        {
                            if (drone.Name == str[0].Substring(0, str[0].Length - 1))
                            {
                                drone.PositionX = Convert.ToDouble(str[1].Substring(1, str[1].Length - 1));
                                drone.PositionY = Convert.ToDouble(str[2].Substring(0, str[2].Length - 1));
                                flag=true;
                                break;
                            }
                        }
                    }
                    if (flag == false)
                    {
                        droneList.Drones.Add(new Drone
                        {
                            Name = str[0].Substring(0, str[0].Length - 1),
                            PositionX = Convert.ToDouble(str[1].Substring(1, str[1].Length - 1)),
                            PositionY = Convert.ToDouble(str[2].Substring(0, str[2].Length - 1))
                        });
                    }
                    
                    // Отправка своих координат клиентам
                    string coordinates = "Координаты лидера: ("+Latitude + " " + Longitude+")\n";
                    foreach(Drone drone in droneList.Drones)
                    {
                        coordinates += drone.Name + ": (" + drone.PositionX +" "+ drone.PositionY +")\n";
                    }
                    byte[] msg = Encoding.Unicode.GetBytes(coordinates); // преобразуем сообщение в массив байтов
                    stream.Write(msg, 0, msg.Length);
                    // Таким образом, сервер сможет одновременно обрабатывать сразу несколько запросов.
                    UpdateCoordinates();
                    
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
        public void StartListening(TcpListener listener, int port, LeaderDrone drone)
        { 
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port); // прослушивает входящие подключения по определенному порту
                listener.Start(); // запуск сервера
                Console.WriteLine("Сервер запущен...");

                while (true)
                {
                    // к серверу обращается клиент, поэтому  методом AcceptTcpClient() создем объект класса TcpClient, который будет использоваться для взаимодействия с подключенным клиентом.
                    TcpClient client = listener.AcceptTcpClient(); // подключение нового клиента. Метод AcceptTcpClient() блокируют выполняющий поток, пока сервер не обслужит подключенного клиента. 
                    drone.client=client;
                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(drone.Process));
                    clientThread.Start();
                    // Таким образом, сервер сможет одновременно обрабатывать сразу несколько запросов.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop(); // завершение работы сервера
            }
        }


        public void SerialazeXML()
        {
            // Создаем объект дрона
            Drone drone = new Drone();
            drone.Name = leaderName;
            drone.PositionX = Latitude;
            drone.PositionY=Longitude;
            drone.Speed=Speed;

            droneList.Leader=drone;

            // Сериализуем данные о дроне в XML файл
            XmlSerializer serializer = new XmlSerializer(typeof(DroneList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, droneList);

            }
            
        }
        
        public void DeserilizeXML()
        {
            // Сериализуем данные о дроне в XML файл
            XmlSerializer serializer = new XmlSerializer(typeof(Drone));
            // Десериализуем данные о дроне из XML файла
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    Drone deserializedDrone = (Drone)serializer.Deserialize(fileStream);
                    Console.WriteLine($"Широта: {deserializedDrone.PositionX}, Долгота: {deserializedDrone.PositionY}, Скорость: {deserializedDrone.Speed}");
                }
        }
    }
    class Program
    {
        const int port = 8888;
        static TcpListener listener;
        static void Main(string[] args)
        {
            LeaderDrone leader = new LeaderDrone();
            leader.SetCoordinates(40.712, 74.0060, 60, 30.2672, 97.7431);// Установка начальных координат лидирующего дрона
            leader.StartListening(listener, port, leader);
            Console.ReadLine();
        }
    }
 
}
