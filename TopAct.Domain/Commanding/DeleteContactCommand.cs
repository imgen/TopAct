using System;
using TopAct.Domain.Contracts;

namespace TopAct.Domain.Commanding
{
    public record DeleteContactCommand(Guid ContactId) : CommandBase;
}
