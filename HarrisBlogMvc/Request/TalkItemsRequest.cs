using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class TalkItemsRequest : TalkCountRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}