using MediatR;
using System;

namespace TopAct.Domain.Contracts
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}
