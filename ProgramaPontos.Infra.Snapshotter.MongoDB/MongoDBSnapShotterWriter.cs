using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Infra.Snapshotter.Core;
using System;

namespace ProgramaPontos.Infra.Snapshotter.MongoDB
{
    public class MongoDBSnapShotterWriter : ISnapshotterWriter
    {
        public void AddSnapshot<T>(T aggregateRoot) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }

     
    }
}
