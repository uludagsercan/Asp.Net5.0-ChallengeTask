using System;
using MongoDB.Driver;
namespace DataAccess.Concrete.Context
{
    public class MongoDbContext:IDisposable
    {
        private const string _connectionString = "mongodb://localhost:27017";
        private const string _databaseName = "challenge";
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient(_connectionString);
            _database = client.GetDatabase(_databaseName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.Trim());
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
