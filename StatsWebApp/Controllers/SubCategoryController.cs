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
    public class SubCategoryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("save")]
        public async Task<ActionResult> Save(SubCategoryDto subcategory)
        {
            var isexist = await _unitOfWork.SubCategoryRepository.IsExistSubCategory(subcategory.SubCategoryName);
            if (isexist != null)
            {
                return Ok("This Subcategory is Already Exists");
            }
            SubCategory subcat = new SubCategory();
            subcat.SubCategoryName = subcategory.SubCategoryName;
            subcat.IsDeleted = false;
            subcat.CategoryId = subcategory.CategoryId;
            _unitOfWork.SubCategoryRepository.Save(subcat);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Subcategory is Saved Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Saving");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> Get([FromQuery] SubCategoryParam subcateParams)
        {

            var subcategories = await _unitOfWork.SubCategoryRepository.GetSubCategories(subcateParams);

            Response.AddPaginationHeader(subcategories.CurrentPage, subcategories.PageSize,
                subcategories.TotalCount, subcategories.TotalPages);

            return Ok(subcategories);

        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(SubCategoryDto subcategory)
        {
            var isexist = await _unitOfWork.SubCategoryRepository.IsExistSubCategory(subcategory.SubCategoryName);
            if (isexist != null)
            {
                return Ok("This Subcategory is Already Exists");
            }
            var isexistbyid = await _unitOfWork.SubCategoryRepository.IsExistubCategorybyId(subcategory.Id);
            isexistbyid.SubCategoryName = subcategory.SubCategoryName;
            isexistbyid.CategoryId = subcategory.CategoryId;
            _unitOfWork.SubCategoryRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Subcategory is Updated Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(SubCategoryDto subcategory)
        {
            var isexistbyid = await _unitOfWork.SubCategoryRepository.IsExistubCategorybyId(subcategory.CategoryId);
            isexistbyid.IsDeleted = true;
            _unitOfWork.SubCategoryRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Subcategory is Deleted Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }
    }
}
