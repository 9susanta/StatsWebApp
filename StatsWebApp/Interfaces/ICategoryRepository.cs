using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using StatsWebApp.Helper;
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
        Task<PagedList<CategoryDto>> GetCategories(CategoryParam param);
        Task<Category> IsExistCategory(string category);
        Task<Category> IsExistCategorybyId(int categoryId);
    }
}
