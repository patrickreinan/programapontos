using Programapontos.Grpc;
using ProgramaPontos.Application;
using ProgramaPontos.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.gRPC.Server.Extensions
{
    static class ParticipanteExtensions
    {

        public static ParticipanteDTO ToParticipanteDTO(this CriarParticipanteRequest request)
        {
            return new ParticipanteDTO()
            {
                Email = request.Email,
                Id = Guid.Parse(request.Id),
                Nome = request.Nome

            };

        }

        public static CriarParticipanteReply ToCriarParticipanteReply(this Resultado resultado)
        {
            return new CriarParticipanteReply()
            {
                Mensagem = resultado.Mensagens == null ? string.Empty : String.Join("|", resultado.Mensagens),
                Sucesso = resultado.Sucesso
            };
        }

        public static RetornarParticipantePorEmailReply ToRetornarParticipantePorEmailReply(this Resultado<ParticipanteDTO> resultado)
        {

            var dados = new RetornarParticipantePorEmailReply.Types.Dados()
            {
                Email = resultado.Dados.Email,
                Id = resultado.Dados.Id.ToString(),
                Nome = resultado.Dados.Nome
            };

            return new RetornarParticipantePorEmailReply()
            {
                Dados = dados,
                Sucesso = resultado.Sucesso,
                Mensagem = String.Join("|", resultado.Mensagens)
            };

        }
    }
}
