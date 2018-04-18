using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository.Entity
{
    public class TheEntity
    {
        /// <summary>
        /// 1. 日志
        /// 2. 说说
        /// </summary>
        public int PostType { get; set; }

        #region 日志
        public string Ename { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime PublishTime { get; set; }
        #endregion


        #region 说说
        public string Content { get; set; }

        public List<string> ImageList { get; set; }
        #endregion
    }
}
