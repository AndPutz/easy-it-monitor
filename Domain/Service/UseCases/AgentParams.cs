using Domain.Service.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Service.UseCases
{
    public class AgentParams
    {
        public List<ProcessEntity> Processes { get; set; }

        public List<ServiceEntity> Services { get; set; }

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
            Processes = new List<ProcessEntity>();
            Services = new List<ServiceEntity>();
        }

        public static AgentParams Load()
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
                    }

                }
                catch
                {
                    InitConfig();
                }
            }
            catch
            {
                InitConfig();
            }

            return oConfig;
        }

        public static void Save(AgentParams oClassConfig)
        {
            String sFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\AgentParams.xml";

            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(AgentParams));

            using (StreamWriter oTextWriter = new StreamWriter(sFilePath))
            {

                oXmlSerializer.Serialize(oTextWriter, oClassConfig);
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
        private static void InitConfig()
        {
            AgentParams oConfig = new AgentParams();

            ServiceController[] oListServices = ServiceController.GetServices();

            Process[] oListProcesses = Process.GetProcesses();

            oConfig.Services = new List<ServiceEntity>();
            oConfig.Processes = new List<ProcessEntity>();
            oConfig.MaxRecoveryAttempts = 3;
            oConfig.TimerKeepAlive = 62000;
            oConfig.TimerProcess = 5000;

            foreach (ServiceController oService in oListServices)
            {
                oConfig.Services.Add(new ServiceEntity() { Name = oService.DisplayName });
            }

            foreach (Process oProcess in oListProcesses)
            {
                oConfig.Processes.Add(new ProcessEntity() { Name = oProcess.ProcessName, Detail = "" });
            }

            Save(oConfig);

        }
    }
}
