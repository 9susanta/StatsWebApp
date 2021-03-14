using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Interfaces
{
    public interface ITokenService
    {
       Task<string> CreateToken(AppUser user);
    }
}
