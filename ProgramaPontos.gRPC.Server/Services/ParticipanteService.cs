using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Programapontos.Grpc;
using ProgramaPontos.Application.Services.Interfaces;
using ProgramaPontos.gRPC.Server.Extensions;
using static Programapontos.Grpc.ParticipanteService;

namespace ProgramaPontos.gRPC.Server.Services
{
    class ParticipanteService : ParticipanteServiceBase
    {
        private readonly IParticipanteApplicationService participanteApplicationService;

        public ParticipanteService(IParticipanteApplicationService participanteApplicationService)
        {
            this.participanteApplicationService = participanteApplicationService;
        }

        public async override Task<CriarParticipanteReply> CriarParticipante(CriarParticipanteRequest request, ServerCallContext context)
        {

            var result = await participanteApplicationService.CriarParticipante(request.ToParticipanteDTO());
            return result.ToCriarParticipanteReply();

           
        }

        public async override Task<RetornarParticipantePorEmailReply> RetornarParticipantePorEmail(RetornarParticipantePorEmailRequest request, ServerCallContext context)
        {

            var result = await participanteApplicationService.RetornarParticipantePorEmail(request.Email);
            return result.ToRetornarParticipantePorEmailReply();

        }
    }
}
