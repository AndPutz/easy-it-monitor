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

            //TODO: com o Machine Name, devemos consulta o DB para trazer a categoria que está associada.
            //Se não trazer nada iremos aplicar a rotina que usará palavras chaves pré-definidas para definir um nome padrão para as categorias.
            //Se não encontrar na lista dessas palavras chaves que vão vir do DB, então podemos alterar a lista do BD. Se não encontrar gravar como Sem Categoria Definida.

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
