using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using System.Collections.Generic;

namespace Domain.UseCases
{
    public class Monitor : UseCase
    {
        public List<MonitorDetail> MonitoringItems = null;

        protected DTOMonitor DTO = null;

        private IMachineData _MachineData;

        public Monitor(IAgentParams agentParams, IAccess access, IMachineData machineData) : base(agentParams)
        {
            ValidateDomain(agentParams, access, machineData);

            MonitoringItems = new List<MonitorDetail>();            
        }

        public virtual void Start()
        {            
            Machine Machine = _MachineData.CollectData();

            //TODO: com o Machine Name, devemos consulta o DB para trazer a categoria que está associada.
            //Se não trazer nada iremos aplicar a rotina que usará palavras chaves pré-definidas para definir um nome padrão para as categorias, baseado em outras propriedades da classe.
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

        private void ValidateDomain(IAgentParams agentParams, IAccess access, IMachineData machineData)
        {
            DomainExceptionValidation.When(agentParams == null,
                "Agent Params object is required!");

            agentParams.Load();

            DomainExceptionValidation.When(access == null,
                "Access object is required!");

            DTO = new DTOMonitor(access);

            DomainExceptionValidation.When(DTO == null,
                "Error to instance DTO with these Access object!");

            DomainExceptionValidation.When(machineData == null,
                "Machine Data object is required!");

            _MachineData = machineData;
        }

        
    }
}
