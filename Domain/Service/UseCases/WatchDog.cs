using Domain.Service.Entities;
using Domain.Service.Interfaces;
using System.Collections.Generic;

namespace Domain.Service.UseCases
{
    public class WatchDog : UseCase
    {
        protected List<RecoveryItem> ListRecovering = null;

        protected List<MonitorDetail> ListMonitorDetails = null;

        protected IAlert _alert = null;

        public WatchDog(IAgentParams agentParams, IAlert alert) : base (agentParams)
        {
            agentParams.Load();

            this.ListRecovering = new List<RecoveryItem>();
            this.ListMonitorDetails = new List<MonitorDetail>();

            _alert = alert;
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
