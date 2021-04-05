using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;
using TopAct.Domain.Entities;
using TopAct.Domain.Exceptions;

namespace TopAct.Domain.Handlers
{
    public class GetContactCommandHandler : ICommandHandler<GetContactCommand, CreateOrGetContactResponseDto>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<CreateOrGetContactResponseDto> Handle(GetContactCommand command, CancellationToken cancellationToken)
        {
            var contactId = command.ContactId;
            var contact = _contactRepository.GetById(new ContactId(contactId));
            if (contact == null)
            {
                throw new ContactNotFoundException($"Cannot find the contact with id {contactId}");
            }

            return Task.FromResult(
                new CreateOrGetContactResponseDto(
                    contactId,
                    contact.FirstName,
                    contact.LastName,
                    contact.OrganisationName,
                    contact.WebsiteUrl,
                    contact.Notes,
                    contact.Phones?.Select(x => x.PhoneNo).ToArray() ?? Array.Empty<string>(),
                    contact.Addresses?.Select(x => x.AddressName).ToArray() ?? Array.Empty<string>(),
                    contact.Emails?.Select(x => x.EmailAddress).ToArray() ?? Array.Empty<string>(),
                    contact.Categories?.Select(x => x.CategoryName).ToArray() ?? Array.Empty<string>(),
                    contact.Tags?.Select(x => x.TagName).ToArray() ?? Array.Empty<string>(),
                    contact.CustomFields?.ToDictionary(x => x.Key, x => x.Value) ?? new()
                )
            );
        }
    }
}
