using NUnit.Framework;
using System;
using EventBusPattern;

namespace EventBusPattern.Tests
{
    [TestFixture]
    public class EventBusTests
    {
        private EventBus eventBus;
        private SubscriberA subscriberA;
        private SubscriberB subscriberB;
        private SubscriberC subscriberC;

        [SetUp]
        public void Setup()
        {
            eventBus = new EventBus();
            subscriberA = new SubscriberA();
            subscriberB = new SubscriberB();
            subscriberC = new SubscriberC();
        }

        [Test]
        public void Subscribe_SubscriberReceivesEvent()
        {
            // Arrange
            string eventName = "message";
            eventBus.Subscribe(eventName, subscriberA);

            var eventData = "Hello, event bus!";
            var eventMessage = new Event { Name = eventName, Data = eventData };

            // Act
            eventBus.Publish(eventMessage);

            // Assert
            Assert.AreEqual($"Subscriber A handled event '{eventName}' with data: {eventData}", subscriberA.GetLastHandledEventMessage());
        }

        [Test]
        public void Unsubscribe_SubscriberDoesNotReceiveEvent()
        {
            // Arrange
            string eventName = "message";
            eventBus.Subscribe(eventName, subscriberA);
            eventBus.Subscribe(eventName, subscriberB);
            eventBus.Subscribe(eventName, subscriberC);

            eventBus.Unsubscribe(eventName, subscriberB);

            var eventData = "Hello, event bus!";
            var eventMessage = new Event { Name = eventName, Data = eventData };

            // Act
            eventBus.Publish(eventMessage);

            // Assert
            Assert.AreEqual($"Subscriber A handled event '{eventName}' with data: {eventData}", subscriberA.GetLastHandledEventMessage());
            Assert.IsNull(subscriberB.GetLastHandledEventMessage());
            Assert.AreEqual($"Subscriber C handled event '{eventName}' with data: {eventData}", subscriberC.GetLastHandledEventMessage());
        }
    }

    public static class SubscriberExtensions
    {
        public static string GetLastHandledEventMessage(this ISubscriber subscriber)
        {
            return subscriber is SubscriberA subscriberA ? subscriberA.LastHandledEventMessage :
                subscriber is SubscriberB subscriberB ? subscriberB.LastHandledEventMessage :
                subscriber is SubscriberC subscriberC ? subscriberC.LastHandledEventMessage : null;
        }
    }
}
