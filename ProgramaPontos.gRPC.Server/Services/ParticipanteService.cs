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

        public override Task<CriarParticipanteReply> CriarParticipante(CriarParticipanteRequest request, ServerCallContext context)
        {

            return Task.Run(() =>
                {
                    var result = participanteApplicationService.CriarParticipante(request.ToParticipanteDTO()).Result;
                    return result.ToCriarParticipanteReply();

                });
        }

        public override Task<RetornarParticipantePorEmailReply> RetornarParticipantePorEmail(RetornarParticipantePorEmailRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var result = participanteApplicationService.RetornarParticipantePorEmail(request.Email).Result;
                return result.ToRetornarParticipantePorEmailReply();
            });
        }
    }
}
