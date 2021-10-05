using System;

namespace Domain.Entities
{
    public sealed class Machine : Entity
    {
        public Int64 Id { get; set; }
        public string MachineName { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string ServicePack { get; set; }
        public int ProcessorCount { get; set; }

        public int CategoryMachinesId { get; set; }

        public CategoryMachines Category { get; set; }
    }
}
