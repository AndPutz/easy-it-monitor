using Domain.Validation;
using System;

namespace Domain.Entities
{
    public sealed class Machine : Entity
    {
        public Int64 Id { get; private set; }
        public string MachineName { get; private set; }
        public string Platform { get; private set; }
        public string Version { get; private set; }
        public string ServicePack { get; private set; }
        public int ProcessorCount { get; private set; }

        public int CategoryMachinesId { get; set; }

        public CategoryMachines Category { get; set; }

        public Machine(string machineName, string platform, string version, string servicePack, int processorCount)
        {
            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;
        }

        public Machine(Int64 id, string machineName, string platform, string version, string servicePack, int processorCount)
        {
            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;

            DomainExceptionValidation.When(id < 0, "Invalid Id Value!");
            Id = id;
        }                

        public void Update(Int64 id)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id Value!");
            Id = id;
        }

        public void Update(string machineName, string platform, string version, string servicePack, int processorCount, int categoryMachinesId)
        {
            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;

            DomainExceptionValidation.When(categoryMachinesId < 0, "Invalid Id Category Value!");
            CategoryMachinesId = categoryMachinesId;            
        }        

        private void ValidateDomain(string machineName)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(machineName),
                "Machine Name is required!");

            DomainExceptionValidation.When(machineName.Length <= 3,
                "Invalid Machine Name, too short!");
            
            MachineName = machineName;
        }
    }
}
