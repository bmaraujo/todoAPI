using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class TokenConfigurations
    {
        [JsonProperty]
        public string Audience { get; set; }

        [JsonProperty]
        public string Issuer { get; set; }

        [JsonProperty]
        public int Seconds { get; set; }
    }
}
