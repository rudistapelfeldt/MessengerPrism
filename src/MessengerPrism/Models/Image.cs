using System;
using Newtonsoft.Json;

namespace MessengerPrism.Models
{
    public class Image
    {
        [JsonProperty("mtype")]
        public string mtype { get; set; }

        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("ts")]
        public long Ts { get; set; }
    }
}
