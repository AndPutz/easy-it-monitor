using System;
using Domain.Service.UseCases;
using Infra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyITMonitorTest
{
    [TestClass]
    public class AutomationTest
    {
        [TestMethod]
        public void DeleteTempFiles()
        {
            AlertHelper alertHelper = new AlertHelper();
            Automation automation = new Automation(alertHelper);
            automation.DeleteTempFiles();

            Assert.IsTrue(true);
        }
    }
}
