using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using StatsWebApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Interfaces
{
    public interface ISubCategoryRepository
    {
        void Save(SubCategory category);
        void Update(SubCategory category);
        Task<PagedList<SubCategoryDto>> GetSubCategories(SubCategoryParam param);
        Task<SubCategory> IsExistSubCategory(string Subcategory);
        Task<SubCategory> IsExistubCategorybyId(int SubcategoryId);
    }
}
