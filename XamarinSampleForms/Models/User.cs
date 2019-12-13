using Newtonsoft.Json;

namespace XamarinSampleForms.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User : Kinvey.Entity
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}

