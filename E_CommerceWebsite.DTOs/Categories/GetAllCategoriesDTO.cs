using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DTOs.Categories
{
    public class GetAllCategoriesDTO
    {
        public int CategoriesId { get; set; }
        public string Name { get; set; }
        
        public List<GetProductsForCategoryDTO> Products { get; set; }
    }
}
