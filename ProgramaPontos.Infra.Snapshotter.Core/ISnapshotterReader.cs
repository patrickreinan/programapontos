using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Infra.Snapshotter.Core
{
    public interface ISnapshotterReader
    {

        bool HasSnapshotForAggregate(Guid aggregateId);

        IAggregateRoot Load<T>(Guid aggregateId) where T : IAggregateRoot;

    }
}
