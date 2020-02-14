using ProgramaPontos.Application.CommandStack.Responses;


using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.CommandStack.Core
{
    public class CommandHandlerHelper
    {


        public static async Task<ICommandResponse> ExecuteToResponse(Action action)
        {
               try
               {
                   action.Invoke();
                   return await Task.FromResult((ICommandResponse) new SuccessCommandResponse());
               }
               catch (Exception ex)
               {
                   return await Task.FromResult((ICommandResponse) new ErrorCommandResponse(ex));
               }
          ;
        }


    }
}
