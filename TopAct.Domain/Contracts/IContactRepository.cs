using System.Collections.Generic;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Contracts
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        void AddAll(IList<Contact> contacts);
        Contact GetById(ContactId contactId);
        IList<Contact> GetAll(
            string name,
            string phone,
            string email,
            string websiteUrl,
            string notes,
            string category
        );
        void Save(Contact contact);
        void Delete(Contact contact);
    }
}
