using Repository.Entity;
using Savory.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class TalkRepository : RepositoryBase
    {
        public List<TalkEntity> GetTalkEntityListData()
        {
            return CacheManager.GetDataWithAbsoluteExpiration("cache_talk", () =>
            {
                return GetEntityList<TalkEntity>("select * from talk where datastatus = 1");
            });
        }
    }
}
