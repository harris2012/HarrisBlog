using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    public class TalkEntity : EntityBase
    {
        public string TalkId { get; set; }

        public int Category { get; set; }

        public string Body { get; set; }

        public string Location { get; set; }

        public string LocationName { get; set; }

        public DateTime? PublishTime { get; set; }


    }
}
