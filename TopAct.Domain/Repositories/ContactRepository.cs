﻿using System;
using TopAct.Domain.Entities;
using TopAct.Infrastructure.Dal;
using TopAct.Infrastructure.Dal.Entities;

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
    }
}
