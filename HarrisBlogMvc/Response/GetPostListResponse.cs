using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class GetPostListResponse : ResponseBase
    {
        [JsonProperty("posts")]
        public List<PostVo> PostList { get; set; }
    }
}