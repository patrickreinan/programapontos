using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Exceptions;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Participante;

using System;
using System.Collections.Generic;

namespace ProgramaPontos.Domain.Aggregates.ParticipanteAggregate
{
    public class Participante : AggregateRoot, ISnapshotAggregate<Participante>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }

        
               

        private Participante(IEnumerable<IDomainEvent> history) : base(history) { }
        private Participante(IAggregateSnapshot snapshot, IEnumerable<IDomainEvent> history) : base(snapshot, history)        {        }



        private Participante() : base() { }


        public Participante(
            Guid id,
            string nome,
            string email) : this()
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarId(id);

            ApplyChange(new ParticipanteCriadoDomainEvent(id, nome,email));
            
        }


      

       
        private void ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                NotificationContext.Add("O e-mail não pode ser vazio");
        }

        private void ValidarId(Guid id)
        {
            if (id == Guid.Empty)
                NotificationContext.Add("O id do participante não pode ser vazio");
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                NotificationContext.Add("O nome do participante é obrigatório");
        }

        public void AlterarNome(string nome)
        {

            ValidarNome(nome);

            if (Nome != nome)
                ApplyChange(new ParticipanteNomeAlteradoDomainEvent(Id, nome));

        }

        public void AlterarEmail(string email)
        {
            ValidarEmail(email);
            if (Email != email)
                ApplyChange(new ParticipanteEmailAlteradoDomainEvent(Id, email));
                
        }

        private void Apply(ParticipanteCriadoDomainEvent e)
        {
            Nome = e.Nome;
            Id = e.AggregateId;
        }

        private void Apply(ParticipanteNomeAlteradoDomainEvent e)
        {
            Nome = e.Nome;
        }

        private void Apply(ParticipanteEmailAlteradoDomainEvent e)
        {
            Email = e.Email;
        }

    }

}
