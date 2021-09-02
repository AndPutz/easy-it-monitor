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
        public const string AGENT_MONITOR_NAME = "EasyITMonitorAgent";
        public const string AGENT_MONITOR_DESCRIPTION = "Easy IT Monitor";

        public const string AGENT_WATCHDOG_NAME = "EasyITWatchDogAgent";
        public const string AGENT_WATCHDOG_DESCRIPTION = "Easy IT WatchDog";

        public static int KeepAliveTimer { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["KeepAliveTimer"]);
        public static int CycleTimer { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["CycleTimer"]);


        public static string ASSEMBLY_NAME = Assembly.GetExecutingAssembly().GetName().Name;

        public static string VERSION =
            $"{Assembly.GetExecutingAssembly().GetName().Version.Major}." +
            $"{Assembly.GetExecutingAssembly().GetName().Version.Minor}." +
            $"{Assembly.GetExecutingAssembly().GetName().Version.Build}";


    }
}
