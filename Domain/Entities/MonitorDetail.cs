using System;

namespace Domain.Entities
{
    public sealed class MonitorDetail : Entity
    {
        public Int64 IdWatchDogItem { get; set; }
        public string Name { get; set; }
        public decimal MemoryUsedPerProcess { get; set; }
        public float AvaibleMemoryMachine { get; set; }
        public float CpuUsedMachine { get; set; }
        public double CpuUsedProcess { get; set; }
        public bool IsService { get; set; }
        public string Path { get; set; }
    }
}
