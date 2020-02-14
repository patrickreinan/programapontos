using ProgramaPontos.Domain.Repository;
using ProgramaPontos.ReadModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.Infra.Repository
{
    public class ExtratoParticipanteRepository : IExtratoParticipanteRepository
    {
        private readonly IExtratoReadModelService extratoParticipanteReadModelService;

        public ExtratoParticipanteRepository(IExtratoReadModelService extratoParticipanteReadModelService)
        {
            this.extratoParticipanteReadModelService = extratoParticipanteReadModelService;
        }

        public async Task<bool> ExisteExtratoParticipante(Guid participanteId)
        {
            var resultado= await extratoParticipanteReadModelService.RetornarIdExtrato(participanteId);
            return resultado.HasValue;
        }
    }
}
