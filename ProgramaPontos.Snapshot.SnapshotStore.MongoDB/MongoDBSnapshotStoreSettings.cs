using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    public class MongoDBSnapshotStoreSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
