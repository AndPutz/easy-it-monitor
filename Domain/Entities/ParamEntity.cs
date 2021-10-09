using Domain.Validation;
using System;

namespace Domain.Entities
{
    public class ParamEntity : Entity, IComparable
    {
        public string Name { get; set; }

        /// <summary>
        /// Cycle Time of each item to monitor
        /// If its zero (0) will use the KeepAlive timer set to Agent Monitor
        /// </summary>
        public int CycleTime { get; set; }

        public ParamEntity()
        {

        }

        public ParamEntity(string name, int cycleTime)
        {
            ValidateDomain(name, cycleTime);
        }
        
        public void Update(string name, int cycleTime)
        {
            ValidateDomain(name, cycleTime);
        }

        private void ValidateDomain(string name, int cycleTime)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Name is required!");

            DomainExceptionValidation.When(name.Length <= 2,
                "Invalid Name (" + name + "), too short!");

            Name = name;

            DomainExceptionValidation.When(cycleTime < 0,
                "Cycle Time must be greater or equal than 0");

            CycleTime = cycleTime;
        }

        public virtual int CompareTo(object obj)
        {
            return Name.CompareTo((obj as ParamEntity).Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj as ParamEntity == null)
                return false;
            if ((obj as ParamEntity).Name == "")
                return base.Equals(obj);

            return (obj as ParamEntity).Name == Name;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Name.GetHashCode();
        }
    }
}
