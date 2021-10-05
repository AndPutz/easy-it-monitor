using System.Diagnostics;
using System.ServiceProcess;

namespace Domain.Entities
{
    public sealed class RecoveryItem : Entity
    {
        public string Name { get; set; }
        public RecoveryType RecoverType { get; set; }
        public RecoveryStatus Status { get; set; }
        public int AttempsToRecover { get; set; }
        public string Path { get; set; }
        public ServiceController ServiceItem { get; set; }
        public Process ProcessItem { get; set; }
    }

    public enum RecoveryType
    {
        Service,
        Process
    }

    public enum RecoveryStatus
    {
        Stop,
        Starting,
        Pending,
        Running,
        NotPossible
    }

   
}
