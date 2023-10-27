using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Nhom4_LTWeb.Models
{
    public class ConectionDaTa
    {
        static ConectionDaTa cond;
        DbMyWebDataContext db = new DbMyWebDataContext("Data Source=LAPTOP-VC5IF5QK\\SQLEXPRESS;Initial Catalog=ComputerMuda;Integrated Security=True");
         private ConectionDaTa()
        {

        }
        public static ConectionDaTa Cond()
        {
            if(cond == null)
            {
                cond = new ConectionDaTa();
            }
            return cond;
        }
        public DbMyWebDataContext GetConectionDaTa()
        {
            return db;
        }
    }
}