using HarrisBlogMvc.Response;
using HarrisBlogMvc.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class CreateBlogRequest
    {

        public BlogVo Blog { get; set; }

        public int Version { get; set; }
    }
}