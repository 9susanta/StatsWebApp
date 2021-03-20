using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.DTOs
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
