using HarrisZhang.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrisZhang.Controllers
{
    public class PostsController : Controller
    {
        PostsRepository postsRepository = new PostsRepository();

        private static readonly int PageSize = 6;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">页码，从1开始</param>
        /// <returns></returns>
        public ActionResult Index(int? param)
        {
            {
                var pageValue = param != null && param.Value > 0 ? param.Value : 1;

                ViewBag.PostsList = postsRepository.GetData().OrderByDescending(v => v.PublishTime).Skip((pageValue - 1) * PageSize).Take(PageSize).ToList();

                var totalCount = postsRepository.GetData().Count;

                ViewBag.PageIndex = pageValue;
                ViewBag.PageCount = totalCount / PageSize + (totalCount % PageSize > 0 ? 1 : 0);
            }

            ViewBag.Tab = "talk";
            return View();
        }
    }
}