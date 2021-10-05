using System.Collections.Generic;

namespace Domain.Entities
{
    public sealed class CategoryMachines : CategoryEntity
    {
        public CategoryMachines(string name, ICollection<Machine> machines) : base (name)
        {
            Machines = machines;
        }

        public CategoryMachines(int id, string name, ICollection<Machine> machines) : base(id, name)
        {
            Machines = machines;
        }

        public ICollection<Machine> Machines { get; private set; }

    }
}
