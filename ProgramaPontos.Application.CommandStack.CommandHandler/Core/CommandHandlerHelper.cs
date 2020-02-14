using ProgramaPontos.Application.CommandStack.Responses;
using ProgramaPontos.Domain.Core.Result;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.CommandStack.Core
{
    public class CommandHandlerHelper
    {


        public static async Task<ICommandResponse> ExecuteToResponse(Action action)
        {
            action.Invoke();
            return await Task.FromResult((ICommandResponse)new SuccessCommandResponse());
        }


        public static async Task<ICommandResponse> ExecuteToResponse<T>(Func<Task<T>> func) where T : DomainResult
        {

            var result = await func.Invoke();

            if (result.Success)
                return (ICommandResponse)new SuccessCommandResponse();
            else
                return (ICommandResponse)new ErrorCommandResponse(result.Reasons);


        }


    }
}
