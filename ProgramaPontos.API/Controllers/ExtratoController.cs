using Microsoft.AspNetCore.Mvc;
using ProgramaPontos.API.Extensions;
using ProgramaPontos.API.ViewModel;
using ProgramaPontos.Application;
using ProgramaPontos.Application.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProgramaPontos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtratoController : ControllerBase
    {
        private readonly IExtratoApplicationService extratoApplicationService;

        public ExtratoController(IExtratoApplicationService extratoApplicationService)
        {
            this.extratoApplicationService = extratoApplicationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] ExtratoViewModel extratoViewModel)
        {
            return (await extratoApplicationService.CriarExtratoParticipante(extratoViewModel.Id, extratoViewModel.ParticipanteId)).ToActionResult();
        }

        [HttpPost]
        [Route("{participanteId}/adicionarpontosparticipante")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AdicionarPontosParticipante(
            [FromRoute] Guid participanteId,
            PontosViewModel pontosViewModel)
        {
            return (await extratoApplicationService.AdicionarPontosParticipante(participanteId, pontosViewModel.Pontos)).ToActionResult();
        }

        [HttpPost]
        [Route("{participanteId}/removerpontosparticipante")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoverPontosParticipante(
           [FromRoute] Guid participanteId,
           PontosViewModel pontosViewModel)
        {
            return (await extratoApplicationService.RemoverPontosParticipante(participanteId, pontosViewModel.Pontos)).ToActionResult();
        }

        [HttpPost]
        [Route("{participanteId}/quebrapontosparticipante")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> QuebraPontosParticipante(
           [FromRoute] Guid participanteId,
           PontosViewModel pontosViewModel)
        {
            return (await extratoApplicationService.EfetuarQuebraPontosParticipante(participanteId, pontosViewModel.Pontos)).ToActionResult();
        }

        [HttpGet]
        [Route("{participanteId}/extratoparticipante")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid participanteId)
        {
            return (await extratoApplicationService.RetornarExtratoParticipante(participanteId)).ToActionResult();


        }

        [HttpGet]
        [Route("{participanteId}/saldoparticipante")]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Resultado), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSaldoParticipante(Guid participanteId)
        {
            return (await extratoApplicationService.RetornarSaldoParticipante(participanteId)).ToActionResult();
        }
    }
}