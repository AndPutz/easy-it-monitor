using Domain.Service.DTO;
using Domain.Service.Entities;
using Domain.Service.Interfaces;
using System.Collections.Generic;

namespace Domain.Service.UseCases
{
    public class Monitor : UseCase
    {
        public List<MonitorDetail> MonitoringItems = null;

        protected DTOMonitor DTO = null;

        private MachineData _MachineData = null;        

        public Monitor(IAgentParams agentParams, IAccess access) : base(agentParams)
        {
            agentParams.Load();

            DTO = new DTOMonitor(access);
            _MachineData = new MachineData(agentParams);
            MonitoringItems = new List<MonitorDetail>();            
        }

        public virtual void Start()
        {
            Machine Machine = _MachineData.CollectData();
            DTO.SaveMachine(Machine);
            IdMachine = Machine.Id;
        }

        public virtual void Save()
        {

        }

        public virtual void Monitoring()
        {

        }
    }
}
