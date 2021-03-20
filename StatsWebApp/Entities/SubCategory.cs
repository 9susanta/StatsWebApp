using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWebApp.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<AppData> AppData { get; set; }
    }
}
