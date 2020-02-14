using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Exceptions;
using ProgramaPontos.Domain.Core.Result;
using ProgramaPontos.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Services
{
    public class ExtratoService : IExtratoService
    {
        private readonly IEventStoreService eventStoreService;
        private readonly IExtratoParticipanteRepository extratoRepository;

        public ExtratoService(
            IEventStoreService eventStoreService,
            IExtratoParticipanteRepository extratoRepository
            )
        {
            this.eventStoreService = eventStoreService;
            this.extratoRepository = extratoRepository;
        }

        public async Task<Extrato> RetornarExtrato(Guid id)
        {
            return await eventStoreService.LoadAggregate<Extrato>(id);
        }

        public async Task<DomainResult> CriarExtrato(Guid extratoId, Guid participanteId)
        {

            if (await extratoRepository.ExisteExtratoParticipante(participanteId))
                return new DomainResult("O participante já possui extrato.");

            var extrato = new Extrato(extratoId, participanteId);


            await eventStoreService.SaveAggregate(extrato);
            return new DomainResult();
        }

        

        public async Task AdicionarPontos(Guid extratoId, int pontos)
        {
            var extrato = await eventStoreService.LoadAggregate<Extrato>(extratoId);
            extrato.AdicionarPontos(pontos);
            await eventStoreService.SaveAggregate(extrato);
        }

        public async Task<DomainResult> RemoverPontos(Guid extratoId, int pontos)
        {
            var extrato = await eventStoreService.LoadAggregate<Extrato>(extratoId);

            if (extrato.Saldo < pontos)
                return new DomainResult("Quantidade de pontos maior do que o saldo");

            extrato.RemoverPontos(pontos);
            await eventStoreService.SaveAggregate(extrato);
            return new DomainResult();

        }

        public async Task EfetuarQuebraPontos(Guid extratoId, int pontos)
        {
            var extrato = await eventStoreService.LoadAggregate<Extrato>(extratoId);
            extrato.EfetuarQuebra(pontos);
           await  eventStoreService.SaveAggregate(extrato);

        }



    }
}
