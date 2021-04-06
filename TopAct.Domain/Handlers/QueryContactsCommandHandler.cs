using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Handlers
{
    public class QueryContactsCommandHandler : ICommandHandler<QueryContactsCommand, QueryContactsResponseDto>
    {
        private readonly IContactRepository _contactRepository;

        public QueryContactsCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<QueryContactsResponseDto> Handle(QueryContactsCommand command, CancellationToken cancellationToken)
        {
            var contacts = _contactRepository.GetAll(
                    command.Name,
                    command.Phone,
                    command.Email,
                    command.WebsiteUrl,
                    command.Notes,
                    command.Category
                );

            return Task.FromResult(contacts.ToQueryContactsDto());
        }
    }
}
