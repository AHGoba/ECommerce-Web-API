using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Models.Models
{
    public class Categories
    {
        public int CategoriesId { get; set; }
        public string Name { get; set; }
        public List<Products> Products { get; set; }


       
    }
}
