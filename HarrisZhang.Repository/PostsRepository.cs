using HarrisZhang.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class PostsRepository : RepositoryBase<List<TheEntity>>
    {
        protected override string FilePath
        {
            get
            {
                return "~/App_Data/posts.xml";
            }
        }
    }
}
