using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace EasyITMonitor.Domain.Test
{
    public class CategoryMachineUnitTest
    {
        [Fact]
        public void CreateMachineCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new CategoryMachines("Main Floor", null);

            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateMachineCategory_WithInvalidParameters_DomainExceptionInvalidName()
        {
            Action action = () => new CategoryMachines("Ma", null);

            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Invalid Name, too short!");
        }

        [Fact]
        public void CreateMachineCategory_MissingNameParameters_DomainExceptionRequiredName()
        {
            Action action = () => new CategoryMachines("", null);

            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Name is required!");
        }

        [Fact]
        public void CreateMachineCategory_NullNameParameters_DomainExceptionInvalidName()
        {
            Action action = () => new CategoryMachines(null, null);

            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Name is required!");
        }

        [Fact]
        public void CreateMachineCategory_NegativeIdParameters_DomainExceptionInvalidId()
        {
            Action action = () => new CategoryMachines(-1, "Main Floor", null);

            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Invalid Id Value!");
        }
    }
}
