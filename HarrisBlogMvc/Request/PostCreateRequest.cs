using HarrisBlogMvc.Response;
using HarrisBlogMvc.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class PostCreateRequest
    {
        /// <summary>
        /// 文章的英文名
        /// </summary>
        public string Ename { get; set; }

        /// <summary>
        /// 文章的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章的markdown主题
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime PublishTime { get; set; }
    }
}