using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramaPontos.Domain.Core.Notifications
{
    class NotificationContext : INotificationContext
    {
        private List<DomainNotification> notifications;


        public NotificationContext()
        {
            notifications = new List<DomainNotification>();
        }

        public IReadOnlyList<DomainNotification> Notifications => notifications;
            
        
        
        public void Add(string message)
        {
            notifications.Add(new DomainNotification(message));
        }

        public bool HasNotifications()
        {
            return notifications.Any();
        }
    }
}
