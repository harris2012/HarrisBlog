using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class TalkCountRequest
    {
        public int CategoryId { get; set; }

        public int DataStatus { get; set; }
    }
}