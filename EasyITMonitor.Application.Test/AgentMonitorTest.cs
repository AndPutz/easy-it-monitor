using Domain.DTO;
using Domain.Entities;
using Domain.UseCases;
using Domain.Validation;
using FluentAssertions;
using Infra;
using System;
using Xunit;

namespace EasyITMonitor.Application.Test
{
    public class AgentMonitorTest
    {
        [Fact]
        public void ServiceMonitorStart_WithValidParameters_ResultValidState()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();
            Access access = new Access();

            Action action = () => new MonitorService(agentParams, alert, access, new MachineData(agentParams));

            action.Should().NotThrow<DomainExceptionValidation>();                        
        }

        [Fact]
        public void ProcessMonitorStart_WithValidParameters_ResultValidState()
        {
            AgentParams agentParams = new AgentParams();            
            Access access = new Access();

            Action action = () => new MonitorProcess(agentParams, access, new MachineData(agentParams));

            action.Should().NotThrow<DomainExceptionValidation>();           
        }

        [Fact]
        public void MachineDataCollect()
        {
            AgentParams agentParams = new AgentParams();
            Access access = new Access();

            MachineData _MachineData = new MachineData(agentParams);
            DTOMonitor DTO = new DTOMonitor(access);

            Machine _Machine = _MachineData.CollectData();
     
            Action action = () => DTO.SaveMachine(_Machine);

            action.Should().NotThrow<Exception>();
        }

        [Fact]
        public void MonitorService()
        {
            AgentParams agentParams = new AgentParams();
            Access access = new Access();
            AlertHelper alert = new AlertHelper();

            MonitorService monitorService = new MonitorService(agentParams, alert, access, new MachineData(agentParams));           

            Action action = () => monitorService.Monitoring();

            action.Should().NotThrow<Exception>();
        }

        [Fact]
        public void MonitorProcess()
        {
            AgentParams agentParams = new AgentParams();
            Access access = new Access();            

            MonitorProcess monitorProcess = new MonitorProcess(agentParams, access, new MachineData(agentParams));

            monitorProcess.Monitoring();

            Action action = () => monitorProcess.Monitoring();

            action.Should().NotThrow<Exception>();
        }
    }
}
