using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Commands;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;
using TopAct.Domain.Exceptions;

namespace TopAct.Domain.Handlers
{
    public class DeleteContactCommandHandler : ICommandHandler<DeleteContactCommand>
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<Unit> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            var contactId = command.ContactId;
            var contact = _contactRepository.GetById(new ContactId(contactId));
            if (contact == null)
            {
                throw new DataNotFoundException($"Cannot find the contact with id {contactId}");
            }

            _contactRepository.Delete(contact);

            return Unit.Task;
        }
    }
}
