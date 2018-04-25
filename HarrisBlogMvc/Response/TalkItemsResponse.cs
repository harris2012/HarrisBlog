using HarrisBlogMvc.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Response
{
    public class TalkItemsResponse : ResponseBase
    {
        [JsonProperty("talkList")]
        public List<TalkVo> TalkList { get; set; }
    }
}