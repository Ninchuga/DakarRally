using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Models.Domain.Events
{
    public static class DomainEvents
    {
        private static Dictionary<Type, List<Delegate>> _handlers;

        public static void Register<T>(Action<T> eventHandler)
            where T : IAmDomainEvent
        {
            _handlers[typeof(T)].Add(eventHandler);
        }

        public static void Raise<T>(T domainEvent)
            where T : IAmDomainEvent
        {
            foreach (Delegate handler in _handlers[domainEvent.GetType()])
            {
                var action = (Action<T>)handler;
                action(domainEvent);
            }
        }
    }
}
