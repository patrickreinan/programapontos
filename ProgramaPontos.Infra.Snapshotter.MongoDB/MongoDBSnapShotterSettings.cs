using System;

namespace ProgramaPontos.Infra.Snapshotter.MongoDB
{
    public class MongoDBSnapShotterSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SnapshotCollectionName { get; set; }
    }
}
