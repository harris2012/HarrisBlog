using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class PostCountResponse : ResponseBase
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}