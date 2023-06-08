using System;
using System.Collections.Generic;

namespace BrokerPattern
{
    // Брокер (Broker)
    class MessageBroker
    {
        private Dictionary<string, List<ISubscriber>> subscribers = new Dictionary<string, List<ISubscriber>>();

        public void Subscribe(string topic, ISubscriber subscriber)
        {
            if (!subscribers.ContainsKey(topic))
            {
                subscribers[topic] = new List<ISubscriber>();
            }

            subscribers[topic].Add(subscriber);
        }

        public void Unsubscribe(string topic, ISubscriber subscriber)
        {
            if (subscribers.ContainsKey(topic))
            {
                subscribers[topic].Remove(subscriber);
            }
        }

        public void Publish(string topic, string message)
        {
            if (subscribers.ContainsKey(topic))
            {
                foreach (var subscriber in subscribers[topic])
                {
                    subscriber.ReceiveMessage(topic, message);
                }
            }
        }
    }

    // Підписник (Subscriber)
    interface ISubscriber
    {
        void ReceiveMessage(string topic, string message);
    }

    // Підписник A (Subscriber A)
    class SubscriberA : ISubscriber
    {
        public void ReceiveMessage(string topic, string message)
        {
            Console.WriteLine($"Subscriber A received message on topic '{topic}': {message}");
        }
    }

    // Підписник B (Subscriber B)
    class SubscriberB : ISubscriber
    {
        public void ReceiveMessage(string topic, string message)
        {
            Console.WriteLine($"Subscriber B received message on topic '{topic}': {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення брокера
            MessageBroker broker = new MessageBroker();

            // Створення підписників
            ISubscriber subscriberA = new SubscriberA();
            ISubscriber subscriberB = new SubscriberB();

            // Підписка підписників на тему "news"
            broker.Subscribe("news", subscriberA);
            broker.Subscribe("news", subscriberB);

            // Публікація повідомлень на тему "news"
            broker.Publish("news", "Breaking news 1");
            broker.Publish("news", "Breaking news 2");

            // Відписка підписників від теми "news"
            broker.Unsubscribe("news", subscriberB);

            // Публікація ще одного повідомлення на тему "news"
            broker.Publish("news", "Breaking news 3");
        }
    }
}
