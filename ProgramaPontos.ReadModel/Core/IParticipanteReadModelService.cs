using System;
using System.Threading.Tasks;
using ProgramaPontos.ReadModel.Participante;

namespace ProgramaPontos.ReadModel.Core
{
    public interface IParticipanteReadModelService
    {
        Task InserirParticipanteReadModel(ParticipanteReadModel participanteReadModel);
        Task AlterarNomeParticipante(Guid participanteId, string nome);
        Task<ParticipanteReadModel> RetornarParticipanteReadModelPeloEmail(string email);
        Task AlterarEmailParticipante(Guid participanteId, string email);
    }
}