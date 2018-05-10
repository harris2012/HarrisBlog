using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Vo
{
    public class PostVo
    {
        public string Ename { get; set; }

        public string Title { get; set; }

        public string CoverImgUrl { get; set; }

        public int PostType { get; set; }

        public string HtmlBody { get; set; }

        public int CategoryId { get; set; }

        public DateTime? PublishTime { get; set; }
    }
}