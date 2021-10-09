using System;
using Domain.DTO;
using Domain.Entities;
using Domain.UseCases;
using Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AgentMonitorTest
    {
        private AgentParams agentParams = null;
        private AlertHelper alert = null;
        private Access access = null;

        public AgentMonitorTest()
        {
            agentParams = new AgentParams();
            alert = new AlertHelper();
            access = new Access();
        }

        [TestMethod]
        public void ServiceStart()
        {            
            MonitorService monitorService = new MonitorService(agentParams, alert, access, new MachineData(agentParams));
            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access, new MachineData(agentParams));

            if (monitorService.Params == null)
                Assert.Fail("Params not loaded");

            if (monitorService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void MachineDataCollect()
        {
            MachineData _MachineData = new MachineData(agentParams);
            DTOMonitor DTO = new DTOMonitor(access);

            Machine _Machine = _MachineData.CollectData();            
            
            DTO.SaveMachine(_Machine);
        }

        [TestMethod]
        public void MonitorService()
        {
            MonitorService monitorService = new MonitorService(agentParams, alert, access, new MachineData(agentParams));            

            monitorService.Monitoring();

            Assert.IsTrue(monitorService.MonitoringItems.Count > 0);
        }

        [TestMethod]
        public void MonitorProcess()
        {
            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access, new MachineData(agentParams));

            monitorProcess.Monitoring();

            Assert.IsTrue(monitorProcess.MonitoringItems.Count > 0);
        }
    }
}
