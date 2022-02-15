using System;
using Newtonsoft.Json;

namespace MessengerPrism.Models
{
    public class Group
    {
        [JsonProperty("mtype")]
        public string Mtype { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }
    }
}
