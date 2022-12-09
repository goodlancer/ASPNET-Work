using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domains.Dtos.Pcm
{
    public class Order
    {

        [JsonProperty("extRefNbr")]
        public string ExtRefNbr { get; set; }
        [JsonProperty("orderConfig")]
        public OrderConfig OrderConfig { get; set; }
        [JsonProperty("recipientList")]
        public List<Recipient> RecipientList { get; set; }
    }

    public class Recipient
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
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
        [JsonProperty("extRefNbr")]
        public string ExtRefNbr { get; set; }
        [JsonProperty("recipientDesignVariables")]
        public List<KeyValuePair<string, string>> RecipientDesignVariables { get; set; }
    }

    public class OrderConfig
    {
        [JsonProperty("designId")]
        public int DesignId { get; set; }
        [JsonProperty("mailClass")]
        public string MailClass { get; set; }
        [JsonProperty("mailDate")]
        public string MailDate { get; set; }
        [JsonProperty("mailTrackingEnabled")]
        public bool MailTrackingEnabled { get; set; }
        [JsonProperty("globalDesignVariables")]
        public List<KeyValuePair<string, string>> GlobalDesignVariables { get; set; }
        [JsonProperty("letterConfig")]
        public LetterConfig LetterConfig { get; set; }
    }

    public class LetterConfig
    {
        [JsonProperty("color")]
        public bool Color { get; set; }
        [JsonProperty("printOnBothSides")]
        public bool PrintOnBothSides { get; set; }
        [JsonProperty("insertAddressingPage")]
        public bool InsertAddressingPage { get; set; }
        [JsonProperty("envelopeType")]
        public string EnvelopeType { get; set; }
    }
}
