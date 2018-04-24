using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisBlogMvc.Vo
{
    public class BlogVo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 文章的英文名
        /// </summary>
        [JsonProperty("ename")]
        public string Ename { get; set; }

        /// <summary>
        /// 文章的标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 文章的markdown主题
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        [JsonProperty("publishTime")]
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 创建记录的时间
        /// </summary>
        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后一次更新记录的时间
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}