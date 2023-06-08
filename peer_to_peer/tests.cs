using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PeerToPeerPattern.Tests
{
    [TestFixture]
    public class PeerTests
    {
        [Test]
        public void SendMessage_ReceivedByConnectedPeers()
        {
            // Arrange
            var peerA = new Peer("A");
            var peerB = new Peer("B");
            var peerC = new Peer("C");
            peerA.AddPeer(peerB);
            peerB.AddPeer(peerC);

            var receivedMessages = new List<string>();

            // Subscribe peerB and peerC to receive messages
            peerB.ReceiveMessage += (sender, args) => receivedMessages.Add(args);
            peerC.ReceiveMessage += (sender, args) => receivedMessages.Add(args);

            // Act
            peerA.SendMessage("Hello, everyone!");

            // Assert
            Assert.AreEqual(1, receivedMessages.Count);
            Assert.AreEqual("Hello, everyone!", receivedMessages[0]);
        }

        [Test]
        public void SendMessage_NotReceivedByDisconnectedPeer()
        {
            // Arrange
            var peerA = new Peer("A");
            var peerB = new Peer("B");
            var peerC = new Peer("C");
            peerA.AddPeer(peerB);

            var receivedMessages = new List<string>();

            // Subscribe peerC to receive messages
            peerC.ReceiveMessage += (sender, args) => receivedMessages.Add(args);

            // Act
            peerA.SendMessage("Hello, everyone!");

            // Assert
            Assert.AreEqual(0, receivedMessages.Count);
        }

        [Test]
        public void SendMessage_NotReceivedBySelf()
        {
            // Arrange
            var peerA = new Peer("A");
            var peerB = new Peer("B");
            peerA.AddPeer(peerB);

            var receivedMessages = new List<string>();

            // Subscribe peerA to receive messages
            peerA.ReceiveMessage += (sender, args) => receivedMessages.Add(args);

            // Act
            peerA.SendMessage("Hello, myself!");

            // Assert
            Assert.AreEqual(0, receivedMessages.Count);
        }
    }
}
