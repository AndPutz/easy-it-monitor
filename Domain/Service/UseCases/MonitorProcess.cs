using Domain.Service.Entities;
using Domain.Service.Interfaces;

namespace Domain.Service.UseCases
{
    public class MonitorProcess : Monitor
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
