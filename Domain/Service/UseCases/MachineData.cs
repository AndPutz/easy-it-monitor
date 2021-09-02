﻿using Domain.Service.Entities;
using System;

namespace Domain.Service.UseCases
{
    public class MachineData : UseCase
    {
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