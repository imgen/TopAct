using LiteDB;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TopAct.Infrastructure.Dal.Entities;

namespace TopAct.Infrastructure.Dal
{
    public class DbMigrator
    {
        private readonly DbContext _dbContext;

        public DbMigrator(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public const int CurrentAppVersion = 3;

        private static MethodInfo GetMigrationMethod(int version)
        {
            var migrationMethodName = $"MigrateDbToVersion{version}";
            var migrationMethod = typeof(DbMigrator).GetMethod(
                    migrationMethodName,
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.Static
                );
            return migrationMethod ?? throw new Exception($"Cannot find the DB migration method {migrationMethodName}");
        }

        public void Migrate()
        {
            using var db = _dbContext.GetDatabase();
            var collection = db.GetCollection<DbVersion>();
            var dbVersion = collection.Query().ToArray().FirstOrDefault();
            int currentDbVersion = dbVersion is null ? 1 : dbVersion.Version;
            if (currentDbVersion <= 0)
            {
                currentDbVersion = 1;
            }
            if (currentDbVersion < CurrentAppVersion)
            {
                var migrationMethod = GetMigrationMethod(CurrentAppVersion);
                var parameters = new object[] { db, currentDbVersion };
                migrationMethod.Invoke(this, parameters);
                collection.Upsert(new DbVersion { Id = 1, Version = CurrentAppVersion });
            }
        }

        private static void InvokePreviousMigrationIfNecessary(
            LiteDatabase db,
            int currentDbVersion,
            [CallerMemberName] string callingMethodName = null
        )
        {
            var callerVersion = int.Parse(callingMethodName.Where(char.IsDigit).ToArray());
            int callerVersionMinusOne = callerVersion - 1;
            if (callerVersionMinusOne > currentDbVersion)
            {
                var method = GetMigrationMethod(callerVersionMinusOne);
                method.Invoke(null, new object[] { db, currentDbVersion });
            }
        }

        private static void MigrateDbToVersion2(LiteDatabase db, int _)
        {
            var collection = db.GetCollection<Contact>();
            var contacts = collection.Query().ToArray();
            foreach (var contact in contacts)
            {
                contact.FullName = $"{contact.FirstName} {contact.LastName}";
            }

            collection.Update(contacts);
        }

        private static void MigrateDbToVersion3(LiteDatabase db, int currentDbVersion)
        {
            InvokePreviousMigrationIfNecessary(db, currentDbVersion);
            var collection = db.GetCollection<Contact>();
            var contacts = collection.Query().ToArray();

            collection.Update(contacts);
        }
    }
}
