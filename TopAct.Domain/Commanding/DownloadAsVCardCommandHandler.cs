using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Contracts;
using TopAct.Domain.Exceptions;

namespace TopAct.Domain.Commanding
{
    public class DownloadAsVCardCommandHandler : ICommandHandler<DownloadAsVCardCommand, byte[]>
    {
        private readonly IContactRepository _contactRepository;

        public DownloadAsVCardCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<byte[]> Handle(DownloadAsVCardCommand command, CancellationToken cancellationToken)
        {
            var contacts = _contactRepository.GetAllByIds(command.ContactIds);
            if (!contacts.Any())
            {
                throw new ContactNotFoundException("There are no contacts that matches the provided contact ids");
            }
            var vCards = contacts.Select(x => x.ExportVCard(ClassificationType.Public))
                    .ToArray();
            var serializedVCards = vCards.Serialize();
            var bytes = Encoding.UTF8.GetBytes(serializedVCards);
            return Task.FromResult(bytes);
        }
    }
}
