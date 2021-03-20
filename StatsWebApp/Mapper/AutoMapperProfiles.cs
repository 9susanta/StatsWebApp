using AutoMapper;
using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<SubCategory,SubCategoryDto>();
        }
    }
}
