using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class AlertHelper
    {
        public static void Alert(string MessageHead, string Message, EAlertLevel AlertLevel)
        {
            switch (AlertLevel)
            {
                case EAlertLevel.OFF:
                    WriteToFile(Message);
                    break;
                case EAlertLevel.ERROR:
                    WriteToFile("ERROR: " + Message);
                    break;
                case EAlertLevel.WARNING:
                    WriteToFile("WARNING: " + Message);
                    break;
                case EAlertLevel.INFO:
                    WriteToFile("INFO: " + Message);
                    break;
                default:
                    break;
            }

            //TODO: Send Alert on Api channel and the web api will query to see the Alert Configs to send as the frequency or period to send it.
        }

        private static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\AgentLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }

    public class AlertConsts
    {
        public const string AGENT_MONITOR_STOPPED = "AGENT_MONITOR_STOPPED";
        public const string AGENT_MONITOR_KEEPALIVE = "AGENT_MONITOR_KEEPALIVE";
        public const string AGENT_MONITOR_STARTED = "AGENT_MONITOR_STARTED";
        public const string AGENT_MONITOR_DISK_ERROR = "AGENT_MONITOR_DISK_ERROR";
        public const string AGENT_MONITOR_SAVE_DISK_ERROR = "AGENT_MONITOR_SAVE_DISK_ERROR";
        public const string AGENT_MONITOR_SERVICE_DONT_EXIST = "AGENT_MONITOR_SERVICE_DONT_EXIST";
        public const string AGENT_WATCHDOG_STOPPED = "AGENT_WATCHDOG_STOPPED";
        public const string AGENT_WATCHDOG_KEEPALIVE = "AGENT_WATCHDOG_KEEPALIVE";
        public const string AGENT_WATCHDOG_STARTED = "AGENT_WATCHDOG_STARTED";
        public const string AGENT_WATCHDOG_SERVICE_OFF = "AGENT_WATCHDOG_SERVICE_OFF";
        public const string AGENT_WATCHDOG_SERVICE_ON = "AGENT_WATCHDOG_SERVICE_ON";
        public const string AGENT_WATCHDOG_PROCESS_OFF = "AGENT_WATCHDOG_PROCESS_OFF";
        public const string AGENT_WATCHDOG_PROCESS_ON = "AGENT_WATCHDOG_PROCESS_ON";
        public const string AGENT_AUTOMATION_CLEAN_TEMP_DIR_INFO = "AGENT_AUTOMATION_CLEAN_TEMP_DIR_INFO";
    }

    public enum EAlertLevel
    {
        OFF,
        ERROR,
        WARNING,
        INFO
    }

    
}
