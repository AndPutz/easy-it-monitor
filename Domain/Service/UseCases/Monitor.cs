using Domain.Service.DTO;
using Domain.Service.Entities;
using System.Collections.Generic;

namespace Domain.Service.UseCases
{
    public class Monitor : UseCase
    {
        protected List<MonitorDetail> MonitoringItems = null;

        protected DTOMonitor DTO = null;

        private MachineData _MachineData = null;


        public Monitor()
        {                        
            DTO = new DTOMonitor();            
            _MachineData = new MachineData();
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
