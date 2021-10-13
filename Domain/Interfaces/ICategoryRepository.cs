using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetCategoriesAsync();

        Task<CategoryEntity> GetCategoryByIdAsync(int? id);

        Task<IEnumerable<CategoryEntity>> GetCategoriesByNameAsync(string name);

        Task<CategoryEntity> CreateAsync(CategoryEntity category);

        Task<CategoryEntity> UpdateAsync(CategoryEntity category);

        Task<CategoryEntity> RemoveAsync(CategoryEntity category);
    }
}
