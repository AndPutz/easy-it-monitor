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
            monitorService = new MonitorService(new AgentParams());
            monitorProcess = new MonitorProcess(new AgentParams());

            AlertHelper.Alert(AlertConsts.AGENT_MONITOR_STARTED, "Service is started at " + DateTime.Now + " with TimerProcess on " + monitorService.Params.GetTimerProcess().ToString() + " ms, Keep Alive Timer on " + monitorService.Params.GetTimerKeepAlive().ToString() + " ms, Services: " + monitorService.Params.GetServices().Count.ToString() + " and Processes: " + monitorService.Params.GetProcesses().Count.ToString(), EAlertLevel.OFF);
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = monitorService.Params.GetTimerProcess();
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = monitorService.Params.GetTimerKeepAlive();
            timerKeepAlive.Enabled = true;
        }

        private void OnElapsedKeepAliveTime(object sender, ElapsedEventArgs e)
        {            
            AlertHelper.Alert(AlertConsts.AGENT_MONITOR_KEEPALIVE, "Keep Alive at " + DateTime.Now, EAlertLevel.OFF);
        }

        protected override void OnStop()
        {
            AlertHelper.Alert(AlertConsts.AGENT_MONITOR_STOPPED, "Service is stopped at " + DateTime.Now, EAlertLevel.OFF);
        }

        private void OnElapsedCycleTime(object source, ElapsedEventArgs e)
        {
            
        }

        
    }
}
