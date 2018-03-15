using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrisZhang.Controls
{
    partial class PagingTemplate
    {
        public PagingTemplate(int pageIndex, int pageCount, int pagingCount)
        {
            this.PageCount = pageCount;
            this.PageIndex = pageIndex;
            if (this.PageCount <= pagingCount)
            {
                this.StartPage = 1;
                this.EndPage = this.PageCount;
            }
            else
            {
                this.StartPage = Math.Max(1, (pageIndex - (pagingCount / 2)) + (((pagingCount % 2) == 0) ? 1 : 0));
                this.EndPage = Math.Min(this.PageCount, pageIndex + (pagingCount / 2));
                if (this.EndPage < pagingCount)
                {
                    this.EndPage = pagingCount;
                }
                if (this.StartPage > ((this.PageCount - pagingCount) + 1))
                {
                    this.StartPage = (this.PageCount - pagingCount) + 1;
                }
            }
        }

        public string UrlTemplate { get; set; } = "https://harriszhang.com/posts/{0}";

        public int EndPage { get; private set; }

        public int PageCount { get; private set; }

        public int PageIndex { get; private set; }

        public int StartPage { get; private set; }
    }
}