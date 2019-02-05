using Newtonsoft.Json;

namespace TodoApi
{
    public class Settings
    {
        [JsonProperty]
        public string ConnectionString { get; set; }

        [JsonProperty]
        public string TestUserId { get; set; }

        [JsonProperty]
        public string TestAccessKey { get; set; }
    }
}
