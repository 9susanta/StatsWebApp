using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Entities
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
