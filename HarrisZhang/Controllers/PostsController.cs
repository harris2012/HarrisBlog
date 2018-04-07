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
    public class PostsController : Controller
    {
        PostsRepository postsRepository = new PostsRepository();

        private static readonly int PageSize = 9;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">页码，从1开始</param>
        /// <returns></returns>
        public ActionResult Index(int? param)
        {
            var pageValue = param != null && param.Value > 0 ? param.Value : 1;

            ViewBag.PostsList = postsRepository.GetData().OrderByDescending(v => v.PublishTime).Skip((pageValue - 1) * PageSize).Take(PageSize).ToList();

            var totalCount = postsRepository.GetData().Count;

            ViewBag.PageIndex = pageValue;
            ViewBag.PageCount = totalCount / PageSize + (totalCount % PageSize > 0 ? 1 : 0);

            ViewBag.Tab = "post";
            return View();
        }
    }
}