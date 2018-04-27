using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    public class PostEntity : EntityBase
    {

        public string Ename { get; set; }

        public string Title { get; set; }

        public string CoverImgUrl { get; set; }

        public int PostType { get; set; }

        public string Body { get; set; }

        public string Summary { get; set; }

        public int CategoryId { get; set; }

        public DateTime? PublishTime { get; set; }
    }
}
