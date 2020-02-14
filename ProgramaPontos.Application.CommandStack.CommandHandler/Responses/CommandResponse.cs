using ProgramaPontos.Application.CommandStack.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Application.CommandStack.Responses
{
    public class CommandResponse : ICommandResponse
    {
        

        public CommandResponse(bool isValid, string[] reasons)
        {
            IsValid = isValid;
            Reasons = reasons;
        }

        public bool IsValid { get; }
        
        public string[] Reasons { get; }
    }
}
