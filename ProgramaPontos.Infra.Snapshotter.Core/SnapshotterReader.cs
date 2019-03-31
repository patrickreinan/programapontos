using System;
using System.Collections.Generic;
using System.Text;
using ProgramaPontos.Domain.Core.Aggregates;

namespace ProgramaPontos.Infra.Snapshotter.Core
{
    class SnapshotterReader : ISnapshotterReader
    {
        public bool HasSnapshotForAggregate(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public IAggregateRoot Load<T>(Guid aggregateId) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
