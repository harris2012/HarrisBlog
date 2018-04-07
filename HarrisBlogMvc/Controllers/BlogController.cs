using HarrisBlog.Repository;
using HarrisBlogMvc.Request;
using HarrisBlogMvc.Response;
using HarrisBlogMvc.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace HarrisBlogMvc.Controllers
{
    public class BlogController : ApiController
    {
        // GET /api/blog
        [HttpGet]
        public GetBlogListResponse Get()
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var items = context.Post.OrderByDescending(v => v.CreateTime).Select(v => ToVo(v)).ToList();

            return new GetBlogListResponse { Status = 1, PostList = items };
        }

        // GET /api/blog/id
        [HttpGet]
        public GetBlogResponse Get(int id)
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var postEntity = context.Post.FirstOrDefault(v => v.Id == id);
            if (postEntity == null)
            {
                return new GetBlogResponse { Status = 404 };
            }

            return new GetBlogResponse { Blog = ToVo(postEntity) };

        }

        private static BlogVo ToVo(Post v)
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
        public CreateBlogResponse Post([FromBody]CreateBlogRequest request)
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            if (request.Blog == null)
            {
                return new CreateBlogResponse { Status = 1002, Message = "blog is null" };
            }

            var blog = request.Blog;

            Post post = new Post();
            post.Title = blog.Title;
            post.Ename = blog.Ename.Replace(" ", "-").ToLower();
            post.PostType = 1;
            post.MarkdownBody = blog.Body;
            post.HtmlBody = blog.HtmlBody;
            post.CreateTime = DateTime.Now;
            post.LastUpdateTime = DateTime.Now;

            var content = StripTagsRegex(blog.HtmlBody);
            post.Summary = content.Length < 50 ? content : content.Substring(0, 47) + "...";

            context.Post.InsertOnSubmit(post);

            context.SubmitChanges();

            return new CreateBlogResponse { Status = 1, Message = "发布成功 - " + request.Version };
        }

        // PUT /api/blog/id
        [HttpPut]
        public UpdateBlogResponse Put(int id, [FromBody]UpdateBlogRequest request)
        {
            if (id <= 0)
            {
                return new UpdateBlogResponse { Status = 1001, Message = "id is required" };
            }
            if (request.Blog == null)
            {
                return new UpdateBlogResponse { Status = 1002, Message = "blog is null" };
            }

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var postEntity = context.Post.FirstOrDefault(v => v.Id == id);
            if (postEntity == null)
            {
                return new UpdateBlogResponse { Status = 404, Message = "post is not found" };
            }

            var blog = request.Blog;
            postEntity.Title = blog.Title;
            postEntity.Ename = blog.Ename.Replace(" ", "-").ToLower();
            postEntity.MarkdownBody = blog.Body;
            postEntity.HtmlBody = blog.HtmlBody;
            postEntity.LastUpdateTime = DateTime.Now;

            var content = StripTagsRegex(blog.HtmlBody);
            postEntity.Summary = content.Length < 50 ? content : content.Substring(0, 47) + "...";

            context.SubmitChanges();

            return new UpdateBlogResponse { Status = 1, Message = "更新成功" + request.Version };
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
