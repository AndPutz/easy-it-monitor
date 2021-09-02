using Domain.Service.UseCases;
using Infra;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace EasyITWatchDog
{
    public partial class AgentWatchDog : ServiceBase
    {
        Timer timerCycle = new Timer();
        Timer timerKeepAlive = new Timer();
        WatchDogService watchDogService = null;
        WatchDogProcess watchDogProcess = null;

        public AgentWatchDog()
        {
            InitializeComponent();

            EventLog eventLog = new EventLog();

            if (!EventLog.SourceExists(SystemInfo.AGENT_WATCHDOG_NAME))
            {
                EventLog.CreateEventSource(SystemInfo.AGENT_WATCHDOG_NAME, SystemInfo.AGENT_WATCHDOG_NAME + "_Log");
            }

            eventLog.Source = SystemInfo.AGENT_WATCHDOG_NAME;
            eventLog.Log = SystemInfo.AGENT_WATCHDOG_NAME + "_Log";
        }

        protected override void OnStart(string[] args)
        {
            watchDogService = new WatchDogService();
            watchDogProcess = new WatchDogProcess();

            AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_STARTED, "Service is started at " + DateTime.Now + " with TimerProcess on " + watchDogService.Params.TimerProcess.ToString() + " ms, Keep Alive Timer on " + watchDogService.Params.TimerKeepAlive.ToString() + " ms, Services: " + watchDogService.Params.Services.Count.ToString() + " and Processes: " + watchDogService.Params.Processes.Count.ToString(), EAlertLevel.OFF);
            timerCycle.Elapsed += new ElapsedEventHandler(OnElapsedCycleTime);
            timerCycle.Interval = watchDogService.Params.TimerProcess;
            timerCycle.Enabled = true;

            timerKeepAlive.Elapsed += new ElapsedEventHandler(OnElapsedKeepAliveTime);
            timerKeepAlive.Interval = watchDogService.Params.TimerKeepAlive;
            timerKeepAlive.Enabled = true;
        }

        private void OnElapsedKeepAliveTime(object sender, ElapsedEventArgs e)
        {
            AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_KEEPALIVE, "Keep Alive at " + DateTime.Now, EAlertLevel.OFF);
        }

        protected override void OnStop()
        {
            AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_STOPPED, "Service is stopped at " + DateTime.Now, EAlertLevel.OFF);
        }

        private void OnElapsedCycleTime(object source, ElapsedEventArgs e)
        {

        }
    }
}
