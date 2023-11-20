using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Client
{
    private TcpClient client;

    public void Connect()
    {
        client = new TcpClient();
        client.Connect(IPAddress.Parse("127.0.0.1"), 8888);
        Console.WriteLine("Connected to server.");

        // Получаем команду от сервера
        string command = ReceiveCommandFromServer();
        Console.WriteLine("Received command from server: " + command);

        // Запускаем поток для чтения данных от сервера
        Thread readDataThread = new Thread(ReadDataFromServer);
        readDataThread.Start();

        // Отправляем данные серверу
        while (true)
        {
            string data = "Some data to send";
            SendDataToServer(data);
            Console.WriteLine("Data sent to server: " + data);

            // Задержка между отправками данных
            Thread.Sleep(1000);
        }
    }

    private string ReceiveCommandFromServer()
    {
        byte[] buffer = new byte[1024];
        NetworkStream stream = client.GetStream();
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        return command;
    }
    private void ReadDataFromServer()
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received data from server: " + data);
        }
    }

    private void SendDataToServer(string data)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        NetworkStream stream = client.GetStream();
        stream.Write(buffer, 0, buffer.Length);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем несколько клиентов и запускаем их подключение к серверу
        Client client1 = new Client();
        Client client2 = new Client();
        Client client3 = new Client();

        Thread client1Thread = new Thread(client1.Connect);
        Thread client2Thread = new Thread(client2.Connect);
        Thread client3Thread = new Thread(client3.Connect);

        client1Thread.Start();
        client2Thread.Start();
        client3Thread.Start();
    }
}