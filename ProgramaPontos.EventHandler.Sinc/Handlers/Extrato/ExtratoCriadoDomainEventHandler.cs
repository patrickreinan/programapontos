using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.Extrato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Extrato
{
    public class ExtratoCriadoDomainEventHandler : IDomainEventHandler<ExtratoCriadoDomainEvent>
    {
        private readonly IExtratoReadModelService extratoReadModelService;

        public ExtratoCriadoDomainEventHandler(IExtratoReadModelService extratoReadModelService)
        {
            this.extratoReadModelService = extratoReadModelService;
        }

        public async Task Handle(ExtratoCriadoDomainEvent @event)
        {
            await extratoReadModelService.InserirExtratoReadModel(new ExtratoParticipanteReadModel()
            {
                ExratoId = @event.AggregateId,
                ParticipanteId = @event.ParticipanteId,
                Id = Guid.NewGuid()
            });

            await extratoReadModelService.InserirExtratoParticipanteSaldoReadModel(new ExtratoParticipanteSaldoReadModel()
            {
                ExtratoId = @event.AggregateId,
                ParticipanteId = @event.ParticipanteId,
                Id = Guid.NewGuid()
            });
        }
    }
}
