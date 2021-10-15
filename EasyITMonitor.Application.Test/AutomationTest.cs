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
    public class AutomationTest
    {
        [Fact]
        public void DeleteTempFiles()
        {
            AlertHelper alertHelper = new AlertHelper();
            Automation automation = new Automation(alertHelper);
            
            Action action = () => automation.DeleteTempFiles();

            action.Should().NotThrow<DomainExceptionValidation>();                        
        }
    }
}
