using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domains.DBModels
{
    public class Income : BaseEntity
    {
        public string OrgId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; set; }
        public string Status { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }
    }
}
