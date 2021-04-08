using System;
using System.Collections.Generic;
using TopAct.Domain.Contracts;
using TopAct.Domain.Rules;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Types;
using System.Linq;
using MixERP.Net.VCards.Models;
using TopAct.Domain.DtoModels;
using System.IO;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace TopAct.Domain.Entities
{
    public class Contact : Entity, IAggregateRoot
    {
        internal Contact(ContactId id,
            string firstName,
            string lastName,
            string organisationName,
            string websiteUrl,
            string notes,
            IList<Phone> phones,
            IList<Address> addresses,
            IList<Email> emails,
            IList<Category> categories,
            IList<Tag> tags,
            IList<CustomField> customFields)
        {
            CreateOrUpdate(id,
                firstName,
                lastName,
                organisationName,
                websiteUrl,
                notes,
                phones,
                addresses,
                emails,
                categories,
                tags,
                customFields
            );
        }

        [Required]
        public ContactId Id { get; private set; }
        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set; }
        public string OrganisationName { get; private set; }
        public string WebsiteUrl { get; private set; }
        public string Notes { get; private set; }

        public IList<Phone> Phones { get; private set; }
        public IList<Address> Addresses { get; private set; }
        public IList<Email> Emails { get; private set; }
        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<CustomField> CustomFields { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public static Contact CreateContact(
            string firstName,
            string lastName,
            string organisationName,
            string websiteUrl,
            string notes,
            IList<PhoneRequestDto> phones,
            IList<string> addresses,
            IList<string> emails,
            IList<string> categories,
            IList<string> tags,
            Dictionary<string, string> customFields)
        {
            return new Contact(
                new ContactId(Guid.NewGuid()),
                firstName,
                lastName,
                organisationName,
                websiteUrl,
                notes,
                phones.ToDomainPhones(),
                addresses.ToDomainAddresses(),
                emails.ToDomainEmails(),
                categories.ToDomainCategories(),
                tags.ToDomainTags(),
                customFields.ToDomainCustomFields()
            );
        }

        public void EditContact(
            string firstName,
            string lastName,
            string organisationName,
            string websiteUrl,
            string notes,
            IList<PhoneRequestDto> phones,
            IList<string> addresses,
            IList<string> emails,
            IList<string> categories,
            IList<string> tags,
            Dictionary<string, string> customFields)
        {
            CreateOrUpdate(
                Id,
                firstName,
                lastName,
                organisationName,
                websiteUrl,
                notes,
                phones.ToDomainPhones(),
                addresses.ToDomainAddresses(),
                emails.ToDomainEmails(),
                categories.ToDomainCategories(),
                tags.ToDomainTags(),
                customFields.ToDomainCustomFields()
            );
        }

        private void CreateOrUpdate(ContactId id,
            string firstName,
            string lastName,
            string organisationName,
            string websiteUrl,
            string notes,
            IList<Phone> phones,
            IList<Address> addresses,
            IList<Email> emails,
            IList<Category> categories,
            IList<Tag> tags,
            IList<CustomField> customFields)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            OrganisationName = organisationName;
            WebsiteUrl = websiteUrl;
            Notes = notes;
            Phones = phones ?? new List<Phone>();
            Addresses = addresses ?? new List<Address>();
            Emails = emails ?? new List<Email>();
            Categories = categories ?? new List<Category>();
            Tags = tags ?? new List<Tag>();
            CustomFields = customFields ?? new List<CustomField>();

            CheckRule(new ContactNameMustBeFilledRule(this));
            CheckRule(new ContactNotesMustBelow200CharsRule(this));
            CheckRule(new ContactWebSiteUrlMustBeValidRule(this));
        }

        public VCard ExportVCard(ClassificationType classificationType)
        {
            return new VCard
            {
                Version = VCardVersion.V4,
                FirstName = FirstName,
                LastName = LastName,
                FormattedName = FullName,
                Organization = OrganisationName,
                Classification = classificationType,
                Note = Notes,
                Url = new Uri(WebsiteUrl),
                Kind = Kind.Individual,
                Categories = Categories.ToDtos().ToArray(),
                CustomExtensions = CustomFields?
                    .Select(x => new CustomExtension
                    {
                        Key = x.Key,
                        Value = x.Value
                    }
                    ).ToArray(),
                Telephones = Phones?
                    .Select(x => new Telephone
                    {
                        Number = x.PhoneNo,
                        Type = x.Type switch
                        {
                            PhoneType.Home => TelephoneType.Home,
                            PhoneType.Mobile => TelephoneType.Cell,
                            PhoneType.Work => TelephoneType.Work,
                            _ => throw new InvalidDataException($"Invalid Phone Type")
                        }
                    }).ToArray(),
                Emails = Emails?
                    .Select(x => new MixERP.Net.VCards.Models.Email
                    {
                        EmailAddress = x.EmailAddress,
                        Type = EmailType.Smtp
                    }
                    ).ToArray()
                // TODO: export addresses
            };
        }

        public static Contact ImportVCard(VCard vCard, Guid? contactId, ILogger<Contact> logger)
        {
            var fullName = vCard.FormattedName;
            try
            {
                var contact = new Contact(new(contactId ?? new Guid()),
                    vCard.FirstName,
                    vCard.LastName,
                    vCard.Organization,
                    vCard.Url.ToString(),
                    vCard.Note,
                    vCard.Telephones?
                        .Select(
                            x => new Phone(
                                x.Number,
                                x.Type switch
                                {
                                    TelephoneType.Cell => PhoneType.Mobile,
                                    TelephoneType.Home => PhoneType.Home,
                                    TelephoneType.Work => PhoneType.Work,
                                    _ => PhoneType.Home
                                }
                            )
                        ).ToArray() ??
                        Array.Empty<Phone>(),
                    vCard.Addresses?
                        .Select(x => new Address(x.FormatAddress()))
                        .ToArray() ??
                        Array.Empty<Address>(),
                    vCard.Emails?
                        .Select(x => new Email(x.EmailAddress))
                        .ToArray() ??
                        Array.Empty<Email>(),
                    vCard.Categories?
                        .Select(x => new Category(x))
                        .ToArray() ??
                        Array.Empty<Category>(),
                    Array.Empty<Tag>(),
                    vCard.CustomExtensions?
                        .Select(x => new CustomField(x.Key, x.Values.First()))
                        .ToArray() ??
                        Array.Empty<CustomField>()
                );

                logger.LogInformation($"Successfully imported the VCard with name {fullName}");
                return contact;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Cannot import VCard with name {fullName} due to error {ex}");
                return null;
            }
        }
    }
}