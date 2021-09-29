using System;
using Domain.Service.UseCases;
using Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AgentWatchDogTest
    {
        private AgentParams agentParams = null;
        private AlertHelper alert = null;
        private WatchDogService watchDogService = null;
        private WatchDogProcess watchDogProcess = null;

        public AgentWatchDogTest()
        {
            agentParams = new AgentParams();
            alert = new AlertHelper();

            watchDogService = new WatchDogService(agentParams, alert);
            watchDogProcess = new WatchDogProcess(agentParams, alert);
        }

        [TestMethod]
        public void ServiceStart()
        {                        
            if (watchDogService.Params == null)
                Assert.Fail("Params not loaded");

            if (watchDogService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WatchDogMonitoring()
        {
            watchDogService.Monitoring();

            watchDogProcess.Monitoring();

            Assert.IsTrue(true);
        }       
    }
}
