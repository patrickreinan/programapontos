using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Application.CommandStack.Responses
{
    public class ErrorCommandResponse : CommandResponse
    {
        public ErrorCommandResponse(string[] reasons) : base(false, reasons)
        {
        }

    }
}
