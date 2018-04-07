using HarrisZhang.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrisZhang.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string param)
        {
            PostRepository repository = new PostRepository(param);
            var entity = repository.GetData();
            if (entity == null)
            {
                return HttpNotFound();
            }

            ViewBag.PostEntity = entity;

            ViewBag.Tab = "post";
            return View();
        }
    }
}