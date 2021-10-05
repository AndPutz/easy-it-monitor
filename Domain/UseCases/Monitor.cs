using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.UseCases
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
