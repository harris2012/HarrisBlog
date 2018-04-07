using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HarrisZhang.Controls
{
    public class PadingControl
    {
        public static MvcHtmlString Paging(int pageIndex, int pageCount, int pagingCount, string urlTemplate)
        {
            StringBuilder builder = new StringBuilder();

            PagingTemplate template = new PagingTemplate(pageIndex, pageCount, pagingCount, urlTemplate);

            return new MvcHtmlString(template.TransformText());
        }
    }
}