using System;
using Domain.Service.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AutomationTest
    {
        [TestMethod]
        public void DeleteTempFiles()
        {
            Automation automation = new Automation();
            automation.DeleteTempFiles();

            Assert.IsTrue(true);
        }
    }
}
