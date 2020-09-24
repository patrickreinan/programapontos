
using ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Application.Extensions;
using ProgramaPontos.Application.Services.Interfaces;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.Extrato;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.Services
{
    public class ExtratoApplicationService : IExtratoApplicationService
    {
        private readonly ICommandBus commandBus;
        private readonly IExtratoReadModelService extratoReadModelService;

        public ExtratoApplicationService(ICommandBus commandBus, IExtratoReadModelService extratoReadModelService)
        {
            this.commandBus = commandBus;
            this.extratoReadModelService = extratoReadModelService;
        }

        public async Task<Resultado> AdicionarPontosParticipante(Guid participanteId, int pontos)
        {

            return await ExecutaAcaoSeExtratoExiste(participanteId,
                   (extratoId) => commandBus.EnviarCommandoRetornaResultadoAsync(new AdicionarPontosExtratoCommand(extratoId, pontos)).Result);


        }



        public async Task<Resultado> RemoverPontosParticipante(Guid participanteId, int pontos)
        {
            return await ExecutaAcaoSeExtratoExiste(participanteId,
                   (extratoId) => commandBus.EnviarCommandoRetornaResultadoAsync(new RemoverPontosExtratoCommand(extratoId, pontos)).Result);

        }

        public async Task<Resultado> EfetuarQuebraPontosParticipante(Guid participanteId, int pontos)
        {
            return await ExecutaAcaoSeExtratoExiste(participanteId,
                    (extratoId) => commandBus.EnviarCommandoRetornaResultadoAsync(new EfetuarQuebraPontosExtratoCommand(extratoId, pontos)).Result);

        }

        public async Task<Resultado> ExecutaAcaoSeExtratoExiste(Guid participanteId, Func<Guid, Resultado> action)
        {
            var extratoId = await RetornarExtrato(participanteId);
            if (!extratoId.Sucesso)
                return extratoId;

            return action.Invoke(extratoId.Dados);

        }



        public async Task<Resultado> CriarExtratoParticipante(Guid extratoId, Guid participanteId)
        {
            return await commandBus.EnviarCommandoRetornaResultadoAsync(new CriarExtratoCommand(extratoId, participanteId));
        }


        public async Task<Resultado<ExtratoParticipanteReadModel>> RetornarExtratoParticipante(Guid participanteId)
        {

            var resultado = await RetornarExtrato(participanteId);

            return resultado.Sucesso ?
                new Resultado<ExtratoParticipanteReadModel>(await extratoReadModelService.RetornarExtrato(resultado.Dados)) :
                new Resultado<ExtratoParticipanteReadModel>(false, resultado.Mensagens);


        }
        private async Task<Resultado<Guid>> RetornarExtrato(Guid participanteId)
        {
            var extratoId = await extratoReadModelService.RetornarIdExtrato(participanteId);
            Resultado<Guid> resultado;

            resultado = !extratoId.HasValue ?
                        new Resultado<Guid>(false, $"O extrato {extratoId} não existe.") :
                        new Resultado<Guid>(extratoId.Value);

            return await Task.FromResult(resultado);
        }

        public async Task<Resultado<int>> RetornarSaldoParticipante(Guid participanteId)
        {
            var resultado = await extratoReadModelService.RetornarSaldoParticipante(participanteId);

            return resultado.HasValue ?
                new Resultado<int>(resultado.Value) :
                new Resultado<int>(false, "$O participante não possui saldo");

        }
    }
}
