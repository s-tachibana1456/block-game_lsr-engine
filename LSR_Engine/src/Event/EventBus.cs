using System;
using System.Collections.Generic;

namespace LSR_Engine.src.Event
{
    internal class EventBus
    {
        private readonly Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        public void Subscribe<TEvent>(Action<TEvent> callback)
        {
            var type = typeof(TEvent);
            if (_subscribers.TryGetValue(type, out var del))
            {
                _subscribers[type] = Delegate.Combine(del, callback);
            }
            else
            {
                _subscribers[type] = callback;
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> callback)
        {
            var type = typeof(TEvent);
            if (_subscribers.TryGetValue(type, out var del))
            {
                var newDel = Delegate.Remove(del, callback);
                if (newDel == null)
                {
                    _subscribers.Remove(type);
                }
                else
                {
                    _subscribers[type] = newDel;
                }
            }
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            var type = typeof(TEvent);
            if (_subscribers.TryGetValue(type, out var del))
            {
                (del as Action<TEvent>)?.Invoke(eventData);
            }
        }
    }
}
