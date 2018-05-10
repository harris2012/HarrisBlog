using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class TalkImageRelationEntity
    {
        public int Id { get; set; }

        public string TalkId { get; set; }

        public string ImageId { get; set; }
    }
}
