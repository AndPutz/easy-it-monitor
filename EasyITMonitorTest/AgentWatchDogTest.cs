using System;
using Domain.Service.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AgentWatchDogTest
    {
        [TestMethod]
        public void ServiceStart()
        {
            WatchDogService watchDogService = new WatchDogService();
            WatchDogProcess watchDogProcess = new WatchDogProcess();

            if (watchDogService.Params == null)
                Assert.Fail("Params not loaded");

            if (watchDogService.Params.HasServicesParam() == false)
                Assert.Fail("Params haven't Services");

            Assert.IsTrue(true);
        }
    }
}
