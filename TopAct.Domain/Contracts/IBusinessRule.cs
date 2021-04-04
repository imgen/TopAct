namespace TopAct.Domain.Contracts
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
