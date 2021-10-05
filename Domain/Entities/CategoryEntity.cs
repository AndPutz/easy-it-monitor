using Domain.Validation;
using System;

namespace Domain.Entities
{
    public class CategoryEntity : Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public CategoryEntity(string name)
        {
            ValidateDomain(name);
        }

        public CategoryEntity(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id Value!");

            Id = id;

            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), 
                "Name is required!");

            DomainExceptionValidation.When(name.Length <= 3, 
                "Invalid Name, too short!");

            Name = name;
        }
    }
}
