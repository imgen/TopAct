using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;

namespace TopAct.Domain.Handlers
{
    public class QueryContactsCommandHandler : ICommandHandler<QueryContactsCommand, IList<QueryContactsItemDto>>
    {
        private readonly IContactRepository _contactRepository;

        public QueryContactsCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<IList<QueryContactsItemDto>> Handle(QueryContactsCommand command, CancellationToken cancellationToken)
        {
            var contacts = _contactRepository.GetAll(
                    command.Name,
                    command.Phone,
                    command.Email,
                    command.WebsiteUrl,
                    command.Notes,
                    command.Category
                );

            IList<QueryContactsItemDto> results = contacts
                .Select(x =>
                    new QueryContactsItemDto(
                        x.Id.Value,
                        x.FirstName,
                        x.LastName,
                        x.OrganisationName,
                        x.WebsiteUrl,
                        x.Notes,
                        x.Tags?.Select(x => x.TagName)?.ToArray() ??
                            Array.Empty<string>()
                    )
                ).ToList();
            return Task.FromResult(results);
        }
    }
}
