using Nhom4_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_LTWeb.Class
{
    public class ConnectData
    {
        static ConnectData cond;
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=.;Initial Catalog=ComputerMuda;Integrated Security=True");
        private ConnectData()
        {

        }
        public static ConnectData Cond()
        {
            if (cond == null)
            {
                cond = new ConnectData();
            }
            return cond;
        }
        public DbMyWebDataContext GetConectionDaTa()
        {
            return db;
        }
    }
}