using Microsoft.EntityFrameworkCore;
using StatsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
