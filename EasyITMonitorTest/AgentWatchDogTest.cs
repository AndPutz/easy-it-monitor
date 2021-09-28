using System;
using Domain.Service.UseCases;
using Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AgentWatchDogTest
    {
        [TestMethod]
        public void ServiceStart()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();

            WatchDogService watchDogService = new WatchDogService(agentParams, alert);
            WatchDogProcess watchDogProcess = new WatchDogProcess(agentParams, alert);

            if (watchDogService.Params == null)
                Assert.Fail("Params not loaded");

            if (watchDogService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }
    }
}
