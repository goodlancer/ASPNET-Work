using Newtonsoft.Json;

using System;

namespace Domains.ConfigSetting
{
    public class LoginResponse
    {
        [JsonProperty("token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires")]
        public DateTime Expire { get; set; }

        [JsonProperty("refreshtoken")]
        public string RefreshToken { get; set; }
    }
}
