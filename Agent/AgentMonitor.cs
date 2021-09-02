using Infra;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using Domain.Service.UseCases;
using Domain.Service.Entities;


namespace Agent
{
    public partial class AgentMonitor : ServiceBase
    {
        Timer timerCycle = new Timer();
        Timer timerKeepAlive = new Timer();
        MonitorService monitorService = null;
        MonitorProcess monitorProcess = null;

        public AgentMonitor()
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
            monitorService = new MonitorService();
            monitorProcess = new MonitorProcess();

            AlertHelper.Alert(AlertConsts.AGENT_STARTED, "Service is started at " + DateTime.Now + " with TimerProcess on " + monitorService.Params.TimerProcess.ToString() + " ms, Keep Alive Timer on " + monitorService.Params.TimerKeepAlive.ToString() + " ms, Services: " + monitorService.Params.Services.Count.ToString() + " and Processes: " + monitorService.Params.Processes.Count.ToString(), EAlertLevel.OFF);
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = monitorService.Params.TimerProcess;
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = monitorService.Params.TimerKeepAlive;
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
