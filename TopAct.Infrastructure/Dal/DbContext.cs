using LiteDB;

namespace TopAct.Infrastructure.Dal
{
    public class DbContext
    {
        private readonly string _dbFilePath;

        public DbContext(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        public LiteDatabase GetDatabase() => new(_dbFilePath);
    }
}
