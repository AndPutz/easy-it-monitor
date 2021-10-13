using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Domain.UseCases
{
    public sealed class WatchDogProcess : WatchDog
    {
        private ManageProcess manageProcess;

        private IAlert _Alert;

        public WatchDogProcess(IAgentParams agentParams, IAlert alert) : base(agentParams, alert)
        {
            _Alert = alert;
            manageProcess = new ManageProcess(alert);
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
                    _Alert.Alert(_Alert.GetAlertTypeForWatchDogProcessOff(), "PROCESS " + RecoverItem.ProcessItem.Name + " OFF", EAlertLevel.HIGH);

                    if (Recover(RecoverItem))
                    {                        
                        ListRecovering.RemoveAt(nIdx);
                    }
                    else if (RecoverItem.Status == RecoveryStatus.NotPossible)
                    {                        
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
                List<ProcessParam> processesParams = Params.GetProcesses();

                foreach (ProcessParam processParam in processesParams)
                {
                    if(manageProcess.GetStatus(processParam.Name) == false)
                    {
                        AddListRecovering(processParam);
                    }                                       
                }                
            }

            return base.HasRecovery();
        }

        private void AddListRecovering(ProcessParam processParam)
        {
            if (!ListRecovering.Exists(fnd => fnd.Name.Equals(processParam.Name) &&
                                                   fnd.RecoverType == RecoveryType.Process))
            {
                ListRecovering.Add(new RecoveryItem()
                {
                    RecoverType = RecoveryType.Process,
                    Name = processParam.Name,
                    Status = RecoveryStatus.Stop,
                    AttempsToRecover = 0,
                    ProcessItem = new ProcessEntity(processParam.Name, processParam.CycleTime, processParam.Detail),
                    ServiceItem = null
                });
            }
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
                if (Item.Status != RecoveryStatus.Running && Item.AttempsToRecover == 0)
                {
                    Item.Status = RecoveryStatus.Starting;
                    Item.AttempsToRecover = 1;

                    if(manageProcess.StartProcess(Item.ProcessItem.Name, Item.ProcessItem.Detail))
                    {
                        Item.Status = RecoveryStatus.Running;
                    }
                }
                else if (Item.AttempsToRecover > 0)
                {
                    if (Item.Status != RecoveryStatus.Running)
                    {
                        if (manageProcess.StartProcess(Item.ProcessItem.Name, Item.ProcessItem.Detail))
                        {
                            Item.Status = RecoveryStatus.Running;
                        }
                        else
                            Item.AttempsToRecover++;

                    }                    
                }
                else if (Item.AttempsToRecover >= Params.GetMaxRecoveryAttempts())
                {
                    Item.Status = RecoveryStatus.NotPossible;

                    _alert.Alert(_alert.GetAlertTypeForWatchDogProcessNotPossible(), "AFTER " + Params.GetMaxRecoveryAttempts().ToString() + " ATTEMPS WAS NOT POSSIBLE TO START THE " + Item.ProcessItem.Name, EAlertLevel.HIGH);
                }
            }
            else
            {
                Item.Status = RecoveryStatus.Running;
            }

            return Item.Status == RecoveryStatus.Running;
        }        
    }
}
