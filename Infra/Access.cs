using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class Access
    {
        public static string Connection { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionDB"].ToString();
    }
}
