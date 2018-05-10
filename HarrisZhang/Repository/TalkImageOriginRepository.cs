using Repository.Entity;
using Savory.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Repository
{
    public class TalkImageOriginRepository : RepositoryBase
    {
        public List<TalkImageOriginEntity> GetTalkImageOriginEntityList()
        {
            return CacheManager.GetDataWithAbsoluteExpiration("cache_talk_image_origin", () =>
            {
                return GetEntityList<TalkImageOriginEntity>("select * from talk_image_origin");
            });
        }
    }
}