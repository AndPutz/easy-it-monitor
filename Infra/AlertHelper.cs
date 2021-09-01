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
                    break;
                case EAlertLevel.WARNING:
                    break;
                case EAlertLevel.INFO:
                    break;
                default:
                    break;
            }

            //TODO: Send Alert on Api channel and the web api will query to see the Alert Configs to send it.
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
        public const string AGENT_STOPPED = "AGENT_STOPPED";
        public const string AGENT_KEEPALIVE = "AGENT_KEEPALIVE";
        public const string AGENT_STARTED = "AGENT_STARTED";
        public const string AGENT_MONITOR_DISK_ERROR = "AGENT_MONITOR_DISK_ERROR";
        public const string AGENT_MONITOR_SAVE_DISK_ERROR = "AGENT_MONITOR_SAVE_DISK_ERROR";
        public const string AGENT_MONITOR_SERVICE_DONT_EXIST = "AGENT_MONITOR_SERVICE_DONT_EXIST";
        public const string AGENT_WATCHDOG_SERVICE_OFF = "AGENT_WATCHDOG_SERVICE_OFF";
        public const string AGENT_WATCHDOG_SERVICE_ON = "AGENT_WATCHDOG_SERVICE_ON";
    }

    public enum EAlertLevel
    {
        OFF,
        ERROR,
        WARNING,
        INFO
    }

    
}
