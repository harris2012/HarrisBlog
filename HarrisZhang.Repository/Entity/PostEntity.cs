using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository.Entity
{
    public class PostEntity
    {
        public string Ename { get; set; }

        public string Title { get; set; }

        public DateTime PublishTime { get; set; }

        public string Summary { get; set; }

        /// <summary>
        /// 1.html
        /// 2.markdown
        /// </summary>
        public int PostStyle { get; set; }

        public string Body { get; set; }

        public string HtmlBody { get; set; }
    }
}
