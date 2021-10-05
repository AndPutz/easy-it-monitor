using Domain.Entities;
using Domain.Interfaces;

namespace Domain.UseCases
{
    public sealed class MonitorProcess : Monitor
    {
        public MonitorProcess(IAgentParams agentParams, IAccess access) : base (agentParams, access)
        {

        }

        public override void Save()
        {
            foreach (MonitorDetail Detail in MonitoringItems)
            {
                Detail.IsService = false;
                DTO.SaveMonitoring(Detail);
            }
        }
    }
}
