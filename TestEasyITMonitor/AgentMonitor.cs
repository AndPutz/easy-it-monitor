using Domain.Service.UseCases;
using Infra;
using System;
using Xunit;

namespace TestEasyITMonitor
{
    public class AgentMonitor
    {
        private AgentParams agentParams = null;
        private AlertHelper alert = null;
        private Access access = null;

        [Fact(DisplayName ="Monitor Service Start")]
        public void Monitor_ServiceStart_ConfigLoaded()
        {
            //Arrange
            agentParams = new AgentParams();
            alert = new AlertHelper();
            access = new Access();

            //Act
            MonitorService monitorService = new MonitorService(agentParams, alert, access);
            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access);

            //Assert            
            Assert.NotNull(monitorService.Params);
            Assert.True(monitorService.Params.HasServicesParam());            
        }
    }
}
