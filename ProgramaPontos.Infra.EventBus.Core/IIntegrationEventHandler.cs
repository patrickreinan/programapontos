using ProgramaPontos.Application.IntegrationEvents;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.EventHandler.Sinc
{
   public interface IIntegrationEventHandler<T>  
        where T : IIntegrationEvent
 
    {
        void Handle(T @event);
    }
}
