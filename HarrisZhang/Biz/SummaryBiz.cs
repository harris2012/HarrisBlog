using HarrisZhang.Repository;
using HarrisZhang.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Biz
{
    public class SummaryBiz
    {
        public List<SummaryVo> GetSummaryList()
        {
            List<SummaryVo> returnValue = new List<SummaryVo>();

            PostRepository postRepository = new PostRepository();
            var postEntityList = postRepository.GetPostEntityList();
            foreach (var postEntity in postEntityList)
            {
                SummaryVo summary = new SummaryVo();

                summary.ItemType = 1;
                summary.ItemId = postEntity.Id;
                summary.Ename = postEntity.Ename;
                summary.Title = postEntity.Title;
                summary.Body = postEntity.Summary;
                summary.PublishTime = postEntity.PublishTime;

                returnValue.Add(summary);
            }

            TalkRepository talkRepository = new TalkRepository();
            var talkEntityList = talkRepository.GetTalkEntityListData();
            TalkImageRelationRepository talkImageRelationRepository = new TalkImageRelationRepository();
            var talkImageRelationEntityList = talkImageRelationRepository.GetTalkImageRelationEntityList();
            TalkImageOriginRepository talkImageOriginRepository = new TalkImageOriginRepository();
            var talkImageOriginEntityList = talkImageOriginRepository.GetTalkImageOriginEntityList();

            foreach (var talkEntity in talkEntityList)
            {
                SummaryVo summary = new SummaryVo();

                summary.ItemType = 2;
                summary.ItemId = talkEntity.Id;
                summary.Body = talkEntity.Body;
                summary.PublishTime = talkEntity.PublishTime;

                summary.ImageList = (from talkImageRelation in talkImageRelationEntityList
                                     join talkImageOrigin in talkImageOriginEntityList on talkImageRelation.ImageId equals talkImageOrigin.ImageId
                                     where talkImageRelation.TalkId == talkEntity.TalkId
                                     select new ImageVo { ImageUrl = talkImageOrigin.RemoteFilePath, Width = talkImageOrigin.Width, Height = talkImageOrigin.Height }).ToList();

                returnValue.Add(summary);
            }

            return returnValue;
        }
    }
}