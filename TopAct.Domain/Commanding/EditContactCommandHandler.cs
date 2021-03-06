using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;
using TopAct.Domain.Exceptions;

namespace TopAct.Domain.Commanding
{
    public class EditContactCommandHandler : ICommandHandler<EditContactCommand>
    {
        private readonly IContactRepository _contactRepository;

        public EditContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<Unit> Handle(EditContactCommand command, CancellationToken cancellationToken)
        {
            var contactId = command.ContactId;
            var contact = _contactRepository.GetById(new ContactId(contactId));
            if (contact == null)
            {
                throw new ContactNotFoundException($"Cannot find the contact with id {contactId}");
            }
            contact.EditContact(
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
            );

            _contactRepository.Save(contact);

            return Unit.Task;
        }
    }
}
