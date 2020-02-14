using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Services
{
    public interface IExtratoService
    {
        Task AdicionarPontos(Guid extratoId, int pontos);
        Task CriarExtrato(Guid extratoId, Guid participanteId);
        Task EfetuarQuebraPontos(Guid extratoId, int pontos);
        Task RemoverPontos(Guid extratoId, int pontos);
        Task<Extrato> RetornarExtrato(Guid extratoId);
     
    }
}