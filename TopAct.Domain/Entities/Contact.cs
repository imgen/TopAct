using System;
using System.Collections.Generic;

namespace TopAct.Domain.Entities
{
    public class Contact : Entity, IAggregateRoot
    {
        private Contact(ContactId id,
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
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            OrganisationName = organisationName;
            WebsiteUrl = websiteUrl;
            Notes = notes;
            Phones = phones ?? new List<Phone>();
            Addresses = addresses ?? new List<Address>();
            Emails = emails ?? new List<Email>();
            Categories = categories ?? new List<Category>();
            Tags = tags ?? new List<Tag>();
            CustomFields = customFields ?? new List<CustomField>();
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

        public static Contact CreateContact(string firstName,
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
            return new Contact(new ContactId(Guid.NewGuid()),
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
    }
}