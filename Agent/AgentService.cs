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

            if (!EventLog.SourceExists(SystemInfo.AGENT_MONITOR_NAME))
            {
                EventLog.CreateEventSource(SystemInfo.AGENT_MONITOR_NAME, SystemInfo.AGENT_MONITOR_NAME + "_Log");
            }

            eventLog.Source = SystemInfo.AGENT_MONITOR_NAME;
            eventLog.Log = SystemInfo.AGENT_MONITOR_NAME + "_Log";
        }

        protected override void OnStart(string[] args)
        {
            AlertHelper.Alert(AlertConsts.AGENT_STARTED, "Service is started at " + DateTime.Now, EAlertLevel.OFF);            
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = SystemInfo.CycleTimer;
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = SystemInfo.KeepAliveTimer;
            timerKeepAlive.Enabled = true;
        }

        private void OnElapsedKeepAliveTime(object sender, ElapsedEventArgs e)
        {            
            AlertHelper.Alert(AlertConsts.AGENT_KEEPALIVE, "Keep Alive at " + DateTime.Now, EAlertLevel.OFF);
        }

        protected override void OnStop()
        {
            AlertHelper.Alert(AlertConsts.AGENT_STOPPED, "Service is stopped at " + DateTime.Now, EAlertLevel.OFF);
        }

        private void OnElapsedCycleTime(object source, ElapsedEventArgs e)
        {
            
        }

        
    }
}
