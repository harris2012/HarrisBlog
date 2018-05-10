using Repository.Entity;
using Savory.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class PostRepository : RepositoryBase
    {
        public List<PostEntity> GetPostEntityList()
        {
            return CacheManager.GetDataWithAbsoluteExpiration("cache_post", () =>
            {
                return GetEntityList<PostEntity>("select * from post where datastatus = 1");
            });
        }
    }
}
