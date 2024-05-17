using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Service.productService
{
    public interface IProductService
    {
        List<GetAllProductsDTO> GetAll();
        List<GetAllProductsDTO> GetOne(int id);
        Products Create(AddOrDeleteProductsDTO _product);
        bool Update(AddOrDeleteProductsDTO _product, int id);
        bool Delete(int id);
    }
}
