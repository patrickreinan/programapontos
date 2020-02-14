using Microsoft.AspNetCore.Mvc;
using ProgramaPontos.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramaPontos.API.Extensions
{
    public static class ResultadoExtensions
    {
        public static IActionResult ToActionResult(this Resultado resultado)
        {
            return (resultado.Sucesso) ?
                new OkObjectResult(resultado) :
                new ObjectResult(resultado) { StatusCode = 400 };
                
        }

        public static IActionResult ToActionResult<T>(this Resultado<T> resultado)
        {
            return (resultado.Sucesso) ?
                new OkObjectResult(resultado) :
                new ObjectResult(resultado) { StatusCode = 400 };

        }
    }
}
