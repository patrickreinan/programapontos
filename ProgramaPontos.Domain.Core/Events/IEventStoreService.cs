using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Core.Events
{
   public interface IEventStoreService
    {
        Task SaveAggregate( IAggregateRoot aggregate);

        Task<T> LoadAggregate<T>(Guid aggregateId) where T : IAggregateRoot;

        Task<IAggregateRoot> LoadAggregate(Guid aggregateId, Type type);



    }
}
