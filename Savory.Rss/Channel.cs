using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Rss
{
    public class Channel
    {
        /// <summary>
        /// 【必须】频道名称
        /// <example>程序员的波波坡</example>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 【必须】与频道关联的Web站点或者站点区域的Ur
        /// <example>https://wwww.harriszhang.com</example>
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 【必须】简要介绍该频道是做什么的
        /// <example>包含编程、休闲、知识、杂记的程序员站点</example>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 频道内容使用的语言。详见常用HTML、RSS语言代码列表。
        /// <example>zh-cn</example>
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 频道内容的版权说明。
        /// <example>Copyright 2008,2009 bobopo.com</example>
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 责任编辑的Email地址。
        /// <example>rosbicn@hotmail.com</example>
        /// </summary>
        public string ManagingEditor { get; set; }

        /// <summary>
        /// 频道相关网站管理员的Email地址。
        /// <example>rosbicn@hotmail.com</example>
        /// </summary>
        public string WebMaster { get; set; }

        /// <summary>
        /// 频道内容发布日期。遵循RFC 822。
        /// <example>Wed, 04 Mar 2009 00:00:01 GMT</example>
        /// </summary>
        public string PubDate { get; set; }

        /// <summary>
        /// 频道内容最后的修改日期。遵循RFC 822。
        /// <example>Wed, 04 Mar 2009 09:42:31 GMT</example>
        /// </summary>
        public string LastBuildDate { get; set; }

        /// <summary>
        /// 频道所属的一个或几个类别。详见后文。
        /// <example>Html</example>
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 生成该频道的程序名字符串。
        /// <example>Bobopo Site Generator 2009</example>
        /// </summary>
        public string Generator { get; set; }

        /// <summary>
        /// 解释当前RSS文件的文档的Url。(给不知道啥是RSS的某人看:)
        /// <example>http://www.bobopo.com/code/rss.htm</example>
        /// </summary>
        public string Docs { get; set; }

        /// <summary>
        /// 允许进程注册为“cloud”，频道更新时通知它，为 RSS 提要实现了一种轻量级的发布-订阅协议。详见后文。
        /// <example>详见后文。</example>
        /// </summary>
        public string Cloud { get; set; }

        /// <summary>
        /// 内容有效期，一个数字，指明该频道可被缓存的最长分钟数。
        /// <example>60</example>
        /// </summary>
        public string TTL { get; set; }

        /// <summary>
        /// 指定一个 GIF或JPEG或PNG图片，用以与频道一起显示。详见后文。
        /// <example>详见后文。</example>
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 內容分级，主要指成人、限制、儿童等，多数情况不用，如果要用参见PICS。
        /// <example></example>
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// 定义可与频道一起显示的输入框。多数情况不用。详见后文。
        /// <example></example>
        /// </summary>
        public string TextInput { get; set; }

        /// <summary>
        /// 提示新闻聚合器，哪些小时时段它可以跳过。可包含最多24个<hour>子节点，它的值是0～23中的一个数字。
        /// <example><hour>2</hour><hour>3</hour></example>
        /// </summary>
        public string SkipHours { get; set; }

        /// <summary>
        /// 提示新闻聚合器，那些天它可以跳过。可包含最多7个<day>子节点，它的值是Monday、Tuesday、Wednesday、Thursday、Friday、Saturday或Sunday之一。
        /// <example><day>Saturday</day><day>Sunday</day></example>
        /// </summary>
        public string SkipDays { get; set; }
    }
}
