using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Gen
{
    public class ConnectionProvider
    {
        static string sqliteConnString = @"Data Source=D:\HarrisData\HarrisBlog.db3;Version=3";

        public static SQLiteConnection GetSqliteConn()
        {
            var sqliteConn = new SQLiteConnection(sqliteConnString);
            sqliteConn.Open();

            return sqliteConn;
        }
    }
}
