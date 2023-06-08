using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BrokerPattern.Tests
{
    [TestClass]
    public class MessageBrokerTests
    {
        private MessageBroker broker;
        private TestSubscriber subscriberA;
        private TestSubscriber subscriberB;

        [TestInitialize]
        public void Initialize()
        {
            broker = new MessageBroker();
            subscriberA = new TestSubscriber();
            subscriberB = new TestSubscriber();
        }

        [TestMethod]
        public void Subscribe_SubscriberAddedToTopic()
        {
            broker.Subscribe("news", subscriberA);

            Assert.IsTrue(broker.Subscribers.ContainsKey("news"));
            Assert.AreEqual(1, broker.Subscribers["news"].Count);
            Assert.IsTrue(broker.Subscribers["news"].Contains(subscriberA));
        }

        [TestMethod]
        public void Unsubscribe_SubscriberRemovedFromTopic()
        {
            broker.Subscribe("news", subscriberA);
            broker.Subscribe("news", subscriberB);

            broker.Unsubscribe("news", subscriberA);

            Assert.IsTrue(broker.Subscribers.ContainsKey("news"));
            Assert.AreEqual(1, broker.Subscribers["news"].Count);
            Assert.IsFalse(broker.Subscribers["news"].Contains(subscriberA));
            Assert.IsTrue(broker.Subscribers["news"].Contains(subscriberB));
        }

        [TestMethod]
        public void Publish_MessageReceivedBySubscribers()
        {
            broker.Subscribe("news", subscriberA);
            broker.Subscribe("news", subscriberB);

            broker.Publish("news", "Breaking news");

            Assert.AreEqual(1, subscriberA.Messages.Count);
            Assert.AreEqual(1, subscriberB.Messages.Count);
            Assert.AreEqual("Breaking news", subscriberA.Messages[0]);
            Assert.AreEqual("Breaking news", subscriberB.Messages[0]);
        }

        private class TestSubscriber : ISubscriber
        {
            public List<string> Messages { get; private set; } = new List<string>();

            public void ReceiveMessage(string topic, string message)
            {
                Messages.Add(message);
            }
        }
    }
}
