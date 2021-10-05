using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAlert
    {
        void Alert(string MessageHead, string Message, EAlertLevel AlertLevel);

        string GetAlertTypeForAgentMonitorStopped();

        string GetAlertTypeForAgentMonitorKeepAlive();

        string GetAlertTypeForAgentMonitorStarted();

        string GetAlertTypeForAgentMonitorDiskError();

        string GetAlertTypeForAgentMonitorSaveDiskError();

        string GetAlertTypeForAgentMonitorServiceDontExist();

        string GetAlertTypeForWatchDogStopped();

        string GetAlertTypeForWatchDogKeepAlive();

        string GetAlertTypeForWatchDogStarted();

        string GetAlertTypeForWatchDogServiceOn();

        string GetAlertTypeForWatchDogServiceOff();

        string GetAlertTypeForWatchDogServiceNotPossible();

        string GetAlertTypeForWatchDogProcessOn();

        string GetAlertTypeForWatchDogProcessOff();        

        string GetAlertTypeForAutomationItCleanTempDirInfo();

        string GetAlertTypeForAutomationItCleanTempUnauthorized();

        

    }
}
