using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZM.Entities
{
    public class Category
    {
        public Guid CategoriesId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }

        public Guid? ParencategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
