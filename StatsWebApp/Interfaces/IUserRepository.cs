using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Roles>> GetRoleByIdAsync(int id);
        Task<AppUser> UserByUsername(string username);
    }
}
