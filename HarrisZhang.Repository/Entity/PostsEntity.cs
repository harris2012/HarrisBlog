using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository.Entity
{
    public class PostsEntity
    {
        public string Ename { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime PublishTime { get; set; }
    }
}
