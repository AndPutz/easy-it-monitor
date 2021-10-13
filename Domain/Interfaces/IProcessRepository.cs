using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProcessRepository
    {
        Task<IEnumerable<ProcessEntity>> GetProcessesAsync();

        Task<ProcessEntity> GetProcessByIdAsync(int? id);

        Task<IEnumerable<ProcessEntity>> GetProcessesByNameAsync(string name);

        Task<ProcessEntity> CreateAsync(ProcessEntity process);

        Task<ProcessEntity> UpdateAsync(ProcessEntity process);

        Task<ProcessEntity> RemoveAsync(ProcessEntity process);
    }
}
