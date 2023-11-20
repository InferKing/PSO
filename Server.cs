using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Server
{
    private TcpListener listener;
    private List<TcpClient> clients;

    public void Start()
    {
        listener = new TcpListener(IPAddress.Any, 8888);
        listener.Start();
        Console.WriteLine("Server started. Waiting for clients...");

        clients = new List<TcpClient>();

        // Запускаем поток, который будет принимать клиентов
        Thread acceptClientsThread = new Thread(AcceptClients);
        acceptClientsThread.Start();

        // Отправляем команды клиентам
        SendCommandToAllClients("You are the new leader!");

        // Читаем данные от клиентов
        ReadDataFromClients();
    }

    private void AcceptClients()
    {
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected.");

            // Добавляем клиента в список
            lock (clients)
            {
                clients.Add(client);
            }
        }
    }

    private void SendCommandToAllClients(string command)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(command);
        lock (clients)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Command sent to client: " + command);
            }
        }
    }

    private void ReadDataFromClients()
    {
        while (true)
        {
            List<TcpClient> connectedClients = new List<TcpClient>();

            lock (clients)
            {
                foreach (TcpClient client in clients)
                {
                    if (IsClientConnected(client))
                    {
                        connectedClients.Add(client);
                    }
                }
            }

            foreach (TcpClient client in connectedClients)
            {
                Thread readDataThread = new Thread(() => ReadDataFromClient(client));
                readDataThread.Start();
            }

            Thread.Sleep(1000);
        }
    }

    private bool IsClientConnected(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            return stream.DataAvailable;
        }
        catch
        {
            return false;
        }
    }

    private void ReadDataFromClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received data from client: " + data);

            // Сохраняем данные
            SaveData(data);
        }
    }

    private void SaveData(string data)
    {
        // Здесь можно добавить логику сохранения данных
        Console.WriteLine("Data saved: " + data);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Server server = new Server();
        server.Start();
    }
}