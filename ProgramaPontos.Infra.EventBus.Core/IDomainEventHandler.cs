using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc
{
   public interface IDomainEventHandler<T>  
        where T : IDomainEvent 
 
    {
        Task Handle(T @event);
    }
}
