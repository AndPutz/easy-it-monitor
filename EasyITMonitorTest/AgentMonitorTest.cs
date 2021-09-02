using System;
using Domain.Service.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AgentMonitorTest
    {
        [TestMethod]
        public void ServiceStart()
        {
            MonitorService monitorService = new MonitorService();
            MonitorProcess monitorProcess = new MonitorProcess();

            if (monitorService.Params == null)
                Assert.Fail("Params not loaded");

            if (monitorService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void MonitorService()
        {
            MonitorService monitorService = new MonitorService();            

            monitorService.Monitoring();

            Assert.IsTrue(monitorService.MonitoringItems.Count > 0);
        }

        public void MonitorProcess()
        {
            MonitorProcess monitorProcess = new MonitorProcess();

            monitorProcess.Monitoring();

            Assert.IsTrue(monitorProcess.MonitoringItems.Count > 0);
        }
    }
}
