using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Rss
{
    public class Item
    {
        /// <summary>
        /// 【Title或Description必填】项目的名称。
        /// <example>RSS简介。</example>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 项目的Url。
        /// <example>http://www.bobopo.com/article/rss.htm</example>
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 【Title或Description必填】项目的摘要。
        /// <example>RSS（简易资讯聚合）是一种消息来源格式规范，用以发布经常更新资料的网站，例如部落格文章、新闻、音讯或视讯的网摘。RSS文件（或称做摘要、网络摘要、或频更新，提供到能道）包含了全文或是节录的文字，……</example>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 作者的Email地址。通常忽略。
        /// <example>rosbicn@hotmail.com</example>
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 频道所属的一个或几个类别。详见后文。
        /// <example>Html</example>
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 与此项目相关的评论的Url。
        /// <example>http://www.bobopo.com/comments/rss.htm</example>
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 此项目相关的多媒体附件。属性url表示附件网址，属性length表示附件字节数，属性type表示附件的MIME类型。
        /// <example><enclosure url="http://www.bobopo.com/video/rss.mp3" length="16131450" type="audio/mpeg" /></example>
        /// </summary>
        public string Enclosure { get; set; }

        /// <summary>
        /// 项目的唯一识别码。详见后文。
        /// <example>http://www.bobopo.com/article/rss.htm</example>
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 项目的发布日期。遵循RFC 822。
        /// <example>Wed, 04 Mar 2009 00:00:01 GMT</example>
        /// </summary>
        public string PubDate { get; set; }

        /// <summary>
        /// 项目来源于哪个Rss频道。如果一个Rss是从其他Rss转贴过来，可以用这个。必须包含属性url，指向另外一个rss。
        /// <example><source url="http://www.blabla.cn/rss.xml">Blabla</source></example>
        /// </summary>
        public string Source { get; set; }
    }
}

