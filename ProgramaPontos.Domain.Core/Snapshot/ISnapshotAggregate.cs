using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotAggregate<T> where T: IAggregateRoot
    {
        
    }
}
