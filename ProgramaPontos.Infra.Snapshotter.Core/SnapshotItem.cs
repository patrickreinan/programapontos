using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Snapshotter.Core
{
    public class SnapshotItem
    {
        public Guid AggregateId { get; }
        public int Version { get; }
        public byte[] Data { get; }

        public SnapshotItem(Guid aggregateId, int version, byte[] data)
        {
            AggregateId = aggregateId;
            Version = version;
            Data = data;
        }
    }
}
