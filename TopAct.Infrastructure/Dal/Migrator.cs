using LiteDB;
using System;
using System.Linq;
using TopAct.Infrastructure.Dal.Entities;

namespace TopAct.Infrastructure.Dal
{
    public class Migrator
    {
        private readonly DbContext _dbContext;

        public Migrator(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public const int CurrentAppVersion = 2;

        public void Migrate()
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DbVersion>();
            var dbVersion = collection.Query().ToArray().FirstOrDefault();
            int currentDbVersion = dbVersion is null ? 1 : dbVersion.Id;
            if (currentDbVersion < CurrentAppVersion)
            {
                var newDbVersion = CurrentAppVersion switch
                {
                    2 => MigrateToVersion2(db, currentDbVersion),
                    _ => throw new NotImplementedException()
                };
                collection.DeleteAll();
                collection.Insert(newDbVersion);
            }
        }

        private static DbVersion MigrateToVersion2(LiteDatabase db, int _)
        {
            var collection = db.GetCollection<Contact>();
            var contacts = collection.Query().ToArray();
            foreach (var contact in contacts)
            {
                contact.FullName = $"{contact.FirstName} {contact.LastName}";
            }

            collection.Update(contacts);

            return new DbVersion
            {
                Id = 2
            };
        }
    }
}
