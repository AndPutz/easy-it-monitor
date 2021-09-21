using Domain.Service.Entities;
using Infra.Interfaces;

namespace Domain.Service.UseCases
{
    public class MonitorProcess : Monitor
    {
        public MonitorProcess(IAgentParams agentParams) : base (agentParams)
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
