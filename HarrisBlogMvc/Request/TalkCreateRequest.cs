using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class TalkCreateRequest
    {
        public string Body { get; set; }

        public string Location { get; set; }

        public string LocationName { get; set; }

        public DateTime PublishTime { get; set; }
    }
}