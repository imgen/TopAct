using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Commanding
{
    public record QueryContactsCommand(string Name,
        string Phone,
        string Email,
        string WebsiteUrl,
        string Notes,
        string Category) :
        CommandBase<QueryContactsResponseDto>;
}
