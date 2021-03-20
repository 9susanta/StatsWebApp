using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StatsWebApp.Data;
using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using StatsWebApp.Helper;
using StatsWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Repository
{
    //[Authorize]
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SubCategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<SubCategoryDto>> GetSubCategories(SubCategoryParam param)
        {
            var query = _context.SubCategories.AsQueryable();
            query = query.Where(x => x.IsDeleted == false);
            var subquery = _context.Category.AsQueryable();
            subquery = subquery.Where(x => x.IsDeleted == false);
            if (param.ShowCategory > 0)
            {
                query = query.Where(x => x.CategoryId == param.ShowCategory);
            }
            query.Join(subquery, cat => cat.CategoryId, subcat => subcat.CategoryId, (cat, subcat) => new { cat, subcat });

            var subcat = query.Select(sc => new SubCategoryDto
            {
                CategoryId = sc.CategoryId,
                Id = sc.Id,
                CategoryName=sc.Category.CategoryName,
                SubCategoryName=sc.SubCategoryName
            });

            return await PagedList<SubCategoryDto>.CreateAsync(subcat,
                param.PageNumber, param.PageSize);
        }

        public async Task<SubCategory> IsExistSubCategory(string Subcategory)
        {
            return await _context.SubCategories.Where(x => x.SubCategoryName == Subcategory && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<SubCategory> IsExistubCategorybyId(int SubcategoryId)
        {
            return await _context.SubCategories.Where(x => x.Id == SubcategoryId && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public void Save(SubCategory subcategory)
        {
            _context.SubCategories.Add(subcategory);
        }

        public void Update(SubCategory subcategory)
        {
            _context.Entry(subcategory).State = EntityState.Modified;
        }
    }
}
