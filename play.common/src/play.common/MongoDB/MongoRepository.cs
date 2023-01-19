using System.Linq.Expressions;
using MongoDB.Driver;

namespace play.common.MongoDB
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        // private const string collectionName = "items"; // A group of objects in MongoDB simliar to RDBMS where you have a table 
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }
        public async Task<IReadOnlyCollection<T>> GetAllItemsAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }
        public async Task<IReadOnlyCollection<T>> GetAllItemsAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).ToListAsync();
        }
        public async Task<T> GetItemsAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<T> GetItemsAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity)); // What to do if you don't want to throw an exception
            }
            await dbCollection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity)); // What to do if you don't want to throw an exception
            }
            FilterDefinition<T> filter = filterBuilder.Eq(exisitingEntity => exisitingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(exisitingEntity => exisitingEntity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }

    }
}