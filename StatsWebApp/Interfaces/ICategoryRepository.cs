using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Interfaces
{
    public interface ICategoryRepository
    {
        void Save(Category category);
        void Update(Category category);
        Task<List<CategoryDto>> GetCategories();
        Task<Category> IsExistCategory(string category);
        Task<Category> IsExistCategorybyId(int categoryId);
    }
}
