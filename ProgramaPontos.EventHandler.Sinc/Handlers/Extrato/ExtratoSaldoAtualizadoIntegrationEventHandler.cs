using ProgramaPontos.Application.IntegrationEvents;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.ReadModel.Core;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Extrato
{
    public class ExtratoSaldoAtualizadoIntegrationEventHandler : IIntegrationEventHandler<ExtratoSaldoAtualizadoIntegrationEvent>
    {
        private readonly IExtratoReadModelService extratoReadModelService;

        public ExtratoSaldoAtualizadoIntegrationEventHandler(IExtratoReadModelService extratoReadModelService)
        {
            this.extratoReadModelService = extratoReadModelService;
        }

        public void Handle(ExtratoSaldoAtualizadoIntegrationEvent @event)
        {          
             extratoReadModelService.AtualizarSaldoExtratoParticipante(@event.AggregateId, @event.Saldo);
        }
    }
}
