using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public int DataStatus { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
