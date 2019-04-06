using ProgramaPontos.Domain.Aggregates.ParticipanteAggregate;
using ProgramaPontos.Domain.Core.Events;
using System;

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


        public void AdicionarParticipante(Guid id, string nome, string email)
        {

            var participante = new Participante(id, nome, email);
            eventStoreService.SaveAggregate(participante);

        }

        public void AlterarEmail(Guid id, string email)
        {
            var participante = eventStoreService.LoadAggregate<Participante>(id);
            participante.AlterarEmail(email);
            eventStoreService.SaveAggregate(participante);
        }

        public void AlterarNome(Guid id, string nome)
        {

            var participante = eventStoreService.LoadAggregate<Participante>(id);
            participante.AlterarNome(nome);
            eventStoreService.SaveAggregate(participante);

        }

       


    }
}
