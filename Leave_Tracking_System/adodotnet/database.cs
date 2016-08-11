using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace adodotnet
{
    public class database
    {
        public static string connectionstring
        {
            get
            {
            return ConfigurationManager.ConnectionStrings["leavetrack"].ToString();
        }
        }
    }
}
