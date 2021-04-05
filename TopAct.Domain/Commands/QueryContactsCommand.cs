using System.Collections.Generic;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Commands
{
    public record QueryContactsCommand(string Name,
        string Phone,
        string Email,
        string WebsiteUrl,
        string Notes,
        string[] Categories) :
        CommandBase<IList<QueryContactsDto>>;
}
