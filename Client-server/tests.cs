using NUnit.Framework;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Server;
using Client;

namespace UnitTests
{
    public class Tests
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 8080;

        [Test]
        public void ServerClientCommunicationTest()
        {
            // Start the server in a separate thread
            System.Threading.Thread serverThread = new System.Threading.Thread(StartServer);
            serverThread.Start();

            // Wait for the server to start
            System.Threading.Thread.Sleep(1000);

            // Start the client
            StartClient();

            // Stop the server
            StopServer(serverThread);
        }

        private void StartServer()
        {
            Program.StartServer();
        }

        private void StartClient()
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(ServerIp), Port);

            string message = "Hello from client";
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Write(messageBytes, 0, messageBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string responseFromServer = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Assert.AreEqual("Hello from server", responseFromServer);

            clientSocket.Close();
        }

        private void StopServer(System.Threading.Thread serverThread)
        {
            serverThread.Abort();
        }
    }
}
