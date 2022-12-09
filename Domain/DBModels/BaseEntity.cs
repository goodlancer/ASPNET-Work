using MongoDB.Bson.Serialization.Attributes;

namespace Domains.DBModels
{
    public class BaseEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}
