using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Services
{
    public interface IParticipanteService
    {
        Task AdicionarParticipante(Guid id, string nome, string email);
        Task AlterarNome(Guid id, string nome);
        Task AlterarEmail(Guid id, string email);
    }
}