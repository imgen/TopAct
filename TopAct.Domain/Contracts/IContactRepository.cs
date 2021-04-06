using System;
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
            string name = null,
            string phone = null,
            string email = null,
            string websiteUrl = null,
            string notes = null,
            string category = null
        );
        void Save(Contact contact);
        void Delete(Contact contact);
        IList<Contact> GetAllByIds(IList<Guid> contactIds);
        void UpsertAll(IList<Contact> importedContacts);
    }
}
