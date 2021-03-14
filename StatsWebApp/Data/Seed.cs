using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StatsWebApp.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext dataContext)
        {
            var roleExist = dataContext.Roles.FirstOrDefault();
            if (roleExist == null)
            {
                var roles = new List<Roles>
                {
                    new Roles { Role = "Admin",IsDeleted=false },
                    new Roles { Role = "Member",IsDeleted=false },
                    new Roles { Role = "Moderator",IsDeleted=false },
                };

                await dataContext.Roles.AddRangeAsync(roles);
                await dataContext.SaveChangesAsync();
            }
            var userExist= dataContext.Users.FirstOrDefault();
            if(userExist==null)
            {
                using var hmac = new HMACSHA512();
                var user = new AppUser { FullName="Susanta Rout",UserName="SusantaR", IsDeleted=false,PasswordHash= hmac.ComputeHash(Encoding.UTF8.GetBytes("Skr@1234")),PasswordSalt = hmac.Key };

                await dataContext.Users.AddAsync(user);
                await dataContext.SaveChangesAsync();

                await dataContext.UserRoles.AddAsync(new UserRoles { RoleId=1,UserId=1});
                await dataContext.SaveChangesAsync();
               
            }
        }
    }
}
