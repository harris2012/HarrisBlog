using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Vo
{
    public class TalkVo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("talkId")]
        public string TalkId { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("postName")]
        public string PosName { get; set; }

        [JsonProperty("postX")]
        public string PosX { get; set; }

        [JsonProperty("postY")]
        public string PosY { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("dataStatus")]
        public int DataStatus { get; set; }

        [JsonProperty("imageIdList")]
        public List<string> ImageIdList { get; set; }
    }
}