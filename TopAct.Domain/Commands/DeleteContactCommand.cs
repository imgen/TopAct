using System;
using TopAct.Domain.Contracts;

namespace TopAct.Domain.Commands
{
    public record DeleteContactCommand(Guid ContactId) : CommandBase;
}
