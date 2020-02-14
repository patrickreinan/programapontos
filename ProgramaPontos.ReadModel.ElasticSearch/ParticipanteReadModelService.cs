using Nest;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.ElasticSearch.Extensions;
using ProgramaPontos.ReadModel.Participante;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramaPontos.ReadModel.ElasticSearch
{
    public class ParticipanteReadModelService : IParticipanteReadModelService
    {
        private readonly ElasticSearchContext context;

        public ParticipanteReadModelService(ElasticSearchContext context)
        {
            this.context = context;
        }

        public async Task AlterarNomeParticipante(Guid participanteId, string nome)
        {

            var participante = await RetornarParticipanteReadModelPeloId(participanteId);
            participante.Nome = nome;

            var update = new UpdateRequest<ParticipanteReadModel, object>(participante)
            {
                Doc = participante
            };
            var response = context.Client.Update(update);
            response.ThrowIfNotValid();

        }

        public async Task InserirParticipanteReadModel(ParticipanteReadModel participanteReadModel)
        {
            var response = await context.Client.IndexDocumentAsync(participanteReadModel);
            response.ThrowIfNotValid();



        }

        private async Task<ParticipanteReadModel> RetornarParticipanteReadModelPeloId(Guid id)
        {
            var response = await context.Client.SearchAsync<ParticipanteReadModel>(
               s => s.
                   Query(q => q
                       .Match(m => m
                           .Field(f => f.Id)
                           .Query(id.ToString())
                           .Operator(Nest.Operator.And)
                       )
                  )
               );

            response.ThrowIfNotValid();

            return response.Documents.FirstOrDefault();
        }

        public async Task<ParticipanteReadModel> RetornarParticipanteReadModelPeloEmail(string email)
        {
            var response = await context.Client.SearchAsync<ParticipanteReadModel>(
               s => s

                   .Query(q => q
                        .QueryString(qs => qs
                            .Query($"{nameof(ParticipanteReadModel.Email).ToLower()}:\"{email}\"")
                       )
                  )
               );

            response.ThrowIfNotValid();

            return response.Documents.FirstOrDefault();
        }

        public async Task AlterarEmailParticipante(Guid participanteId, string email)
        {
            var participante = await RetornarParticipanteReadModelPeloId(participanteId);
            participante.Email = email;

            var update = new UpdateRequest<ParticipanteReadModel, object>(participante)
            {
                Doc = participante
            };
            var response = await context.Client.UpdateAsync(update);

            response.ThrowIfNotValid();
        }
    }
}
