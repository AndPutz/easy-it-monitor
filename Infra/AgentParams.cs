using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Xml.Serialization;

namespace Infra
{
    public class AgentParams : IAgentParams
    {
        public List<ProcessParam> Processes { get; set; }

        public List<ParamEntity> Services { get; set; }

        /// <summary>
        /// Cycle Time
        /// </summary>
        public int TimerProcess { get; set; }
        /// <summary>
        /// Keep Alive Cycle Time
        /// </summary>
        public int TimerKeepAlive { get; set; }

        /// <summary>
        /// Max attemps to recovey each process and/or services
        /// </summary>
        public int MaxRecoveryAttempts { get; set; }

        public AgentParams()
        {
            Processes = new List<ProcessParam>();
            Services = new List<ParamEntity>();
        }

        public void Load()
        {           
            AgentParams oConfig = null;

            try
            {
                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(AgentParams));

                try
                {
                    String sFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\AgentParams.xml";

                    using (StreamReader oStreamReader = new StreamReader(sFilePath))
                    {

                        oConfig = (AgentParams)oXmlSerializer.Deserialize(oStreamReader);

                        this.MaxRecoveryAttempts = oConfig.MaxRecoveryAttempts;
                        this.Processes = oConfig.Processes;
                        this.Services = oConfig.Services;
                        this.TimerKeepAlive = oConfig.TimerKeepAlive;
                        this.TimerProcess = oConfig.TimerProcess;                        
                    }

                }
                catch(Exception)
                {
                    InitConfig();
                }
            }
            catch(Exception)
            {
                InitConfig();
            }

            
        }

        public int GetMaxRecoveryAttempts()
        {
            return MaxRecoveryAttempts;
        }

        public List<ParamEntity> GetServices()
        {
            return Services;
        }

        public List<ProcessParam> GetProcesses()
        {
            return Processes;
        }

        public int GetTimerProcess()
        {
            return TimerProcess;
        }

        public int GetTimerKeepAlive()
        {
            return TimerKeepAlive;
        }

        public void Save()
        {
            String sFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\AgentParams.xml";

            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(AgentParams));

            using (StreamWriter oTextWriter = new StreamWriter(sFilePath))
            {

                oXmlSerializer.Serialize(oTextWriter, this);
            }
        }

        public bool HasServicesParam()
        {
            if (Services != null && Services.Count > 0)
                return true;
            else
                return false;
        }

        public bool HasProcessesParam()
        {
            if (Processes != null && Processes.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Initialize example of Config File with a discovery of all services and process that are running.
        /// </summary>
        private void InitConfig()
        {            
            ServiceController[] ListServices = ServiceController.GetServices();

            Process[] ListProcesses = Process.GetProcesses();

            Services = new List<ParamEntity>();
            Processes = new List<ProcessParam>();
            MaxRecoveryAttempts = 3;
            TimerKeepAlive = 62000;
            TimerProcess = 5000;

            foreach (ServiceController _Service in ListServices)
            {
                ParamEntity serviceEntity = new ParamEntity(_Service.DisplayName, 0);

                if (Services.Contains(serviceEntity) == false)
                    Services.Add(serviceEntity);
            }

            foreach (Process _Process in ListProcesses)
            {
                ProcessParam processEntity = new ProcessParam(_Process.ProcessName, 0, string.Empty);

                if (Processes.Contains(processEntity) == false)
                    Processes.Add(processEntity);
            }

            Save();            

            //TODO: Send to Web Api discovery services and process

        }
        
    }
}
