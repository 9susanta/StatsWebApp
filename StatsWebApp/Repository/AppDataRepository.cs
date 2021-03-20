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
    public class AppDataRepository : IAppDataRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AppDataRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PagedList<AppDataDto>> GetAppData(AppDataParams param)
        {
            var query = _context.AppData.AsQueryable();
            query = query.Where(x => x.IsDeleted == false);

            var querycategories = _context.Category.AsQueryable();
            querycategories = querycategories.Where(x => x.IsDeleted == false);

            var querysubcategory = _context.SubCategories.AsQueryable();
            querysubcategory = querysubcategory.Where(x => x.IsDeleted == false);

            querysubcategory.Join(querycategories, subcat=> subcat.Id,cat=>cat.CategoryId,(cat,subcat)=>new { cat,subcat});

            query.Join(querysubcategory, qur => qur.subCategoryId, subcat => subcat.CategoryId, (qur, subcat) => new { qur, subcat });

            var data = query.Select(sc => new AppDataDto
            {
                Id = sc.Id,
                Title=sc.Title,
                Description=sc.Description,
                metaData=sc.metaData,
                jsonData=sc.jsonData,
                excelPath=sc.excelPath,
                subCategoryId=sc.subCategoryId,
                subCategory=sc.subCategory.SubCategoryName,
                category=sc.subCategory.Category.CategoryName
            });

            return await PagedList<AppDataDto>.CreateAsync(data,
                param.PageNumber, param.PageSize);
        }

        public async Task<AppData> IsExistAppData(string title)
        {
            return await _context.AppData.Where(x => x.Title == title && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<AppData> IsExistAppDatabyId(int appDataId)
        {
            return await _context.AppData.Where(x => x.Id == appDataId && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public void Save(AppData appData)
        {
            _context.AppData.Add(appData);
        }

        public void Update(AppData appData)
        {
            _context.Entry(appData).State = EntityState.Modified;
        }
    }
}
