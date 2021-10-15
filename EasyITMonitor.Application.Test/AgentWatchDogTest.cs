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
    public class AgentWatchDogTest
    {
        [Fact]
        public void ServiceWatchDogStart_WithValidParameters_ResultValidState()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();            

            Action action = () => new WatchDogService(agentParams, alert);

            action.Should().NotThrow<Exception>();                        
        }

        [Fact]
        public void ProcessWatchDogStart_WithValidParameters_ResultValidState()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();

            Action action = () => new WatchDogProcess(agentParams, alert);

            action.Should().NotThrow<Exception>();           
        }        

        [Fact]
        public void WatchDogServiceMonitoring()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();
            WatchDogService watchDogService = new WatchDogService(agentParams, alert);

            Action action = () => watchDogService.Monitoring();

            action.Should().NotThrow<Exception>();
        }

        [Fact]
        public void WatchDogProcessMonitoring()
        {
            AgentParams agentParams = new AgentParams();
            AlertHelper alert = new AlertHelper();
            WatchDogProcess watchDogProcess = new WatchDogProcess(agentParams, alert);

            Action action = () => watchDogProcess.Monitoring();

            action.Should().NotThrow<Exception>();
        }
    }
}
