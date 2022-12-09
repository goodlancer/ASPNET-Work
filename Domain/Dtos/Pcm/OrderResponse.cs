using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domains.Dtos.Pcm
{
    public class OrderResponse
    {
        [JsonProperty("batchID")]
        public int? BatchID { get; set; }
        [JsonProperty("successfulOrders")]
        public List<OrderResult> SuccessfulOrders { get; set; }
        [JsonProperty("failedOrders")]
        public List<OrderResult> FailedOrders { get; set; }

    }

    public class OrderResult
    {
        [JsonProperty("orderID")]
        public int? OrderID { get; set; }
        [JsonProperty("orderIndex")]
        public int OrderIndex { get; set; }
        [JsonProperty("errors")]
        public List<ErrMsg> Errors { get; set; }
        [JsonProperty("successfulRecipientCount")]
        public int SuccessfulRecipientCount { get; set; }
        [JsonProperty("failedRecipientCount")]
        public int FailedRecipientCount { get; set; }
        [JsonProperty("failedRecipientList")]
        public List<FailedRecipient> FailedRecipientList { get; set; }
    }

    public class FailedRecipient
    {
        [JsonProperty("recipientIndex")]
        public int? RecipientIndex { get; set; }
        [JsonProperty("extRefNbr")]
        public string ExtRefNbr { get; set; }
        [JsonProperty("missingFields")]
        public List<string> MissingFields { get; set; }
    }

    public class ErrMsg
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
