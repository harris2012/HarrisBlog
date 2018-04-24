using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    public class PostEntity
    {
        public int Id { get; set; }

        public string Ename { get; set; }

        public string Title { get; set; }

        public string CoverImgUrl { get; set; }

        public int PostType { get; set; }

        public string MarkdownBody { get; set; }

        public string Summary { get; set; }

        public int CategoryId { get; set; }

        public System.Nullable<System.DateTime> PublishTime { get; set; }

        public int DataStatus { get; set; }

        public System.Nullable<System.DateTime> CreateTime { get; set; }

        public System.Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
