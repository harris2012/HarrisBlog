using HarrisBlogMvc.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class PostItemsResponse : ResponseBase
    {
        [JsonProperty("posts")]
        public List<BlogVo> PostList { get; set; }
    }
}