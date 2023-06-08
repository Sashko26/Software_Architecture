using System;
using System.Collections.Generic;

namespace EventBusPattern
{
    // Подія (Event)
    public class Event
    {
        public string Name { get; set; }
        public object Data { get; set; }
    }

    // Підписник (Subscriber)
    public interface ISubscriber
    {
        void HandleEvent(Event e);
    }

    // Event Bus
    public class EventBus
    {
        private Dictionary<string, List<ISubscriber>> eventSubscribers = new Dictionary<string, List<ISubscriber>>();

        public void Subscribe(string eventName, ISubscriber subscriber)
        {
            if (!eventSubscribers.ContainsKey(eventName))
            {
                eventSubscribers[eventName] = new List<ISubscriber>();
            }

            eventSubscribers[eventName].Add(subscriber);
        }

        public void Unsubscribe(string eventName, ISubscriber subscriber)
        {
            if (eventSubscribers.ContainsKey(eventName))
            {
                eventSubscribers[eventName].Remove(subscriber);
            }
        }

        public void Publish(Event e)
        {
            if (eventSubscribers.ContainsKey(e.Name))
            {
                foreach (var subscriber in eventSubscribers[e.Name])
                {
                    subscriber.HandleEvent(e);
                }
            }
        }
    }

    // Підписник A (Subscriber A)
    public class SubscriberA : ISubscriber
    {
        public void HandleEvent(Event e)
        {
            Console.WriteLine($"Subscriber A handled event '{e.Name}' with data: {e.Data}");
        }
    }

    // Підписник B (Subscriber B)
    public class SubscriberB : ISubscriber
    {
        public void HandleEvent(Event e)
        {
            Console.WriteLine($"Subscriber B handled event '{e.Name}' with data: {e.Data}");
        }
    }

    // Підписник C (Subscriber C)
    public class SubscriberC : ISubscriber
    {
        public void HandleEvent(Event e)
        {
            Console.WriteLine($"Subscriber C handled event '{e.Name}' with data: {e.Data}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Створення EventBus
            var eventBus = new EventBus();

            // Створення підписників
            var subscriberA = new SubscriberA();
            var subscriberB = new SubscriberB();
            var subscriberC = new SubscriberC();

            // Підписка підписників на подію "message"
            eventBus.Subscribe("message", subscriberA);
            eventBus.Subscribe("message", subscriberB);
            eventBus.Subscribe("message", subscriberC);

            // Публікація події "message" з даними
            var eventData = "Hello, event bus!";
            var eventMessage = new Event { Name = "message", Data = eventData };
            eventBus.Publish(eventMessage);

            // Відписка підписника C від події "message"
            eventBus.Unsubscribe("message", subscriberC);

            // Публікація ще однієї події "message" з новими даними
            var newEventData = "Goodbye, event bus!";
            var newEventMessage = new Event { Name = "message", Data = newEventData };
            eventBus.Publish(newEventMessage);
        }
    }
}
