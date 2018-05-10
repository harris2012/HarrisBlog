using HarrisZhang.Biz;
using HarrisZhang.Repository;
using HarrisZhang.Vo;
using Repository.Entity;
using Savory.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HarrisZhang.Controllers
{
    public class PostsController : Controller
    {


        private static readonly int PageSize = 6;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">页码，从1开始</param>
        /// <returns></returns>
        public ActionResult Index(int? param)
        {
            var pageValue = param != null && param.Value > 0 ? param.Value : 1;

            SummaryBiz summaryBiz = new SummaryBiz();

            List<SummaryVo> summaries = summaryBiz.GetSummaryList();

            {
                ViewBag.PostsList = summaries.OrderByDescending(v => v.PublishTime).Skip((pageValue - 1) * PageSize).Take(PageSize).ToList();

                var totalCount = summaries.Count;

                ViewBag.PageIndex = pageValue;
                ViewBag.PageCount = totalCount / PageSize + (totalCount % PageSize > 0 ? 1 : 0);
            }

            ViewBag.Tab = "post";
            return View();
        }
    }
}