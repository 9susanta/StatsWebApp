using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Entities
{
    public class AppData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string metaData { get; set; }
        public string excelPath { get; set; }
        public string jsonData { get; set; }
        public int subCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public SubCategory subCategory { get; set; }
    }
}
