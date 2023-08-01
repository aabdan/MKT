using Newtonsoft.Json;

namespace MKT.Website.UI.Models
{
    public class PersonSchema
    {
        [JsonProperty("@context")]
        public string Context { get; set; } = "https://schema.org";

        [JsonProperty("@type")]
        public string Type { get; set; } = "WebSite";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        // Add more properties as needed
    }

}
