using E_CommerceWebsite.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceWebsite.Models.Models;

namespace E_CommerceWebsite.DTOs.Products
{
    public class GetAllProductsDTO
    {
        public int ProductsID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        
        public int CategoriesId { get; set; }
        //public E_CommerceWebsite.Models.Models.Categories Categories12 { get; set; }


    }
}
