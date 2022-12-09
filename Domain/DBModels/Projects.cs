using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class Projects : BaseEntity
    {
        public string ProjectName { get; set; }
        public string ClientId { get; set; }
        public string TeamId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Deadline { get; set; }
        public Decimal Cost { get; set; }
        public string Status { get; set; }
        public string OrgId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> DesignerIds { get; set; }
    }
}
