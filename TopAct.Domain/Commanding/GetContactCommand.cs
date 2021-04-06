using System;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Commanding
{
    public record GetContactCommand(Guid ContactId) :
        CommandBase<CreateOrGetContactResponseDto>;
}
