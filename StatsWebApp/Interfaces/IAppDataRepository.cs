using StatsWebApp.DTOs;
using StatsWebApp.Entities;
using StatsWebApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Interfaces
{
    public interface IAppDataRepository
    {
        void Save(AppData appData);
        void Update(AppData appData);
        Task<PagedList<AppDataDto>> GetAppData(AppDataParams param);
        Task<AppData> IsExistAppData(string Title);
        Task<AppData> IsExistAppDatabyId(int appDataId);
    }
}
