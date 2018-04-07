using HarrisBlogMvc.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class GetBlogResponse : ResponseBase
    {
        [JsonProperty("blog")]
        public BlogVo Blog { get; set; }
    }
}