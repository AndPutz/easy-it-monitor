using System;
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
            MonitorService monitorService = new MonitorService(agentParams, alert, access);
            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access);

            if (monitorService.Params == null)
                Assert.Fail("Params not loaded");

            if (monitorService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void MonitorService()
        {
            MonitorService monitorService = new MonitorService(agentParams, alert, access);            

            monitorService.Monitoring();

            Assert.IsTrue(monitorService.MonitoringItems.Count > 0);
        }

        public void MonitorProcess()
        {
            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access);

            monitorProcess.Monitoring();

            Assert.IsTrue(monitorProcess.MonitoringItems.Count > 0);
        }
    }
}
