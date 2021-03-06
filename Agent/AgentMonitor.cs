using Infra;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using Domain.UseCases;
using Domain.Entities;


namespace Agent
{
    public partial class AgentMonitor : ServiceBase
    {
        Timer timerCycle = new Timer();
        Timer timerKeepAlive = new Timer();
        MonitorService monitorService = null;
        MonitorProcess monitorProcess = null;
        AlertHelper alertHelper = null;
        Access access = null;

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

            alertHelper = new AlertHelper();
            access = new Access();
        }

        protected override void OnStart(string[] args)
        {
            AgentParams agentParams = new AgentParams();
            monitorService = new MonitorService(agentParams, alertHelper, access, new MachineData(agentParams));
            monitorProcess = new MonitorProcess(agentParams, access, new MachineData(agentParams));

            alertHelper.Alert(alertHelper.GetAlertTypeForAgentMonitorStarted(), "Service is started at " + DateTime.Now + " with TimerProcess on " + monitorService.Params.GetTimerProcess().ToString() + " ms, Keep Alive Timer on " + monitorService.Params.GetTimerKeepAlive().ToString() + " ms, Services: " + monitorService.Params.GetServices().Count.ToString() + " and Processes: " + monitorService.Params.GetProcesses().Count.ToString(), EAlertLevel.OFF);
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = monitorService.Params.GetTimerProcess();
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = monitorService.Params.GetTimerKeepAlive();
            timerKeepAlive.Enabled = true;
        }

        private void OnElapsedKeepAliveTime(object sender, ElapsedEventArgs e)
        {
            alertHelper.Alert(alertHelper.GetAlertTypeForAgentMonitorKeepAlive(), "Keep Alive at " + DateTime.Now, EAlertLevel.OFF);
        }

        protected override void OnStop()
        {
            alertHelper.Alert(alertHelper.GetAlertTypeForAgentMonitorStopped(), "Service is stopped at " + DateTime.Now, EAlertLevel.OFF);
        }

        private void OnElapsedCycleTime(object source, ElapsedEventArgs e)
        {
            monitorService.Monitoring();

            monitorProcess.Monitoring();
        }

        
    }
}
