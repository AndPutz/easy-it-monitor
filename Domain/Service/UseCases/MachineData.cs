﻿using Domain.Service.Entities;
using Domain.Service.Interfaces;
using System;

namespace Domain.Service.UseCases
{
    public class MachineData : UseCase
    {
        public MachineData(IAgentParams agentParams) : base (agentParams)
        {            
        }

        public Machine CollectData()
        {
            OperatingSystem os = Environment.OSVersion;

            Machine _Machine = new Machine();

            _Machine.Id = 0;
            _Machine.MachineName = Environment.MachineName;
            _Machine.Platform = os.Platform.ToString();
            _Machine.Version = os.VersionString;
            _Machine.ServicePack = os.ServicePack;
            _Machine.ProcessorCount = Environment.ProcessorCount;

            return _Machine;
        }
    }
}
