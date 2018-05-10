using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Vo
{
    public class SummaryVo
    {
        /// <summary>
        /// 1. 日志
        /// 2. 说说
        /// </summary>
        public int ItemType { get; set; }

        public int ItemId { get; set; }

        public string Ename { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime? PublishTime { get; set; }

        public List<ImageVo> ImageList { get; set; }
    }
}