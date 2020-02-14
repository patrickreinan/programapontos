using Nest;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.ElasticSearch.Extensions;
using ProgramaPontos.ReadModel.Extrato;
using ProgramaPontos.ReadModel.Participante;
using System;

namespace ProgramaPontos.ReadModel.ElasticSearch
{
    public class ElasticSearchContext
    {
        

        internal ElasticClient Client { get; }

        public ElasticSearchContext(ElasticSearchSettings settings)
        {

            
            var connectionSettings = new ConnectionSettings(new Uri(settings.Url));
            ApplyDefaultMappings(connectionSettings);

            Client = new ElasticClient(connectionSettings);

            CreateIndicesIfNotExists();
        }

        private void CreateIndicesIfNotExists()
        {
            CreateIndexIfNotExists<ParticipanteReadModel>();
            CreateIndexIfNotExists<ExtratoParticipanteReadModel>();
            CreateIndexIfNotExists<ExtratoParticipanteSaldoReadModel>();
        }

        private void CreateIndexIfNotExists<T>() where T : IReadModel
        {
            var indexName = GetIndexName<T>().Name;
            
            if (!Client.Indices.Exists(Indices.Parse(indexName)).Exists)
                Client.Indices.Create(indexName);


        }

        private void ApplyDefaultMappings(ConnectionSettings connectionSettings)
        {

            connectionSettings.MappingFor<ParticipanteReadModel>();
            connectionSettings.MappingFor<ExtratoParticipanteReadModel>();
            connectionSettings.MappingFor<ExtratoParticipanteSaldoReadModel>();
        }

        public IndexName GetIndexName<T>() where T : IReadModel
        {
            return typeof(T).Name.ToLower();
        }

    }
}
