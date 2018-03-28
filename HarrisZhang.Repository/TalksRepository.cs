using HarrisZhang.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class TalksRepository : RepositoryBase<List<TalksEntity>>
    {
        protected override string FilePath
        {
            get
            {
                return "~/Data/talks.xml";
            }
        }
    }
}
