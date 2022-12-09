using Newtonsoft.Json;

using System.Collections.Generic;

namespace Domains.PostMania
{
    public class RecipientListRecord
    {
        [JsonProperty("recordID")]
        public int RecordID { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("extRefNbr")]
        public string ExtRefNbr { get; set; }

        [JsonProperty("undeliverable")]
        public bool Undeliverable { get; set; }

        [JsonProperty("recipientDesignVariables")]
        public List<Variable> Variables { get; set; }
    }
}
