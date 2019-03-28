using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Infra.Snapshotter.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Snapshotter.MongoDB
{
    class MongoDBSnapShotterReader : ISnapshotterReader
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
