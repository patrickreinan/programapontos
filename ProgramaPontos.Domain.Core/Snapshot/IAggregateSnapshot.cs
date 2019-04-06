using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface IAggregateSnapshot
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
}