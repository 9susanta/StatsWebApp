using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using StatsWebApp.Extensions;
using StatsWebApp.Helper;
using StatsWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("save")]
        public async Task<ActionResult> Save(CategoryDto category)
        {
            var isexist = await _unitOfWork.CategoryRepository.IsExistCategory(category.CategoryName);
            if(isexist!=null)
            {
                return Ok("This Category is Already Exists");
            }
            Category cat = new Category();
            cat.CategoryName = category.CategoryName;
            cat.IsDeleted = false;
            _unitOfWork.CategoryRepository.Save(cat);
            bool issucess= await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Category is Saved Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Saving");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery]CategoryParam cateParams)
        {
           
            var categories = await _unitOfWork.CategoryRepository.GetCategories(cateParams);

            Response.AddPaginationHeader(categories.CurrentPage, categories.PageSize,
                categories.TotalCount, categories.TotalPages);

            return Ok(categories);

        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(CategoryDto category)
        {
            var isexist = await _unitOfWork.CategoryRepository.IsExistCategory(category.CategoryName);
            if (isexist != null)
            {
                return Ok("This Category is Already Exists");
            }
            var isexistbyid = await _unitOfWork.CategoryRepository.IsExistCategorybyId(category.CategoryId);
            isexistbyid.CategoryName = category.CategoryName;
            _unitOfWork.CategoryRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Category is Updated Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(CategoryDto category)
        {
            var isexistbyid = await _unitOfWork.CategoryRepository.IsExistCategorybyId(category.CategoryId);
            isexistbyid.IsDeleted = true;
            _unitOfWork.CategoryRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Category is Deleted Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }
    }
}
