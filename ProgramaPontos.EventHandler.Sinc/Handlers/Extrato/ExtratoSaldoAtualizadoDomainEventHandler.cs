using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.ReadModel.Core;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Extrato
{
    public class ExtratoSaldoAtualizadoDomainEventHandler : IDomainEventHandler<ExtratoSaldoAtualizadoDomainEvent>
    {
        private readonly IExtratoReadModelService extratoReadModelService;

        public ExtratoSaldoAtualizadoDomainEventHandler(IExtratoReadModelService extratoReadModelService)
        {
            this.extratoReadModelService = extratoReadModelService;
        }

        public void Handle(ExtratoSaldoAtualizadoDomainEvent @event)
        {          
             extratoReadModelService.AtualizarSaldoExtratoParticipante(@event.AggregateId, @event.Saldo);
        }
    }
}
