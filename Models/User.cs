using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class User
    {
        [JsonProperty]
        public string UserId { get; set; }

        [JsonProperty]
        public string AccessKey { get; set; }
    }
}
