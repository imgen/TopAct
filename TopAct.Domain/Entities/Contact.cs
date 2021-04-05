using System;
using System.Collections.Generic;
using System.Linq;
using TopAct.Domain.Contracts;
using TopAct.Domain.Rules;

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

        public ContactId Id { get; private set; }
        public string FirstName { get; private set; }
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
            IList<string> phones,
            IList<string> addresses,
            IList<string> emails,
            IList<string> categories,
            IList<string> tags,
            IList<CustomField> customFields)
        {
            return new Contact(
                new ContactId(Guid.NewGuid()),
                firstName,
                lastName,
                organisationName,
                websiteUrl,
                notes,
                phones?.Select(x => new Phone(x)).ToList(),
                addresses?.Select(x => new Address(x)).ToList(),
                emails?.Select(x => new Email(x)).ToList(),
                categories?.Select(x => new Category(x)).ToList(),
                tags?.Select(x => new Tag(x)).ToList(),
                customFields
            );
        }

        public void EditContact(
            string firstName,
            string lastName,
            string organisationName,
            string websiteUrl,
            string notes,
            IList<string> phones,
            IList<string> addresses,
            IList<string> emails,
            IList<string> categories,
            IList<string> tags,
            IList<CustomField> customFields)
        {
            CreateOrUpdate(Id,
                firstName,
                lastName,
                organisationName,
                websiteUrl,
                notes,
                phones?.Select(x => new Phone(x)).ToList(),
                addresses?.Select(x => new Address(x)).ToList(),
                emails?.Select(x => new Email(x)).ToList(),
                categories?.Select(x => new Category(x)).ToList(),
                tags?.Select(x => new Tag(x)).ToList(),
                customFields
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
    }
}