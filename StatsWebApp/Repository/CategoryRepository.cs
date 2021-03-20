using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<CategoryDto>> GetCategories(CategoryParam param)
        {
            var query = _context.Category.AsQueryable();
                query = query.Where(x => x.IsDeleted == false);

            return await PagedList<CategoryDto>.CreateAsync(query.ProjectTo<CategoryDto>(_mapper
                .ConfigurationProvider).AsNoTracking(),
                param.PageNumber, param.PageSize);
        }

        public void Save(Category category)
        {
           _context.Category.Add(category);
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

        public async Task<Category> IsExistCategory(string category)
        {
            return await _context.Category.Where(x => x.CategoryName == category && x.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<Category> IsExistCategorybyId(int categoryId)
        {
            return await _context.Category.Where(x => x.CategoryId == categoryId && x.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
