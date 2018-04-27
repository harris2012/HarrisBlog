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

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }


        [JsonProperty("publishTime")]
        public DateTime PublishTime { get; set; }

        [JsonProperty("imageIdList")]
        public List<string> ImageIdList { get; set; }


        [JsonProperty("dataStatus")]
        public int DataStatus { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}