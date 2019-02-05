using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class Token
    {
        [JsonProperty]
        public bool Autheticated { get; set; }

        [JsonProperty]
        public string Created { get; set; }

        [JsonProperty]
        public string Expiration { get; set; }

        [JsonProperty]
        public string AccessToken { get; set; }

        [JsonProperty]
        public string Message { get; set; }


    }
}
