using HarrisBlog.Repository;
using HarrisBlogMvc.Request;
using HarrisBlogMvc.Response;
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
            post.PostType = 1;
            post.MarkdownBody = request.Body;
            post.HtmlBody = request.HtmlBody;
            post.CreateTime = DateTime.Now;

            var content = StripTagsRegex(request.HtmlBody);
            post.Summary = content.Length < 50 ? content : content.Substring(0, 47) + "...";

            context.Post.InsertOnSubmit(post);

            context.SubmitChanges();

            return new CreatePostResponse { Status = 1, Message = "发布成功" };
        }

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty).Replace("[\r\n]", string.Empty);
        }
    }
}
