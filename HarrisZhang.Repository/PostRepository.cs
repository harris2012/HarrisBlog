using HarrisZhang.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisZhang.Repository
{
    public class PostRepository : RepositoryBase<PostEntity>
    {
        public string FileRelativePath { get; private set; }

        public PostRepository(string postEname)
        {
            this.FileRelativePath = $"~/App_Data/posts/{postEname}.xml";
        }

        protected override string FilePath
        {
            get
            {
                return this.FileRelativePath;
            }
        }
    }
}
