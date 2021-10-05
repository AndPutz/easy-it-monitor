using Domain.Entities;
using Domain.Interfaces;
using System;
using System.IO;

namespace Infra
{
    public class AlertHelper : IAlert
    {                                                                
        public void Alert(string MessageHead, string Message, EAlertLevel AlertLevel)
        {
            switch (AlertLevel)
            {
                case EAlertLevel.OFF:
                    WriteToFile(Message);
                    break;
                case EAlertLevel.CRITICAL:
                    WriteToFile("CRITICAL: " + Message);
                    break;
                case EAlertLevel.HIGH:
                    WriteToFile("HIGH: " + Message);
                    break;
                case EAlertLevel.MEDIUM:
                    WriteToFile("MEDIUM: " + Message);
                    break;
                case EAlertLevel.LOW:
                    WriteToFile("LOW: " + Message);
                    break;
                case EAlertLevel.INFO:
                    WriteToFile("INFO: " + Message);
                    break;
                default:
                    break;
            }

            //TODO: Send Alert on Api channel and the web api will query to see the Alert Configs to send as the frequency or period to send it.
        }

        public string GetAlertTypeForAgentMonitorStopped()
        {
            return "AGENT_MONITOR_STOPPED";
        }

        public string GetAlertTypeForAgentMonitorKeepAlive()
        {
            return "AGENT_MONITOR_KEEPALIVE";
        }

        public string GetAlertTypeForAgentMonitorStarted()
        {
            return "AGENT_MONITOR_STARTED";
        }

        public string GetAlertTypeForAgentMonitorDiskError()
        {
            return "AGENT_MONITOR_DISK_ERROR";
        }

        public string GetAlertTypeForAgentMonitorSaveDiskError()
        {
            return "AGENT_MONITOR_SAVE_DISK_ERROR";
        }

        public string GetAlertTypeForAgentMonitorServiceDontExist()
        {
            return "AGENT_MONITOR_SERVICE_DONT_EXIST";
        }

        public string GetAlertTypeForWatchDogStopped()
        {
            return "AGENT_WATCHDOG_STOPPED";
        }

        public string GetAlertTypeForWatchDogKeepAlive()
        {
            return "AGENT_WATCHDOG_KEEPALIVE";
        }

        public string GetAlertTypeForWatchDogStarted()
        {
            return "AGENT_WATCHDOG_STARTED";
        }

        public string GetAlertTypeForWatchDogServiceOn()
        {
            return "AGENT_WATCHDOG_SERVICE_ON";
        }

        public string GetAlertTypeForWatchDogServiceOff()
        {
            return "AGENT_WATCHDOG_SERVICE_OFF";
        }

        public string GetAlertTypeForWatchDogProcessOn()
        {
            return "AGENT_WATCHDOG_PROCESS_ON";
        }

        public string GetAlertTypeForWatchDogProcessOff()
        {
            return "AGENT_WATCHDOG_PROCESS_OFF";
        }

        public string GetAlertTypeForAutomationItCleanTempDirInfo()
        {
            return "AGENT_AUTOMATIONIT_CLEAN_TEMP_DIR_INFO";
        }

        public string GetAlertTypeForAutomationItCleanTempUnauthorized()
        {
            return "AGENT_AUTOMATIONIT_CLEANTEMP_UNAUTHORIZED";
        }

        public string GetAlertTypeForWatchDogServiceNotPossible()
        {
            return "WATCHDOG_SERVICE_NOT_POSSIBLE";
        }

        private void WriteToFile(string Message)
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
}
