using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Repository
{
    public interface IExtratoParticipanteRepository
    {
        Task<bool> ExisteExtratoParticipante(Guid id);
    }
}
