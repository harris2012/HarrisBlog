using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class CreatePostRequest
    {

        public string Title { get; set; }

        public string Ename { get; set; }

        public string Body { get; set; }

        public string HtmlBody { get; set; }
    }
}