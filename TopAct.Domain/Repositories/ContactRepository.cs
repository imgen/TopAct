using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TopAct.Domain.Contracts;
using TopAct.Domain.Entities;
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
            var dalContacts = contacts.Select(x => x.ToDal()).ToArray();
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

        public IList<Contact> GetAll()
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DalContact>();
            return collection.Query()
                .ToArray()
                .Select(x => x.ToDomain())
                .ToList();
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
    }
}
