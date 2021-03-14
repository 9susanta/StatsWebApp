using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Entities
{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public Roles Role { get; set; }
        public int RoleId { get; set; }
    }
}
