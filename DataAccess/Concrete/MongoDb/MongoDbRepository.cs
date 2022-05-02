using Core.DataAccess.MongoDb;
using Core.Entities.Abstract;
using DataAccess.Concrete.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class MongoDbRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new MongoDbContext())
            {
                var collection = context.GetCollection<TEntity>();
                collection.InsertOneAsync(entity);
            }
        }

        public void DeleteById(string id)
        {
            using (var context = new MongoDbContext())
            {
                var collection = context.GetCollection<TEntity>();
                var objectId = ObjectId.Parse(id);
                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                collection.FindOneAndDelete(filter);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new MongoDbContext())
            {
                var collection = context.GetCollection<TEntity>();
                return collection.Find(filter).SingleOrDefault();
            }
        }

        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new MongoDbContext())
            {
                var collection = context.GetCollection<TEntity>();
                return filter==null ? collection.AsQueryable().ToList() : 
                    collection.AsQueryable().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity, string id)
        {
            using (var context = new MongoDbContext())
            {
                var collection = context.GetCollection<TEntity>();
                var objectId = ObjectId.Parse(id);
                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                collection.ReplaceOne(filter, entity);
            }
        }
    }
}
