using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class PostVo
    {
        [JsonProperty("ename")]
        public string Ename { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}