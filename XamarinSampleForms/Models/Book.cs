using Newtonsoft.Json;
using Kinvey;

namespace XamarinSampleForms.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Book : Kinvey.Entity
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set;}

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("year")]
        public short Year { get; set; }

        [JsonProperty("pages")]
        public ushort Pages { get; set; }
    }
}