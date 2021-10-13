using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ServiceEntity>> GetServicesAsync();

        Task<ServiceEntity> GetServiceByIdAsync(int? id);

        Task<IEnumerable<ServiceEntity>> GetServicesByNameAsync(string name);

        Task<ServiceEntity> CreateAsync(ServiceEntity service);

        Task<ServiceEntity> UpdateAsync(ServiceEntity service);

        Task<ServiceEntity> RemoveAsync(ServiceEntity service);
    }
}
