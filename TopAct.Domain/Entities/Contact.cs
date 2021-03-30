using System.Collections.Generic;

namespace TopAct.Domain.Entities
{
    public class Contact : Entity, IAggregateRoot
    {
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
    }
}