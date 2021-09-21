using Domain.Service.Entities;
using Infra;
using Infra.Interfaces;
using System;
using System.Diagnostics;

namespace Domain.Service.UseCases
{
    public class WatchDogProcess : WatchDog
    {

        public WatchDogProcess(IAgentParams agentParams) : base(agentParams)
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
                    AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_PROCESS_OFF, "PROCESS " + RecoverItem.ProcessItem.ProcessName + " OFF", EAlertLevel.HIGH);

                    if (Recover(RecoverItem))
                    {
                        RecoverItem.ProcessItem.Dispose();
                        ListRecovering.RemoveAt(nIdx);
                    }
                    else if (RecoverItem.Status == RecoveryStatus.NotPossible)
                    {
                        RecoverItem.ProcessItem.Dispose();
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
                Process[] ListProcesses = Process.GetProcesses();
                //TODO: Identify Process not running
                //foreach (Process ProcessContr in ListProcesses)
                //{                    
                //    ServiceEntity ServiceToWatch = Params.Services.FirstOrDefault(f => f.Name.Equals(ProcessContr.ProcessName));

                //    if (ServiceToWatch == null)
                //    {
                //        ProcessContr.Dispose();
                //    }
                //    else if (ProcessContr. != ServiceControllerStatus.Running)
                //    {
                //        if (!ListRecovering.Exists(fnd => fnd.Name.Equals(ServiceToWatch.Name) &&
                //                                   fnd.RecoverType == RecoveryType.Service))
                //        {
                //            ListRecovering.Add(new RecoveryItem()
                //            {
                //                RecoverType = RecoveryType.Service,
                //                Name = ServiceToWatch.Name,
                //                Status = RecoveryStatus.Stop,
                //                AttempsToRecover = 0,
                //                ProcessItem = ProcessContr,
                //                ServiceItem = null
                //            });
                //        }
                //    }
                //}
            }

            return base.HasRecovery();
        }

        /// <summary>
        /// Verify if has configurated Services to WatchDog in XML file.
        /// </summary>
        /// <returns></returns>
        private bool HasParameters()
        {            
            if (Params != null && Params.GetProcesses() != null && Params.GetProcesses().Count > 0)
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
            if (Item.ProcessItem != null)
            {
                //TODO: Recover Process
            }
            else
            {
                Item.Status = RecoveryStatus.Running;
            }

            return Item.Status == RecoveryStatus.Running;
        }

        private void TryToStartProcess(RecoveryItem Item)
        {
            TimeSpan oTimeOut = TimeSpan.FromSeconds(5);

            //TODO: Start Process
            //Item.ServiceItem.Start();
            //Item.ServiceItem.WaitForStatus(ServiceControllerStatus.Running, oTimeOut);

            //if (Item.ServiceItem.Status == ServiceControllerStatus.Running)
            //{
            //    Item.Status = RecoveryStatus.Running;

            //    AlertHelper.Alert(AlertConsts.AGENT_WATCHDOG_PROCESS_ON, Item.ServiceItem.DisplayName + " ON", EAlertLevel.INFO);                
            //}
        }
    }
}
