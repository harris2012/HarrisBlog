using HarrisBlog.Repository;
using HarrisBlog.Repository.Entity;
using HarrisBlogMvc.Request;
using HarrisBlogMvc.Response;
using HarrisBlogMvc.Vo;
using HeyRed.MarkdownSharp;
using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace HarrisBlogMvc.Controllers
{
    public class PostController : ApiController
    {
        [HttpPost]
        public PostItemsResponse Items(PostItemsRequest request)
        {
            PostItemsResponse response = new PostItemsResponse();

            int pageIndex = request.PageIndex > 0 ? request.PageIndex : 1;
            int pageSize = request.PageSize > 0 ? request.PageSize : 10;

            var sql = "select * from post where datastatus=1 order by createtime desc limit @Offset, @Size";

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var entityList = sqliteConn.Query<PostEntity>(sql, new { Offset = (pageIndex - 1) * pageSize, Size = pageSize }).ToList();
                if (entityList != null && entityList.Count > 0)
                {
                    List<BlogVo> blogList = new List<BlogVo>();
                    foreach (var entity in entityList)
                    {
                        blogList.Add(ToVo(entity));
                    }
                    response.PostList = blogList;
                }
            }

            response.Status = 1;

            return response;
        }

        [HttpPost]
        public PostCountResponse Count(PostCountRequest request)
        {
            PostCountResponse response = new PostCountResponse();

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var sql = "select count(1) from post where datastatus=1 order by createtime desc";

                response.Count = sqliteConn.QuerySingle<int>(sql);
            }

            response.Status = 1;

            return response;
        }

        // GET /api/blog/id
        [HttpPost]
        public PostItemResponse Item(PostItemRequest request)
        {
            PostItemResponse response = new PostItemResponse();

            var sql = "select * from post where Id=@Id";

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                var postEntity = sqliteConn.QueryFirstOrDefault<PostEntity>(sql, new { Id = request.Id });
                if (postEntity == null)
                {
                    response.Status = 404;
                    return response;
                }

                response.Blog = ToVo(postEntity);
            }

            return response;
        }

        private static BlogVo ToVo(PostEntity v)
        {
            return new BlogVo
            {
                Id = v.Id,
                Ename = v.Ename,
                Title = v.Title,
                Body = v.MarkdownBody,
                CreateTime = v.CreateTime.Value,
                LastUpdateTime = v.CreateTime.Value
            };
        }

        // POST /api/blog
        [HttpPost]
        public CreateBlogResponse Create([FromBody]CreateBlogRequest request)
        {
            if (request.Blog == null)
            {
                return new CreateBlogResponse { Status = 1002, Message = "blog is null" };
            }

            var blog = request.Blog;

            PostEntity post = new PostEntity();
            post.Title = blog.Title;
            post.Ename = blog.Ename.Replace(" ", "-").ToLower();
            post.PostType = 1;
            post.MarkdownBody = blog.Body;
            post.PublishTime = blog.PublishTime;
            post.DataStatus = 1;
            post.CreateTime = DateTime.Now;
            post.LastUpdateTime = DateTime.Now;

            Markdown mark = new Markdown();
            var htmlBody = mark.Transform(blog.Body);
            var content = StripTagsRegex(htmlBody);
            post.Summary = content.Length < 50 ? content : content.Substring(0, 47) + "...";

            string sql = "insert into Post(Title, Ename, PostType, MarkdownBody, HtmlBody, PublishTime, DataStatus, CreateTime, LastUpdateTime)values(@Title, @Ename, @PostType, @MarkdownBody, @HtmlBody, @PublishTime, @DataStatus, @CreateTime, @LastUpdateTime)";

            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                sqliteConn.Execute(sql, post);
            }

            return new CreateBlogResponse { Status = 1, Message = "发布成功 - " + request.Version };
        }

        // PUT /api/blog/id
        [HttpPut]
        public UpdateBlogResponse Update(int id, [FromBody]UpdateBlogRequest request)
        {
            if (id <= 0)
            {
                return new UpdateBlogResponse { Status = 1001, Message = "id is required" };
            }
            if (request.Blog == null)
            {
                return new UpdateBlogResponse { Status = 1002, Message = "blog is null" };
            }

            //HarrisBlogDataContext context = new HarrisBlogDataContext();

            //var postEntity = context.Post.FirstOrDefault(v => v.Id == id);
            //if (postEntity == null)
            //{
            //    return new UpdateBlogResponse { Status = 404, Message = "post is not found" };
            //}

            //var blog = request.Blog;
            //postEntity.Title = blog.Title;
            //postEntity.Ename = blog.Ename.Replace(" ", "-").ToLower();
            //postEntity.MarkdownBody = blog.Body;
            //postEntity.HtmlBody = blog.HtmlBody;
            //postEntity.LastUpdateTime = DateTime.Now;

            //var content = StripTagsRegex(blog.HtmlBody);
            //postEntity.Summary = content.Length < 50 ? content : content.Substring(0, 47) + "...";

            //context.SubmitChanges();

            return new UpdateBlogResponse { Status = 1, Message = "更新成功" + request.Version };
        }

        [HttpPost]
        public DeletePostResponse Delete(int id)
        {
            //HarrisBlogDataContext context = new HarrisBlogDataContext();

            //var postEntity = context.Post.FirstOrDefault(v => v.Id == id);
            //if (postEntity == null)
            //{
            //    return new DeletePostResponse { Status = 404 };
            //}

            //postEntity.DataStatus = 2;
            //postEntity.LastUpdateTime = DateTime.Now;
            //context.SubmitChanges();

            return new DeletePostResponse { Status = 1 };
        }

        //[ActionName("image-list")]
        //public GetImageListResponse XGetPostList(GetImageListRequest request)
        //{
        //    HarrisBlogDataContext context = new HarrisBlogDataContext();

        //    var items = context.Post.Select(v => new PostVo { Ename = v.Ename, Title = v.Title }).ToList();

        //    return new GetImageListResponse { Status = 1 };
        //}

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty).Replace("[\r\n]", string.Empty);
        }
    }
}
