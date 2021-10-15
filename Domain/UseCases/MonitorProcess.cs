using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;

namespace Domain.UseCases
{
    public sealed class MonitorProcess : Monitor
    {
        public MonitorProcess(IAgentParams agentParams, IAccess access, IMachineData machineData) : base (agentParams, access, machineData)
        {
            ValidateDomain(agentParams);
        }

        public override void Save()
        {
            foreach (MonitorDetail Detail in MonitoringItems)
            {
                Detail.IsService = false;
                DTO.SaveMonitoring(Detail);
            }
        }

        private void ValidateDomain(IAgentParams agentParams)
        {
            DomainExceptionValidation.When(agentParams.HasProcessesParam() == false,
                "Process Params is required! Probably the Init Config doesnt worked. Please add at least one Process to run the Agent Monitor");
        }
    }
}
