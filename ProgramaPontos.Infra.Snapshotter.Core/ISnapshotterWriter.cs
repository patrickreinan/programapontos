using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Snapshotter.Core
{
    public interface ISnapshotterWriter
    {
        void AddSnapshot<T>(T aggregateRoot) where T : IAggregateRoot;
    }
}
