using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAgentParams
    {
        void Load();

        void Save();

        bool HasServicesParam();

        bool HasProcessesParam();

        int GetMaxRecoveryAttempts();

        List<ParamEntity> GetServices();

        List<ProcessParam> GetProcesses();


        int GetTimerProcess();


        int GetTimerKeepAlive();        
    }
}
