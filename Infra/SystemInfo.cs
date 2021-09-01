using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class SystemInfo
    {
        public const string AGENT_NAME = "EasyITMonitorAgent";
        public const string DESCRIPTION = "Easy IT Monitor";       
        public static int KeepAliveTimer { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["KeepAliveTimer"]);
        public static int CycleTimer { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["CycleTimer"]);


        public static string ASSEMBLY_NAME = Assembly.GetExecutingAssembly().GetName().Name;

        public static string VERSION =
            $"{Assembly.GetExecutingAssembly().GetName().Version.Major}." +
            $"{Assembly.GetExecutingAssembly().GetName().Version.Minor}." +
            $"{Assembly.GetExecutingAssembly().GetName().Version.Build}";


    }
}
