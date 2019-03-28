using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Snapshotter
{
     interface IDomainEventHandler<T>
          where T : IDomainEvent

    {
        void Handle(T @event);
    }
}
