using ProgramaPontos.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramaPontos.Domain.Core.Result
{
    public static class DomainResultExtensions
    {
        public static DomainResult ToDomainResult(this IReadOnlyCollection<DomainNotification> notifications)
        {
            if (notifications == null || notifications.Count == 0)
                return new DomainResult();
                
            return new DomainResult(false, notifications.Select(s => s.Message).ToArray());
        }

    }
}
