using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Application.IntegrationEvents
{
   public interface IIntegrationBus
    {
        void PublishEvent<T>(T @event) where T : IIntegrationEvent;

        Action<IIntegrationEvent> OnRaiseEvent { get; set; }

        void Consume();

    }
}
