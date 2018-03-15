using HarrisZhang.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class PostsRepository : RepositoryBase<List<PostsEntity>>
    {
        protected override string FilePath
        {
            get
            {
                return "~/Data/posts.xml";
            }
        }
    }
}
