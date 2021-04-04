using MediatR;

namespace TopAct.Domain.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}