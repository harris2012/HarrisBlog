using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HarrisZhang.Repository
{
    internal class ConnectionProvider
    {
        public static SQLiteConnection GetSqliteConn()
        {
#if DEBUG
            var sqliteConnString = @"Data Source=D:\HarrisBlog.db3;Version=3";
#else
            var path = HttpContext.Current.Server.MapPath("~/App_Data/HarrisBlog.db3");

            var sqliteConnString = string.Format(@"Data Source={0};Version=3", path);
#endif

            var sqliteConn = new SQLiteConnection(sqliteConnString);
            sqliteConn.Open();

            return sqliteConn;
        }
    }
}
