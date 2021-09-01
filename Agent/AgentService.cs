using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Agent
{
    public partial class AgentService : ServiceBase
    {
        Timer timerCycle = new Timer();
        Timer timerKeepAlive = new Timer();

        public AgentService()
        {
            InitializeComponent();            

            EventLog eventLog = new EventLog();

            if (!EventLog.SourceExists(SystemInfo.AGENT_NAME))
            {
                EventLog.CreateEventSource(SystemInfo.AGENT_NAME, SystemInfo.AGENT_NAME + "_Log");
            }

            eventLog.Source = SystemInfo.AGENT_NAME;
            eventLog.Log = SystemInfo.AGENT_NAME + "_Log";
        }

        protected override void OnStart(string[] args)
        {            
            WriteToFile("Service is started at " + DateTime.Now);
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = SystemInfo.CycleTimer;
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = SystemInfo.KeepAliveTimer;
            timerKeepAlive.Enabled = true;
        }

        private void OnElapsedKeepAliveTime(object sender, ElapsedEventArgs e)
        {
            //TODO: Save on DB and if no connection or error, write on file.
            WriteToFile("Keep Alive at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedCycleTime(object source, ElapsedEventArgs e)
        {
            
        }

        public void WriteToFile(string Message)
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
