using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;
using TopAct.Domain.Entities;
using TopAct.Domain.Exceptions;

namespace TopAct.Domain.Commanding
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
                    contact.Phones.ToDtos(),
                    contact.Addresses.ToDtos(),
                    contact.Emails.ToDtos(),
                    contact.Categories.ToDtos(),
                    contact.Tags.ToDtos(),
                    contact.CustomFields.ToDtos()
                )
            );
        }
    }
}
