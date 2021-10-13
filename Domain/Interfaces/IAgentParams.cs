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

        /// <summary>
        /// Get services configurated
        /// </summary>
        /// <returns></returns>
        List<ParamEntity> GetServices();

        /// <summary>
        /// Get processes configurated 
        /// </summary>
        /// <returns></returns>
        List<ProcessParam> GetProcesses();


        int GetTimerProcess();


        int GetTimerKeepAlive();        
    }
}
