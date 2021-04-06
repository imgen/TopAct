using LiteDB;
using System;
using System.Collections.Generic;
using TopAct.Common;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;
using TopAct.Domain.Rules;
using TopAct.Infrastructure.Dal;
using DalContact = TopAct.Infrastructure.Dal.Entities.Contact;

namespace TopAct.Domain.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DbContext _dbContext;

        public ContactRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Contact contact)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            collection.Insert(contact.ToDal());
        }

        public void AddAll(IList<Contact> contacts)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            var dalContacts = contacts.ToDals();
            collection.InsertBulk(dalContacts);
        }

        public Contact GetById(ContactId contactId)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            collection.EnsureIndex(x => x.Id);
            return collection.Query()
                .Where(x => x.Id == contactId.Value)
                .FirstOrDefault()
                .ToDomain();
        }

        public IList<Contact> GetAll(
            string name,
            string phone,
            string email,
            string websiteUrl,
            string notes,
            string category)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            var query = collection.Query();
            if (name.IsNullOrWhiteSpace() is false)
            {
                collection.EnsureIndex(x => x.FirstName);
                collection.EnsureIndex(x => x.LastName);
                collection.EnsureIndex(x => x.FullName);
                query = query.Where(x =>
                    x.FirstName.Contains(name) ||
                    x.LastName.Contains(name) ||
                    x.FullName.Contains(name)
                );
            }

            if (phone.IsNullOrWhiteSpace() is false &&
                phone.IsValidPhoneNo())
            {
                collection.EnsureIndex(x => x.PhoneNumbers);
                query = query.Where(x => x.PhoneNumbers.Contains(phone));
            }

            if (email.IsNullOrWhiteSpace() is false &&
                email.IsValidEmailAddress())
            {
                collection.EnsureIndex(x => x.Emails);
                query = query.Where(x => x.Emails.Contains(email));
            }

            if (websiteUrl.IsNullOrWhiteSpace() is false)
            {
                collection.EnsureIndex(x => x.WebsiteUrl);
                query = query.Where(x =>
                    x.WebsiteUrl.Contains(websiteUrl)
                );
            }

            if (notes.IsNullOrWhiteSpace() is false)
            {
                collection.EnsureIndex(x => x.Notes);
                query = query.Where(x =>
                    x.Notes.Contains(notes)
                );
            }

            if (category.IsNullOrWhiteSpace() is false)
            {
                collection.EnsureIndex(x => x.Categories);
                query = query.Where(x =>
                    x.Categories.Contains(category)
                );
            }

            return query
                .ToArray()
                .ToDomains();
        }

        public void Save(Contact contact)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            collection.Upsert(contact.ToDal());
        }

        public void Delete(Contact contact)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            var id = new BsonValue(contact.Id.Value);
            collection.Delete(id);
        }

        public IList<Contact> GetAllByIds(IList<Guid> contactIds)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            var query = collection.Query();
            query = query.Where(x => contactIds.Contains(x.Id));
            return query.ToArray().ToDomains();
        }

        public void UpsertAll(IList<Contact> importedContacts)
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            collection.Upsert(importedContacts.ToDals());
        }
    }
}
