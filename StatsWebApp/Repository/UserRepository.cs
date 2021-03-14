using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StatsWebApp.Data;
using StatsWebApp.Entities;
using StatsWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Roles>> GetRoleByIdAsync(int id)
        {
            var query = (from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.RoleId
                        where ur.UserId==id
                        select r).ToListAsync();

            return await query;
        }
        public async Task<AppUser> UserByUsername(string username)
        {
            return await _context.Users.Where(x => x.UserName.ToLower() == username).FirstOrDefaultAsync();
        }
    }
}
