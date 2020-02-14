using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Notifications
{
    public interface INotificationContext
    {
        IReadOnlyList<DomainNotification> Notifications { get; }

        void Add(string message);
        bool HasNotifications();
    }
}
