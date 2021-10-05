using System.Collections.Generic;

namespace Domain.Entities
{
    public sealed class CategoryServices : CategoryEntity
    {
        public CategoryServices(string name, ICollection<ServiceEntity> services) : base(name)
        {
            Services = services;
        }

        public CategoryServices(int id, string name, ICollection<ServiceEntity> services) : base(id, name)
        {
            Services = services;
        }

        public ICollection<ServiceEntity> Services { get; set; }
    }
}
