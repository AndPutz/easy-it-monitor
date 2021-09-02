using Domain.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra;
using System.ServiceProcess;

namespace Domain.Service.UseCases
{
    public class WatchDogService : WatchDog
    {

        public WatchDogService()            
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
                    AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_SERVICE_OFF, "SERVICE " + RecoverItem.ServiceItem.DisplayName + " OFF", EAlertLevel.WARNING);

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
                    //else = Monitoring 
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
                    ServiceEntity ServiceToWatch = Params.Services.FirstOrDefault(f => f.Name.Equals(ServiceContr.DisplayName));

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
            if (Params != null && Params.Services != null && Params.Services.Count > 0)
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
                else if (Item.AttempsToRecover >= Params.MaxRecoveryAttempts)
                {
                    Item.Status = RecoveryStatus.NotPossible;

                    AlertHelper.Alert("WATCHDOG_SERVICE_NOT_POSSIBLE", "AFTER " + Params.MaxRecoveryAttempts.ToString() + " ATTEMPS WAS NOT POSSIBLE TO START THE " + Item.ServiceItem.DisplayName, EAlertLevel.WARNING);
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

                AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_SERVICE_ON, Item.ServiceItem.DisplayName + " ON", EAlertLevel.INFO);                
            }
        }
    }
}
