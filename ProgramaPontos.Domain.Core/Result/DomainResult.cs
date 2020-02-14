using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Result
{
    public class DomainResult
    {
        public DomainResult() : this(true, null)
        {

        }


        public DomainResult(string mensagem) : this(false, new[] { mensagem })
        {

        }

        public DomainResult(bool success, string[] reasons)
        {
            Success = success;
            Reasons = reasons;
        }

        public bool Success { get;  }

        public string[] Reasons { get; }
    }
}
