using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProgramaPontos.ReadModel.Extrato;

namespace ProgramaPontos.ReadModel.Core
{
    public interface IExtratoReadModelService
    {
        Task<Guid?> RetornarIdExtrato(Guid participanteId);
        Task InserirExtratoReadModel(ExtratoParticipanteReadModel extratoParticipanteReadModel);
        Task AdicionarPontosExtrato(Guid extratoId, DateTime data, int pontos);
        Task RemoverPontosExtrato(Guid extratoId, DateTime data, int pontos);
        Task QuebraPontosExtrato(Guid extratoId, DateTime data, int pontos);
        Task<ExtratoParticipanteReadModel> RetornarExtrato(Guid extratoId);
        Task AtualizarSaldoExtratoParticipante(Guid extratoId, int saldo);
        Task InserirExtratoParticipanteSaldoReadModel(ExtratoParticipanteSaldoReadModel extratoParticipanteSaldoReadModel);
        Task<int?> RetornarSaldoParticipante(Guid participanteId);
    }
}
