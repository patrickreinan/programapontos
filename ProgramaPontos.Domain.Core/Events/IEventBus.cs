using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Events
{
    public interface IEventBus
    {
        void PublishEvent<T>(T @event) where T : IDomainEvent;
        
        Action<IDomainEvent> OnRaiseEvent { get; set; }

        void Consume();
        
    }
}
