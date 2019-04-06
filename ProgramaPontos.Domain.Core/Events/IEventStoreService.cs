using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Events
{
   public interface IEventStoreService
    {
        void SaveAggregate( IAggregateRoot aggregate);

        T LoadAggregate<T>(Guid aggregateId) where T : IAggregateRoot;





    }
}
