using Infra.Entities;
using System.Collections.Generic;

namespace Infra.Interfaces
{
    public interface IAgentParams
    {
        void Load();

        void Save();

        bool HasServicesParam();

        bool HasProcessesParam();

        int GetMaxRecoveryAttempts();

        List<ServiceEntity> GetServices();

        List<ProcessEntity> GetProcesses();


        int GetTimerProcess();


        int GetTimerKeepAlive();        
    }
}
