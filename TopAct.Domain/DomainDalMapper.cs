using System.Linq;
using TopAct.Domain.Entities;
using DalContact = TopAct.Infrastructure.Dal.Entities.Contact;

namespace TopAct.Domain
{
    public static class DomainDalMapper
    {
        public static DalContact ToDal(this Contact contact)
        {
            return contact == null ? null : new DalContact
            {
                Id = contact.Id.Value,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                OrganisationName = contact.OrganisationName,
                WebsiteUrl = contact.WebsiteUrl,
                Notes = contact.Notes,
                Phones = contact.Phones.Select(x => x.PhoneNo).ToList(),
                Addresses = contact.Addresses.Select(x => x.AddressName).ToList(),
                Emails = contact.Emails.Select(x => x.EmailAddress).ToList(),
                Categories = contact.Categories.Select(x => x.CategoryName).ToList(),
                Tags = contact.Tags.Select(x => x.TagName).ToList(),
                CustomFields = contact.CustomFields.ToDictionary(x => x.Key, x => x.Value)
            };
        }

        public static Contact ToDomain(this DalContact dalContact)
        {
            return dalContact == null ? null : new Contact(
                new ContactId(dalContact.Id),
                dalContact.FirstName,
                dalContact.LastName,
                dalContact.OrganisationName,
                dalContact.WebsiteUrl,
                dalContact.Notes,
                dalContact.Phones.Select(x => new Phone(x)).ToList(),
                dalContact.Addresses.Select(x => new Address(x)).ToList(),
                dalContact.Emails.Select(x => new Email(x)).ToList(),
                dalContact.Categories.Select(x => new Category(x)).ToList(),
                dalContact.Tags.Select(x => new Tag(x)).ToList(),
                dalContact.CustomFields.Select(x => new CustomField(x.Key, x.Value)).ToList()
            );
        }
    }
}
