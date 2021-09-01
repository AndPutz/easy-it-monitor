using System;

namespace Domain.Service.Entities
{
    public class Machine : Entity
    {
        public Int64 Id { get; set; }
        public string MachineName { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string ServicePack { get; set; }
        public int ProcessorCount { get; set; }
    }
}
