using HarrisBlogMvc.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class TalkItemResponse : ResponseBase
    {
        [JsonProperty("talk")]
        public TalkVo Talk { get; set; }
    }
}