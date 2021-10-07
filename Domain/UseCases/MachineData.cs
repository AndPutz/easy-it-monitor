using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Domain.UseCases
{
    public sealed class MachineData : UseCase
    {
        public MachineData(IAgentParams agentParams) : base (agentParams)
        {            
        }

        public Machine CollectData()
        {
            OperatingSystem os = Environment.OSVersion;

            Machine _Machine = new Machine(Environment.MachineName,
                                           os.Platform.ToString(),
                                           os.VersionString,
                                           os.ServicePack,
                                           Environment.ProcessorCount);
            

            return _Machine;
        }
    }
}
