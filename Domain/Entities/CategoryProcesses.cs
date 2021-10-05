using System.Collections.Generic;

namespace Domain.Entities
{
    public sealed class CategoryProcesses : CategoryEntity
    {
        public CategoryProcesses(string name, ICollection<ProcessEntity> processes) : base(name)
        {
            Processes = processes;
        }

        public CategoryProcesses(int id, string name, ICollection<ProcessEntity> processes) : base(id, name)
        {
            Processes = processes;
        }

        public ICollection<ProcessEntity> Processes { get; set; }
    }
}
