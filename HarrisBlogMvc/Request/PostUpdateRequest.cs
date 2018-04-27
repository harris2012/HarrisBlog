using HarrisBlogMvc.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Request
{
    public class PostUpdateRequest
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

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
        /// 版本号
        /// </summary>
        public int Version { get; set; }
    }
}