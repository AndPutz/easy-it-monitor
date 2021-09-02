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
                Assert.Fail("Params dont has Services");

            Assert.IsTrue(true);
        }
    }
}
