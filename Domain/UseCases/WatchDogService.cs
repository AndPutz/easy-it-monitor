using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Linq;
using System.ServiceProcess;

namespace Domain.UseCases
{
    public sealed class WatchDogService : WatchDog
    {

        public WatchDogService(IAgentParams agentParams, IAlert alert) : base(agentParams, alert)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Monitoring()
        {
            if (HasRecovery())
            {

                for (int nIdx = 0; nIdx < ListRecovering.Count; nIdx++)
                {
                    RecoveryItem RecoverItem = ListRecovering[nIdx];
                    
                    _alert.Alert(_alert.GetAlertTypeForWatchDogServiceOff(), "SERVICE " + RecoverItem.ServiceItem.DisplayName + " OFF", EAlertLevel.HIGH);

                    if (Recover(RecoverItem))
                    {
                        RecoverItem.ServiceItem.Dispose();
                        ListRecovering.RemoveAt(nIdx);
                    }
                    else if (RecoverItem.Status == RecoveryStatus.NotPossible)
                    {
                        RecoverItem.ServiceItem.Dispose();
                        ListRecovering.RemoveAt(nIdx);
                    } 
                }
            }

            base.Monitoring();
        }       

        protected override bool HasRecovery()
        {
            if (HasParameters())
            {
                ServiceController[] oListServices = ServiceController.GetServices();
                foreach (ServiceController ServiceContr in oListServices)
                {                    
                    ParamEntity param = Params.GetServices().FirstOrDefault(f => f.Name.Equals(ServiceContr.DisplayName));

                    ServiceEntity ServiceToWatch = null;

                    if (param != null)
                        ServiceToWatch = new ServiceEntity(param.Name, param.CycleTime);

                    if (ServiceToWatch == null)
                    {
                        ServiceContr.Dispose();
                    }
                    else if (ServiceContr.Status != ServiceControllerStatus.Running)
                    {
                        if (!ListRecovering.Exists(fnd => fnd.Name.Equals(ServiceToWatch.Name) &&
                                                   fnd.RecoverType == RecoveryType.Service))
                        {
                            ListRecovering.Add(new RecoveryItem()
                            {
                                RecoverType = RecoveryType.Service,
                                Name = ServiceToWatch.Name,
                                Status = RecoveryStatus.Stop,
                                AttempsToRecover = 0,
                                ProcessItem = null,
                                ServiceItem = ServiceContr
                            });
                        }
                    }
                }
            }

            return base.HasRecovery();
        }

        /// <summary>
        /// Verify if has configurated Services to WatchDog in XML file.
        /// </summary>
        /// <returns></returns>
        private bool HasParameters()
        {            
            if (Params != null && Params.GetServices() != null && Params.GetServices().Count > 0)
                return true;
            else
            {
                if (ListRecovering != null)
                    ListRecovering.Clear();

                return false;
            }            
        }

        private bool Recover(RecoveryItem Item)
        {
            if (Item.ServiceItem != null)
            {
                if (Item.ServiceItem.Status != ServiceControllerStatus.Running && Item.AttempsToRecover == 0)
                {
                    Item.Status = RecoveryStatus.Starting;
                    Item.AttempsToRecover = 1;

                    TryToStartService(Item);
                }
                else if (Item.AttempsToRecover > 0)
                {
                    if (Item.ServiceItem.Status == ServiceControllerStatus.Running)
                    {
                        Item.Status = RecoveryStatus.Running;

                    }
                    else
                    {
                        TryToStartService(Item);

                        Item.AttempsToRecover++;
                    }
                }
                else if (Item.AttempsToRecover >= Params.GetMaxRecoveryAttempts())
                {
                    Item.Status = RecoveryStatus.NotPossible;

                    _alert.Alert(_alert.GetAlertTypeForWatchDogServiceNotPossible(), "AFTER " + Params.GetMaxRecoveryAttempts().ToString() + " ATTEMPS WAS NOT POSSIBLE TO START THE " + Item.ServiceItem.DisplayName, EAlertLevel.HIGH);
                }
            }
            else
            {
                Item.Status = RecoveryStatus.Running;
            }

            return Item.Status == RecoveryStatus.Running;
        }

        private void TryToStartService(RecoveryItem Item)
        {
            TimeSpan oTimeOut = TimeSpan.FromSeconds(5);

            Item.ServiceItem.Start();
            Item.ServiceItem.WaitForStatus(ServiceControllerStatus.Running, oTimeOut);

            if (Item.ServiceItem.Status == ServiceControllerStatus.Running)
            {
                Item.Status = RecoveryStatus.Running;

                _alert.Alert(_alert.GetAlertTypeForWatchDogServiceOn(), Item.ServiceItem.DisplayName + " ON", EAlertLevel.INFO);                
            }
        }
    }
}
