using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository.Entity
{
    public class TalksEntity
    {
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public List<string> ImageList { get; set; }
    }
}
