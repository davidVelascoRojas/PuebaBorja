using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lands.Api.Controllers
{
    public class Connection
    {
        public static SqlConnection Conn()
        {
            string db = "LABORJA";
            string servidorSql = "SERVER";
            //string servidorSql = "DESKTOP-R7S5800";
            SqlConnection con =
            new SqlConnection(@"Data Source=" + servidorSql + ";Initial Catalog=" + db + ";Integrated Security=True");
            return con;
        }
    }
}