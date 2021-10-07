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

        public int CategoryMachinesId { get; private set; }

        public CategoryMachines Category { get; private set; }

        public Machine(string machineName, string platform, string version, string servicePack, int processorCount)
        {
            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;
        }

        public Machine(string machineName, string platform, string version, string servicePack, int processorCount, int categoryMachinesId, CategoryMachines category)
        {
            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;

            DomainExceptionValidation.When(categoryMachinesId < 0, "Invalid Category MachinesId Value!");
            CategoryMachinesId = categoryMachinesId;

            DomainExceptionValidation.When(category == null, "Category Machines is required!");
            Category = category;
        }

        public Machine(Int64 id, string machineName, string platform, string version, string servicePack, int processorCount, int categoryMachinesId, CategoryMachines category)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id Value!");
            Id = id;

            ValidateDomain(machineName);

            Platform = platform;
            Version = version;
            ServicePack = servicePack;
            ProcessorCount = processorCount;

            DomainExceptionValidation.When(categoryMachinesId < 0, "Invalid Category MachinesId Value!");
            CategoryMachinesId = categoryMachinesId;

            DomainExceptionValidation.When(category == null, "Category Machines is required!");
            Category = category;
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
