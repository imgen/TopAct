using System;

namespace TopAct.Domain.Contracts
{
    public abstract record CommandBase : ICommand
    {
        public Guid Id { get; init; }

        protected CommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            Id = id;
        }
    }

    public abstract record CommandBase<TResult> : ICommand<TResult>
    {
        public Guid Id { get; init; }

        protected CommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            Id = id;
        }
    }
}