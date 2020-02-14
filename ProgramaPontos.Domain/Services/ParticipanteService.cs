using ProgramaPontos.Domain.Aggregates.ParticipanteAggregate;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Result;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IEventStoreService eventStoreService;

        public ParticipanteService(IEventStoreService eventStoreService
            )
        {
            this.eventStoreService = eventStoreService;

        }


        public async Task<DomainResult> AdicionarParticipante(Guid id, string nome, string email)
        {

            var participante = new Participante(id, nome, email);

            if (!participante.IsValid())
                return participante.Notifications().ToDomainResult();

            await eventStoreService.SaveAggregate(participante);

            return new DomainResult();
        }

        public async Task AlterarEmail(Guid id, string email)
        {
            var participante = await eventStoreService.LoadAggregate<Participante>(id);
            participante.AlterarEmail(email);
            await eventStoreService.SaveAggregate(participante);
        }

        public async Task AlterarNome(Guid id, string nome)
        {

            var participante = await eventStoreService.LoadAggregate<Participante>(id);
            participante.AlterarNome(nome);
            await eventStoreService.SaveAggregate(participante);

        }




    }
}
