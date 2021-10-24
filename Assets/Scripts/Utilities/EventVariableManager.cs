using System.Collections.Generic;
using System.Threading;

namespace DungeonBrickStudios
{
    public class EventVariableManager
    {
        public static EventVariableManager instance { get; private set; }

        static EventVariableManager()
        {
            instance = new EventVariableManager();
        }

        private readonly int mainThreadID;
        private readonly Queue<EventVariablePropertiesBase> sideThreadEvents;
        private Queue<EventVariablePropertiesBase> currentPushingEventStack;
        private bool running;

        private EventVariableManager()
        {
            sideThreadEvents = new Queue<EventVariablePropertiesBase>();
            currentPushingEventStack = new Queue<EventVariablePropertiesBase>();
            mainThreadID = Thread.CurrentThread.ManagedThreadId;
            running = false;
        }

        public void AddEvent(EventVariablePropertiesBase eventVariableBase)
        {
            if (Thread.CurrentThread.ManagedThreadId == mainThreadID)
            {
                currentPushingEventStack.Enqueue(eventVariableBase);
                TriggerEvents();
            }
            else
            {
                sideThreadEvents.Enqueue(eventVariableBase);
            }
        }

        public void Update()
        {
            while (sideThreadEvents.Count > 0)
                AddEvent(sideThreadEvents.Dequeue());
        }

        private void TriggerEvents()
        {
            if (running)
                return;

            running = true;
            TriggerEvents(currentPushingEventStack);
            running = false;
        }

        private void TriggerEvents(Queue<EventVariablePropertiesBase> events)
        {
            currentPushingEventStack = new Queue<EventVariablePropertiesBase>();
            while (events.Count > 0)
            {
                EventVariablePropertiesBase eventVariableBase = events.Dequeue();
                eventVariableBase.Trigger();
                if (currentPushingEventStack.Count > 0)
                    TriggerEvents(currentPushingEventStack);
            }
        }
    }
}
