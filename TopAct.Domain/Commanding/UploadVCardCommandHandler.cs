using Microsoft.Extensions.Logging;
using MixERP.Net.VCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TopAct.Domain.Contracts;
using TopAct.Domain.DtoModels;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Commanding
{
    public class UploadVCardCommandHandler : ICommandHandler<UploadVCardCommand, UploadVCardResponseDto>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILoggerFactory _loggerFactory;

        public UploadVCardCommandHandler(IContactRepository contactRepository, ILoggerFactory loggerFactory)
        {
            _contactRepository = contactRepository;
            _loggerFactory = loggerFactory;
        }

        public async Task<UploadVCardResponseDto> Handle(UploadVCardCommand command, CancellationToken cancellationToken)
        {
            var bytes = command.Bytes;
            var str = Encoding.UTF8.GetString(bytes);
            var vCards = Deserializer.GetVCards(str).ToArray();
            var contacts = _contactRepository.GetAll();
            var contactsByFullName = contacts.ToDictionary(x => x.FullName.ToLowerInvariant());
            int totalVCardContactCount = vCards.Length;
            int updatedContactCount = 0;
            int createdContactCount = 0;
            int skippedVCardContactCount = 0;

            var logger = _loggerFactory.CreateLogger<UploadVCardCommand>();

            if (totalVCardContactCount == 0)
            {
                logger.LogInformation($"Found {totalVCardContactCount} VCard contacts");
                return new(0, 0, 0, 0);
            }

            logger.LogInformation($"Found {totalVCardContactCount} VCard contacts");
            List<Contact> importedContacts = new();
            foreach (var vCard in vCards)
            {
                var fullName = vCard.FormattedName;
                Guid? contactId =
                    contactsByFullName.TryGetValue(fullName.ToLowerInvariant(), out var contact) ?
                    contact.Id.Value : null;

                var importedContact = Contact.ImportVCard(vCard,
                    contactId,
                    _loggerFactory.CreateLogger<Contact>()
                );

                if (importedContact is null)
                {
                    skippedVCardContactCount++;
                }
                else
                {
                    if (contactId is null)
                    {
                        createdContactCount++;
                    }
                    else
                    {
                        updatedContactCount++;
                    }
                    importedContacts.Add(importedContact);
                }
            }
            var stats = new UploadVCardResponseDto
            (
                totalVCardContactCount,
                updatedContactCount,
                createdContactCount,
                skippedVCardContactCount
            );
            if (importedContacts.Any())
            {
                _contactRepository.UpsertAll(importedContacts);
            }
            else
            {
                logger.LogWarning($"Cannot import any VCard contacts");
            }
            logger.LogInformation($"Import stats: {stats}");

            return stats;
        }
    }
}
