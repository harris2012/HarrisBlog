using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class TalkCountResponse : ResponseBase
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}