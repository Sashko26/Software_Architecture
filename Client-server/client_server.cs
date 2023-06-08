// Server.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			StartServer();
		}
		static void StartServer()
		{
			// Встановлення IP-адреси сервера та порту
			IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
			int port = 8080;

			// Створення серверного сокету та зв'язування з адресою
			TcpListener serverSocket = new TcpListener(ipAddress, port);
			serverSocket.Start();
			Console.WriteLine("Server started on " + ipAddress + ":" + port);

			while (true)
			{
				// Приймання вхідного підключення
				TcpClient clientSocket = serverSocket.AcceptTcpClient();
				Console.WriteLine("Client connected");

				// Отримання даних від клієнта
				byte[] buffer = new byte[1024];
				NetworkStream networkStream = clientSocket.GetStream();
				int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
				string dataFromClient = Encoding.ASCII.GetString(buffer, 0, bytesRead);
				Console.WriteLine("Received from client: " + dataFromClient);

				// Відправка відповіді клієнту
				string response = "Hello from server";
				byte[] responseBytes = Encoding.ASCII.GetBytes(response);
				networkStream.Write(responseBytes, 0, responseBytes.Length);
				Console.WriteLine("Sent to client: " + response);

				// Закриття з'єднання з клієнтом
				clientSocket.Close();
				Console.WriteLine("Client disconnected");
			}
		}
	}
}

// Client.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			StartClient();
		}

		static void StartClient()
		{
			// Встановлення IP-адреси сервера та порту
			IPAddress serverIp = IPAddress.Parse("127.0.0.1");
			int port = 8080;

			// Створення клієнтського сокету та підключення до сервера
			TcpClient clientSocket = new TcpClient();
			clientSocket.Connect(serverIp, port);
			Console.WriteLine("Connected to server");

			// Відправка даних на сервер
			string message = "Hello from client";
			byte[] messageBytes = Encoding.ASCII.GetBytes(message);
			NetworkStream networkStream = clientSocket.GetStream();
			networkStream.Write(messageBytes, 0, messageBytes.Length);
			Console.WriteLine("Sent to server: " + message);

			// Отримання відповіді від сервера
			byte[] buffer = new byte[1024];
			int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
			string responseFromServer = Encoding.ASCII.GetString(buffer, 0, bytesRead);
			Console.WriteLine("Received from server: " + responseFromServer);
			
			// Закриття з'єднання з сервером та закриття клієнтського сокету
			clientSocket.Close();
			Console.WriteLine("Disconnected from server");
		}
	}
}