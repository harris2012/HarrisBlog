using Savory.Dapper;
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
    public abstract class RepositoryBase
    {
        protected List<T> GetEntityList<T>(string sql) where T : class
        {
            using (var sqliteConn = ConnectionProvider.GetSqliteConn())
            {
                return sqliteConn.Query<T>(sql).ToList();
            }
        }
    }
}
