﻿using HarrisBlogMvc.Request;
using HarrisBlogMvc.Response;
using HarrisBlogMvc.Vo;
using Repository.Entity;
using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HarrisBlogMvc.Controllers
{
    public class TalkController : ApiController
    {
        [HttpPost]
        public TalkItemsResponse Items(TalkItemsRequest request)
        {
            TalkItemsResponse response = new TalkItemsResponse();

            int pageIndex = request.PageIndex > 0 ? request.PageIndex : 1;
            int pageSize = request.PageSize > 0 ? request.PageSize : 10;

            List<TalkVo> talkList = new List<TalkVo>();
            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "select * from talk where (@CategoryId = 0 or Category = @CategoryId) and (@DataStatus = 0 or DataStatus = @DataStatus) order by CreateTime desc limit @Offset, @Size";

                var entityList = sqliteConn.Query<TalkEntity>(sql, new { Offset = (pageIndex - 1) * pageSize, Size = pageSize, CategoryId = request.CategoryId, DataStatus = request.DataStatus }).ToList();
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var entity in entityList)
                    {
                        var vo = ToVo(entity);
                        talkList.Add(vo);
                    }
                }
            }
            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "select * from talk_image_relation where TalkId In @TalkIdList";
                var talkIdList = talkList.Select(v => v.TalkId).ToList();

                var entityList = sqliteConn.Query<TalkImageRelationEntity>(sql, new { TalkIdList = talkIdList }).ToList();
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var item in talkList)
                    {
                        item.ImageIdList = entityList.Where(v => v.TalkId == item.TalkId).Select(v => v.ImageId).ToList();
                    }
                }
            }

            response.TalkList = talkList;

            response.Status = 1;

            return response;
        }

        [HttpPost]
        public TalkCountResponse Count(TalkCountRequest request)
        {
            TalkCountResponse response = new TalkCountResponse();

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "select count(1) from talk where (@CategoryId = 0 or Category = @CategoryId) and (@DataStatus = 0 or DataStatus = @DataStatus)";

                response.TotalCount = sqliteConn.QuerySingle<int>(sql, new { CategoryId = request.CategoryId, DataStatus = request.DataStatus });
            }

            response.Status = 1;

            return response;
        }

        [HttpPost]
        public TalkDeleteResponse Delete(TalkDeleteRequest request)
        {
            TalkDeleteResponse response = new TalkDeleteResponse();

            TalkEntity entity = null;
            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "select * from talk where Id = @ID";

                entity = sqliteConn.QueryFirstOrDefault<TalkEntity>(sql, new { Id = request.Id });
            }

            if (entity == null)
            {
                response.Status = 404;
                response.Message = "说说不存在";
                return response;
            }

            if (entity.DataStatus == 2)
            {
                response.Status = 302;
                response.Message = "无法重复删除说说";
                return response;
            }

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "update talk set DataStatus = 2, LastUpdateTime = @LastUpdateTime where Id = @Id";

                sqliteConn.Execute(sql, new { Id = request.Id, LastUpdateTime = DateTime.Now });
            }

            response.Status = 1;
            return response;
        }

        [HttpPost]
        public TalkCreateResponse Create(TalkCreateRequest request)
        {
            TalkCreateResponse response = new TalkCreateResponse();

            TalkEntity entity = new TalkEntity();
            entity.TalkId = Guid.NewGuid().ToString("N").ToLower();
            entity.Category = 999;
            entity.Body = request.Body;
            entity.Location = request.Location;
            entity.LocationName = request.LocationName;
            entity.PublishTime = request.PublishTime;

            entity.DataStatus = 1;
            entity.CreateTime = DateTime.Now;
            entity.LastUpdateTime = DateTime.Now;

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "insert into talk(TalkId, Category, Body, Location, LocationName, PublishTime, DataStatus, CreateTime, LastUpdateTime) values (@TalkId, @Category, @Body, @Location, @LocationName, @PublishTime, @DataStatus, @CreateTime, @LastUpdateTime);";

                sqliteConn.Execute(sql, entity);
            }

            response.Status = 1;
            return response;
        }

        [HttpPost]
        public TalkItemResponse Item(TalkItemRequest request)
        {
            TalkItemResponse response = new TalkItemResponse();

            var sql = "select * from talk where Id=@Id";

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var postEntity = sqliteConn.QueryFirstOrDefault<TalkEntity>(sql, new { Id = request.Id });
                if (postEntity == null)
                {
                    response.Status = 404;
                    return response;
                }

                response.Talk = ToVo(postEntity);
            }

            response.Status = 1;
            return response;
        }

        [HttpPost]
        public TalkUpdateResponse Update(TalkUpdateRequest request)
        {
            TalkUpdateResponse response = new TalkUpdateResponse();

            var sql = "update talk set Body = @Body where Id = @Id";

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                sqliteConn.Execute(sql, new { Id = request.Id, Body = request.Body });
            }

            response.Status = 1;
            return response;
        }

        private static TalkVo ToVo(TalkEntity entity)
        {
            TalkVo returnValue = new TalkVo();

            returnValue.Id = entity.Id;
            returnValue.TalkId = entity.TalkId;
            returnValue.Category = entity.Category;
            returnValue.Body = entity.Body;
            returnValue.Location = entity.Location;
            returnValue.LocationName = entity.LocationName;
            returnValue.PublishTime = entity.PublishTime.Value;

            returnValue.DataStatus = entity.DataStatus;
            returnValue.CreateTime = entity.CreateTime.Value;
            returnValue.LastUpdateTime = entity.LastUpdateTime.Value;

            return returnValue;
        }
    }
}
