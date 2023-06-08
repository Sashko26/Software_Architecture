using System;
using System.Collections.Generic;

namespace PeerToPeerPattern
{
    // Peer клас
    class Peer
    {
        private string name;
        private List<Peer> peers;

        public string Name { get { return name; } }

        public Peer(string name)
        {
            this.name = name;
            peers = new List<Peer>();
        }

        public void AddPeer(Peer peer)
        {
            if (!peers.Contains(peer))
            {
                peers.Add(peer);
                peer.AddPeer(this);
            }
        }

        public void SendMessage(string message)
        {
            Console.WriteLine("Peer {0} received message: {1}", name, message);

            // Передача повідомлення всім пірів, окрім поточного
            foreach (var peer in peers)
            {
                if (peer != this)
                {
                    peer.ReceiveMessage(name, message);
                }
            }
        }

        public void ReceiveMessage(string senderName, string message)
        {
            Console.WriteLine("Peer {0} received message from {1}: {2}", name, senderName, message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення пірів
            Peer peerA = new Peer("A");
            Peer peerB = new Peer("B");
            Peer peerC = new Peer("C");

            // Встановлення зв'язків між пірами
            peerA.AddPeer(peerB);
            peerB.AddPeer(peerC);

            // Відправка повідомлення від піра A
            peerA.SendMessage("Hello, everyone!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
