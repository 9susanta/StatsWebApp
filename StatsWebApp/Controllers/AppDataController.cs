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
    
    public class AppDataController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppDataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("save")]
        public async Task<ActionResult> Save(AppDataDto appData)
        {
            var isexist = await _unitOfWork.AppDataRepository.IsExistAppData(appData.Title);
            if (isexist != null)
            {
                return Ok("This Title is Already Exists");
            }
            AppData ap = new AppData();
            ap.Title = appData.Title;
            ap.Description = appData.Description;
            ap.excelPath = appData.excelPath;
            ap.metaData = appData.metaData;
            ap.subCategoryId = appData.subCategoryId;
            ap.jsonData = appData.jsonData;
            ap.IsDeleted = false;
         
            _unitOfWork.AppDataRepository.Save(ap);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This data is saved Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Saving");
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<AppDataDto>>> Get([FromQuery] AppDataParams appDatas)
        {

            var appData = await _unitOfWork.AppDataRepository.GetAppData(appDatas);

            Response.AddPaginationHeader(appData.CurrentPage, appData.PageSize,
                appData.TotalCount, appData.TotalPages);

            return Ok(appData);

        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(AppDataDto appDataDto)
        {
            var isexist = await _unitOfWork.AppDataRepository.IsExistAppData(appDataDto.Title);
            if (isexist != null)
            {
                return Ok("This Title is Already Exists");
            }
            var isexistbyid = await _unitOfWork.AppDataRepository.IsExistAppDatabyId(appDataDto.Id);
            isexistbyid.Title = appDataDto.Title;
            isexistbyid.Description = appDataDto.Description;
            if(!string.IsNullOrEmpty(appDataDto.excelPath))
            {
                isexistbyid.excelPath = appDataDto.excelPath;
            }
            isexistbyid.jsonData = appDataDto.jsonData;
            isexistbyid.metaData = appDataDto.metaData;
            isexistbyid.subCategoryId = appDataDto.subCategoryId;

            _unitOfWork.AppDataRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Data is Updated Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(AppDataDto appDataDto)
        {
            var isexistbyid = await _unitOfWork.AppDataRepository.IsExistAppDatabyId(appDataDto.Id);
            isexistbyid.IsDeleted = true;
            _unitOfWork.AppDataRepository.Update(isexistbyid);
            bool issucess = await _unitOfWork.Complete();
            if (issucess == true)
            {
                return Ok("This Data is Deleted Sucessfully");
            }
            else
            {
                return Ok("Encounter an error while Updating");
            }
        }


    }
}
