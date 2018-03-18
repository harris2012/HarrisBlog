using HarrisBlog.Repository;
using HarrisBlogMvc.Request;
using HarrisBlogMvc.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HarrisBlogMvc.Controllers
{
    public class BlogController : ApiController
    {
        [ActionName("post-list")]
        public GetPostListResponse XGetPostList(GetPostListRequest request)
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var items = context.Post.Select(v => new PostVo { Ename = v.Ename, Title = v.Title }).ToList();

            return new GetPostListResponse { Status = 1, PostList = items };
        }

        [ActionName("create-post")]
        public CreatePostResponse CreatePost(CreatePostRequest request)
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            Post post = new Post();
            post.Title = request.Title;
            post.Ename = request.Ename;
            post.Body = request.Body;

            context.Post.InsertOnSubmit(post);

            context.SubmitChanges();

            return new CreatePostResponse { Status = 1, Message = request.HtmlBody };
        }
    }
}
