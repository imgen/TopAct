using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Handlers
{
    public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand, Guid>
    {
        private readonly IContactRepository _contactRepository;

        public CreateContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<Guid> Handle(CreateContactCommand command, CancellationToken cancellationToken)
        {
            var contact = Contact.CreateContact(
                command.FirstName,
                command.LastName,
                command.OrganisationName,
                command.WebsiteUrl,
                command.Notes,
                command.Phones,
                command.Addresses,
                command.Emails,
                command.Categories,
                command.Tags,
                command.CustomFields
                    ?.Select(x => new CustomField(x.Key, x.Value))
                    .ToList()
            );

            _contactRepository.Add(contact);
            return Task.FromResult(contact.Id.Value);
        }
    }
}
