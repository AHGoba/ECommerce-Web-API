using E_CommerceWebsite.DTOs.Categories;
using E_CommerceWebsite.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_CommerceWebsite.DTOs.Products
{
    public class AddOrDeleteProductsDTO
    {
        public string Name { get; set;}
        public string Description { get; set;}
            
        public int Price { get; set; }

        public int CategoriesId { get; set; }

    }
}
