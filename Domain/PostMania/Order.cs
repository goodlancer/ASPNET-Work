using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Domains.PostMania
{
    public class Order
    {
        [JsonProperty("orderID")]
        public int OrderID { get; set; }

        [JsonProperty("batchID")]
        public int BatchID { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("designID")]
        public int DesignID { get; set; }

        [JsonProperty("mailClass")]
        public string MailClass { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("orderDate")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OrderDate { get; set; }

        [JsonProperty("recipients")]
        public List<RecipientListRecord> RecipientList { get; set; }

        [JsonProperty("design")]
        public object Design { get; set; }
    }
}
