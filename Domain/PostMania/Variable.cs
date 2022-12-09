using Newtonsoft.Json;

namespace Domains.PostMania
{
    public class Variable
    {
        [JsonProperty("colName")]
        public string ColName { get; set; }

        [JsonProperty("colVal")]
        public string ColVal { get; set; }
    }
}
