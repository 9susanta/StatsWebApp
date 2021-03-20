using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.DTOs
{
    public class AppDataDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string metaData { get; set; }
        [Required]
        public string excelPath { get; set; }
        public string jsonData { get; set; }
        [Required]
        public int subCategoryId { get; set; }
        public string subCategory { get; set; }
        public string category {get;set;}
    }
}
