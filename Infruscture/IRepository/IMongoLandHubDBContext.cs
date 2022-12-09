using MongoDB.Driver;

namespace Services.Repository
{
    public interface IMongoScolptioDBContext
    {
        IMongoCollection<User> GetCollection<User>(string name);
    }
}
