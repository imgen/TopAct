using System.Collections.Generic;
using TopAct.Domain.Entities;

namespace TopAct.Domain.Repositories
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        void AddAll(IList<Contact> contacts);
        Contact GetById(ContactId contactId);
        IList<Contact> GetAll();
    }
}
