using E_CommerceWebsite.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Service.categoryService
{
    public interface ICategoriesService
    {
        List<CategoryDTO> GetAll();
        List<GetAllCategoriesDTO> GetOne(int id);
        bool Create(AddCategoryDTO category);
        bool Update(AddCategoryDTO category, int id);
        bool Delete(int id);
    }
}
