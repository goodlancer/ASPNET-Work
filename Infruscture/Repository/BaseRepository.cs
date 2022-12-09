using Domains.DBModels;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoScolptioDBContext _mongoContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public BaseRepository(IMongoScolptioDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>($"{typeof(TEntity).Name}");
        }



        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> criteria)
        {
            //var filter = Builders<Properties>.Filter.Where(voteCollection => voteCollection.OrgId == "d86ba48d-5a1b-4aae-b345-bc4a437cf0b0");
            //var update = Builders<Properties>.Update.Set(voteCollection => voteCollection.ImportedTime, DateTime.Today.AddDays(-1));
            //_mongoContext.GetCollection<Properties>("Properties").UpdateMany(filter, update);
            var all = await _dbCollection.FindAsync(criteria);
            return all.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> criteria)
        {
            //var filter = Builders<Properties>.Filter.Where(voteCollection => voteCollection.OrgId == "d86ba48d-5a1b-4aae-b345-bc4a437cf0b0");
            //var update = Builders<Properties>.Update.Set(voteCollection => voteCollection.ImportedTime, DateTime.Today.AddDays(-1));
            //_mongoContext.GetCollection<Properties>("Properties").UpdateMany(filter, update);
            var all =  _dbCollection.Find(criteria);
            return all.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithPagingAsync(Expression<Func<TEntity, bool>> criteria, int pageNumber = 1, int pageSize = 10)
        {
            var all = await _dbCollection.Find(criteria).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
            return all;
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> criteria)
        {
            var all = await _dbCollection.FindAsync(criteria);
            return all.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public async Task<string> Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            if (obj.Id == null)
            {
                obj.Id = Guid.NewGuid().ToString();
            }
            await _dbCollection.InsertOneAsync(obj);
            return obj.Id;
        }

        public async Task<bool> UpdateAsync(TEntity obj)
        {
            var result = await _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj);
            return result.IsAcknowledged;
        }

        public async Task Delete(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }

        public async Task DeleteAllAsync(Expression<Func<TEntity, bool>> criteria)
        {
            await _dbCollection.DeleteManyAsync(criteria);
        }

        public long GetTotalCount(Expression<Func<TEntity, bool>> criteria)
        {
            var result = _dbCollection.Find(criteria);
            return result.CountDocuments();
        }

    }
}
