using Domain.Service.Entities;
using Infra;
using Infra.Entities;
using Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading;

namespace Domain.Service.UseCases
{
    public class MonitorService : Monitor
    {
        PerformanceCounter MemCounter = null;
        PerformanceCounter CpuUsage = null;

        public MonitorService(IAgentParams agentParams) : base(agentParams)
        {
            MemCounter = new PerformanceCounter("Memory", "Available MBytes");
            CpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public override void Save()
        {
            foreach (MonitorDetail Detail in MonitoringItems)
            {
                Detail.IsService = true;
                DTO.SaveMonitoring(Detail);
            }
        }

        public override void Monitoring()
        {
            base.Monitoring();
            
            if (Params.HasServicesParam())
            {
                MonitoringProcessing();

                IdentifyServicesNotInstalled();
            }
        }

        private void MonitoringProcessing()
        {
            ServiceController[] ListServices = ServiceController.GetServices();
            
            foreach (ServiceController Service in ListServices)
            {
                if (IsServiceConfiguratedToMonitor(Service))
                {
                    if (Service.Status == ServiceControllerStatus.Running)
                    {
                        CollectData(Service, GetServiceParam(Service));
                    }
                }
                else
                {
                    Service.Dispose();
                }
            }

        }

        private void IdentifyServicesNotInstalled()
        {
            List<string> ServicesNotInstalled = GetServicesNotInstalled();

            if (ServicesNotInstalled != null && ServicesNotInstalled.Count > 0)
            {
                string ErrorMessage = "";

                foreach (string ServiceName in ServicesNotInstalled)
                {
                    ErrorMessage += ServiceName + "|";
                }

                ErrorMessage = ErrorMessage.Substring(0, ErrorMessage.Length - 1);

                AlertHelper.Alert(AlertConsts.AGENT_MONITOR_SERVICE_DONT_EXIST, "SERVICES: " + ErrorMessage + " DONT EXISTS", EAlertLevel.HIGH);                
            }
        }

        /// <summary>
        /// Search Services Configurated to Monitor to validate if they are installed on Windows Service List.
        /// If not Installed, will return the ServiceName of them.
        /// This function will report error on configuration file.
        /// </summary>
        private List<string> GetServicesNotInstalled()
        {
            List<string> ServicesNotInstalled = new List<string>();

            ServiceController[] ListServices = ServiceController.GetServices();
            
            foreach (ServiceEntity ServiceItem in Params.GetServices())
            {
                ServiceController Validation = ListServices.FirstOrDefault(f => f.ServiceName.Equals(ServiceItem.Name) || f.DisplayName.Equals(ServiceItem.Name));

                if (Validation == null)
                {
                    ServicesNotInstalled.Add(ServiceItem.Name);
                }
            }

            return ServicesNotInstalled;
        }

        public int GetWindowsProcessIdByServiceName(string ServiceName)
        {
            ManagementObject wmiService;
            wmiService = new ManagementObject("Win32_Service.Name='" + ServiceName + "'");
            object oProcessId = wmiService.GetPropertyValue("ProcessId");
            int ProcessId = 0;

            if (oProcessId != null)
                ProcessId = (int)((UInt32)oProcessId);

            return ProcessId;
        }

        /// <summary>
        /// Will get Memory RAM and CPU usage of each service and add or update the oListMonitorDetails
        /// </summary>        
        /// <param name="Service"></param>
        /// <param name="oMonitorService"></param>
        private void CollectData(ServiceController Service, ServiceEntity oMonitorService)
        {
            int ProcessId = GetWindowsProcessIdByServiceName(Service.ServiceName);

            if (ProcessId > 0)
            {
                Process oProcess = Process.GetProcessById(ProcessId);

                MonitorDetail oMonitorItemDetail = MonitoringItems.FirstOrDefault(f => f.Name.Equals(oMonitorService.Name));

                if (oMonitorItemDetail != null)
                {
                    oMonitorItemDetail.MemoryUsedPerProcess = GetMemoryFromApp(oProcess);
                    oMonitorItemDetail.AvaibleMemoryMachine = GetMemoryFromMachine();
                    oMonitorItemDetail.CpuUsedMachine = GetCpuFromMachine();
                    oMonitorItemDetail.CpuUsedProcess = GetCpuFromApp(oProcess.ProcessName);
                    oMonitorItemDetail.Path = oProcess.MainModule.FileName;

                }
                else
                {
                    string path = "";

                    try
                    {
                        path = oProcess.MainModule.FileName;
                    }
                    catch
                    {

                    }

                    MonitoringItems.Add(new MonitorDetail()
                    {
                        Name = oMonitorService.Name,
                        IsService = true,
                        MemoryUsedPerProcess = GetMemoryFromApp(oProcess),
                        IdWatchDogItem = IdMachine,
                        CpuUsedMachine = GetCpuFromMachine(),
                        CpuUsedProcess = GetCpuFromApp(oProcess.ProcessName),
                        AvaibleMemoryMachine = GetMemoryFromMachine(),
                        Path = path
                    });
                }
            }
        }

        private bool IsServiceConfiguratedToMonitor(ServiceController Service)
        {
            return Params.GetServices().Exists(f => f.Name.Equals(Service.DisplayName) || f.Name.Equals(Service.ServiceName));
        }

        private ServiceEntity GetServiceParam(ServiceController Service)
        {
            return Params.GetServices().FirstOrDefault(f => f.Name.Equals(Service.DisplayName) || f.Name.Equals(Service.ServiceName));
        }

        private float GetCpuFromMachine()
        {            
            CpuUsage.NextValue();
            Thread.Sleep(250);

            float CpuUsageValue = CpuUsage.NextValue();            

            return CpuUsageValue;
        }

        private double GetCpuFromApp(string ProcessName)
        {
            PerformanceCounter CpuApp = new PerformanceCounter("Process", "% Processor Time", ProcessName, true);

            List<double> ListCpuApp = new List<double>(2);

            for (int i = 0; i < 2; i++)
            {
                ListCpuApp.Add(CpuApp.NextValue());
                Thread.Sleep(250);
            }

            double CpuAppResult = ListCpuApp.Max();

            CpuApp.Dispose();

            return CpuAppResult;
        }

        private float GetMemoryFromMachine()
        {            
            MemCounter.NextValue();
            Thread.Sleep(250);

            float MemValue = MemCounter.NextValue();            

            return MemValue;
        }

        private decimal GetMemoryFromApp(Process ProcessObj)
        {
            decimal MemoryMB = 0;                        

            if (ProcessObj.PagedMemorySize64 > 0)
                MemoryMB = Math.Round(Convert.ToDecimal(ProcessObj.PagedMemorySize64 / 1024) / 1024, 2);            

            return MemoryMB;
        }

    }
}
