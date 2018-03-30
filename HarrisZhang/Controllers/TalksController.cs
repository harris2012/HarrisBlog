using HarrisZhang.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrisZhang.Controllers
{
    public class TalksController : Controller
    {
        TalksRepository postsRepository = new TalksRepository();

        private static readonly int PageSize = 15;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">页码，从1开始</param>
        /// <returns></returns>
        public ActionResult Index(int? param)
        {
            var pageValue = param != null && param.Value > 0 ? param.Value : 1;

            ViewBag.TalksList = postsRepository.GetData().OrderByDescending(v => v.PublishTime).Skip((pageValue - 1) * PageSize).Take(PageSize).ToList();

            var totalCount = postsRepository.GetData().Count;

            ViewBag.PageIndex = pageValue;
            ViewBag.PageCount = totalCount / PageSize + (totalCount % PageSize > 0 ? 1 : 0);

            return View();
        }
    }
}