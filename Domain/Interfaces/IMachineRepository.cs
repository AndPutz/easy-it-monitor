using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetMachinesAsync();

        Task<Machine> GetMachineByIdAsync(int? id);

        Task<IEnumerable<Machine>> GetMachinesByNameAsync(string name);

        Task<Machine> CreateAsync(Machine machine);

        Task<Machine> UpdateAsync(Machine machine);

        Task<Machine> RemoveAsync(Machine machine);
    }
}
