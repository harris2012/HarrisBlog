using Repository.Entity;
using Savory.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Repository
{
    public class TalkImageRelationRepository : RepositoryBase
    {
        public List<TalkImageRelationEntity> GetTalkImageRelationEntityList()
        {
            return CacheManager.GetDataWithAbsoluteExpiration("cache_talk_image_relation", () =>
            {
                return GetEntityList<TalkImageRelationEntity>("select * from talk_image_relation");
            });
        }
    }
}