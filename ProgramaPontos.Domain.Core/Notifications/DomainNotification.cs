using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Notifications
{
    public class DomainNotification
    {
        public DomainNotification(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
