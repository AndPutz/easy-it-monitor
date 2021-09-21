using Domain.Service.Entities;
using System.Collections.Generic;
using Infra;
using Infra.Interfaces;

namespace Domain.Service.UseCases
{
    public class WatchDog : UseCase
    {
        protected List<RecoveryItem> ListRecovering = null;

        protected List<MonitorDetail> ListMonitorDetails = null;        
        

        public WatchDog(IAgentParams agentParams) : base (agentParams)
        {
            agentParams.Load();

            this.ListRecovering = new List<RecoveryItem>();
            this.ListMonitorDetails = new List<MonitorDetail>();
        }

        protected virtual bool HasRecovery()
        {
            if (ListRecovering != null && ListRecovering.Count > 0)
                return true;
            else
                return false;
        }

        public virtual void Monitoring()
        {

        }
    }
}
