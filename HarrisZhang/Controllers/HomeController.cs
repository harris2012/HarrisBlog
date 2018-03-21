using HarrisZhang.Repository;
using HarrisZhang.Repository.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace HarrisZhang.Controllers
{
    public class HomeController : Controller
    {
        PostsRepository postsRepository = new PostsRepository();

        public ActionResult Index()
        {
            ViewBag.PostsList = postsRepository.GetData().ToList();
            ViewBag.TotalCount = postsRepository.GetData().Count;

            return View();
        }

        private void GenerateData()
        {
            List<PostEntity> postEntityList = new List<PostEntity>();
            for (int i = 0; i < 25; i++)
            {
                PostEntity postEntity = new PostEntity();
                postEntity.Title = $"This is a test {i}";
                postEntity.Ename = $"this-is-post-{i}";
                postEntity.PostStyle = i % 2 + 1;
                postEntity.Summary = $"This is summary {i}";
                postEntity.PublishTime = DateTime.Now;
                
                postEntity.HtmlBody = $"<p>this is from {i}</p>";
                if (postEntity.PostStyle == 1)
                {
                    postEntity.MarkdownBody = $"# this is from {i}"; ;
                }
                postEntityList.Add(postEntity);
            }

            List<PostsEntity> postsEntityList = new List<PostsEntity>();
            foreach (var item in postEntityList)
            {
                PostsEntity entity = new PostsEntity();
                entity.Ename = item.Ename;
                entity.Title = item.Title;
                entity.Summary = item.Summary;
                entity.PublishTime = item.PublishTime;
                postsEntityList.Add(entity);
            }

            {
                string path = HttpContext.Server.MapPath("~/Data/posts.xml");

                XmlSerializer serializer = new XmlSerializer(typeof(List<PostsEntity>));

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(stream, postsEntityList);
                }
            }

            {
                foreach (var item in postEntityList)
                {
                    string path = HttpContext.Server.MapPath($"~/Data/posts/{item.Ename}.xml");

                    XmlSerializer serializer = new XmlSerializer(typeof(PostEntity));

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        serializer.Serialize(stream, item);
                    }
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}