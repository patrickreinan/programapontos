using Nest;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.ElasticSearch.Extensions;
using ProgramaPontos.ReadModel.Extrato;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramaPontos.ReadModel.ElasticSearch
{
    public class ExtratoReadModelService : IExtratoReadModelService
    {
        private readonly ElasticSearchContext context;

        public ExtratoReadModelService(ElasticSearchContext context)
        {
            this.context = context;

        }



        public async Task InserirExtratoReadModel(ExtratoParticipanteReadModel extratoParticipanteReadModel)
        {


            var response = await context.Client.IndexDocumentAsync(extratoParticipanteReadModel);
                response.ThrowIfNotValid();

        }

        public async Task AdicionarPontosExtrato(Guid extratoId, DateTime data, int pontos)
        {
            await AdicionarMovimentacao(extratoId, data, pontos, "Pontos adicionados");
        }
        public async Task QuebraPontosExtrato(Guid extratoId, DateTime data, int pontos)
        {
            await AdicionarMovimentacao(extratoId, data, pontos, "Quebra de pontos");
        }

        private async Task AdicionarMovimentacao(Guid extratoId, DateTime data, int pontos, string tipo)
        {
            var extrato =await RetornarExtrato(extratoId);
            extrato.Movimentacoes.Add(new MovimentacaoExtratoReadModel()
            {
                Data = data,
                Pontos = pontos,
                Tipo = tipo
            });

           await AtualizarMovimentacaoExtrato(extrato);
        }


        public async Task AtualizarSaldoExtratoParticipante(Guid extratoId, int saldo)
        {
            var saldoParticipante = await RetornarSaldoExtratoParticipante(extratoId);

            saldoParticipante.Saldo = saldo;

            var update = new UpdateRequest<ExtratoParticipanteSaldoReadModel, object>(saldoParticipante)
            {
                Doc = new { saldoParticipante.Saldo }
            };

            var response = await context.Client.UpdateAsync(update);
            response.ThrowIfNotValid();


        }
        private async Task AtualizarMovimentacaoExtrato(ExtratoParticipanteReadModel extrato)
        {

            var update = new UpdateRequest<ExtratoParticipanteReadModel, object>(extrato)
            {
                Doc = extrato
            };

            var response = await context.Client.UpdateAsync(update);
            response.ThrowIfNotValid();
        }
    

       
        private async Task<ExtratoParticipanteSaldoReadModel> RetornarSaldoExtratoParticipante(Guid participanteId)
        {
            var searchResponse = await context.Client.SearchAsync<ExtratoParticipanteSaldoReadModel>(s => s
                          .Query(q => q
                              .Match(m => m
                                  .Field(f => f.ExtratoId)
                                  .Query(participanteId.ToString())
                                  .Operator(Operator.And)
                              )
                          )
                       );

            searchResponse.ThrowIfNotValid();

            return searchResponse.Documents.FirstOrDefault();
        }

        

        public async Task RemoverPontosExtrato(Guid aggregateId, DateTime data, int pontos)
        {
            await AdicionarMovimentacao(aggregateId, data, pontos, "Pontos removidos");
        }

        public  async Task<ExtratoParticipanteReadModel> RetornarExtrato(Guid extratoId)
        {
            var response = await context.Client.SearchAsync<ExtratoParticipanteReadModel>(
                          s => s
                              .Query(q => q
                                  .Match(m => m
                                      .Field(f => f.ExratoId)
                                      .Query(extratoId.ToString())
                                      .Operator(Nest.Operator.And)
                                   )
                              )

                          );


            response.ThrowIfNotValid();

            if (response.Documents.Count == 0)
                return null;

            return response.Documents.First();

        }

        public async Task<Guid?> RetornarIdExtrato(Guid participanteId)
        {



            var response = await context.Client.SearchAsync<ExtratoParticipanteReadModel>(
                            s => s
                                .Query(q => q
                                    .Match(m => m
                                        .Field(f => f.ParticipanteId)
                                        .Query(participanteId.ToString())
                                        .Operator(Nest.Operator.And)
                                     )
                                )

                            );


            response.ThrowIfNotValid();

            if (response.Documents.Count == 0)
                return null;

            return response.Documents.First().ExratoId;


        }

        

        public async Task InserirExtratoParticipanteSaldoReadModel(ExtratoParticipanteSaldoReadModel extratoParticipanteSaldoReadModel)
        {
            var response = await context.Client.IndexDocumentAsync(extratoParticipanteSaldoReadModel);

            response.ThrowIfNotValid();
        }

        public async Task<int?> RetornarSaldoParticipante(Guid participanteId)
        {
            var response = await context.Client.SearchAsync<ExtratoParticipanteSaldoReadModel>(
                s => s
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.ParticipanteId)
                            .Query(participanteId.ToString())
                            .Operator(Operator.And)
                        )
                    )
                );

            response.ThrowIfNotValid();

            if (response.Documents.Count == 0)
                return null ;
            
            return response.Documents.First().Saldo;
        }
    }
}
