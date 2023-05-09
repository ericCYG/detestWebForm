using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace deTestWebForm0509
{
    public class Unity
    {
        string ConnectStr = "";
        string sqlStr = "";

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=GSSWEB;Integrated Security=True");
          
        }



    }
}