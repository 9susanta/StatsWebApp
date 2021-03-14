using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StatsWebApp.Entities;
using StatsWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StatsWebApp.Services
{
    public class TokenService:ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IUnitOfWork _userRepository;

        public TokenService(IConfiguration config, IUnitOfWork userRepository)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ZXCVBNMLKJHGFDSAQWERTYUIOPqwertyuioplkjhgfdsazxcvbnm"));
            _userRepository = userRepository;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };
            var roles = await _userRepository.UserRepository.GetRoleByIdAsync(user.UserId);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role.Role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
