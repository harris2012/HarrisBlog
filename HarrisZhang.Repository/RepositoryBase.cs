using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;

namespace HarrisZhang.Repository
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected abstract string FilePath { get; }

        public T GetData()
        {
            var item = HttpContext.Current.Cache.Get(FilePath);

#if DEBUG
            item = null;
#endif
            if (item == null)
            {
                lock (lockObject)
                {
                    item = HttpContext.Current.Cache.Get(FilePath);
                    if (item == null)
                    {
                        string path = HttpContext.Current.Server.MapPath(FilePath);
                        if (File.Exists(path))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(T));

                            using (FileStream stream = new FileStream(path, FileMode.Open))
                            {
                                item = (T)serializer.Deserialize(stream);

                                CacheDependency dependency = new CacheDependency(path);

                                HttpContext.Current.Cache.Insert(path, item, dependency);
                            }
                        }
                    }
                }
            }

            return item as T;
        }

        private static readonly object lockObject = new object();
    }
}
