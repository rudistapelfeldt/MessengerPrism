using System;
using Newtonsoft.Json;

namespace MessengerPrism.Models
{
    public class Text
    {
        [JsonProperty("mtype")]
        public string Mtype { get; set; }

        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ts")]
        public long Ts { get; set; }
    }
}
