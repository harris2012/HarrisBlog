using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    public class TalkEntity
    {
        public int Id { get; set; }

        public string TalkId { get; set; }

        public int Category { get; set; }

        public string MsgContent { get; set; }

        public string PosName { get; set; }

        public string PosX { get; set; }

        public string PosY { get; set; }

        public DateTime CreateTime { get; set; }

        public int DataStatus { get; set; }
    }
}
