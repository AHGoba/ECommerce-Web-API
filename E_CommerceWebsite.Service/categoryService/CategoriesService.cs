using E_CommerceWebsite.DTOs.Categories;
using E_CommerceWebsite.DTOs.Orders;
using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Models.Models;
using E_CommerceWebsite.Repositories.HighLevel;
using E_CommerceWebsite.Repository;
using Goba.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Service.categoryService
{
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository categoriesRepository;
        public CategoriesService(ICategoriesRepository _categoriesRepository)
        {
            categoriesRepository = _categoriesRepository;
        }

        //////////////////////////////////////// GetAll
        ///
        public List<CategoryDTO> GetAll()
        {
            var Query = categoriesRepository.GetAll().Select(ca => new GetAllCategoriesDTO
            {
                CategoriesId = ca.CategoriesId,
                Name = ca.Name,
                Products = (List<GetProductsForCategoryDTO>)ca.Products.Select(p => new GetProductsForCategoryDTO
                {
                    Name = p.Name,
                    ProductsID = p.ProductsID,
                })
            }).ToList();
            var categoryDTOs = Query.Select(ca => new CategoryDTO
            {
                CategoriesId= ca.CategoriesId,
                Name = ca.Name
            }).ToList();
            //return Query;
            return categoryDTOs;
        }

        //////////////////////////////////////// GetOne
        ///
        public List<GetAllCategoriesDTO> GetOne(int id)
        {
            var Query = categoriesRepository.GetAll().Where(c => c.CategoriesId == id)
                .Select(ca => new GetAllCategoriesDTO
                {
                    CategoriesId = ca.CategoriesId,
                    Name = ca.Name,
                    Products = (List<GetProductsForCategoryDTO>)ca.Products.Select(p => new GetProductsForCategoryDTO
                    {
                        Name = p.Name,
                        ProductsID = p.ProductsID,
                    })
                }).ToList();
            return Query;
        }


        //////////////////////////////////////// Create category
        ///

        public bool Create(AddCategoryDTO category)
        {
            Categories categories = new Categories()
            {
                Name = category.Name
            };
            categoriesRepository.Create(categories);
            return true;
        }



        //////////////////////////////////////// Update Existing category
        ///

        public bool Update(AddCategoryDTO category, int id)
        {
            // check existing
            var categoryToUpdate = categoriesRepository.GetAll().Where(cat => cat.CategoriesId == id)
                .FirstOrDefault();
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoriesRepository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        //////////////////////////////////////// Delete Existing category
        ///

        public bool Delete(int id)
        {
            var categoryToDelete = categoriesRepository.GetAll().Where(ca => ca.CategoriesId == id)
                .FirstOrDefault();//Here the tracker track him
            if (categoryToDelete != null)
            {

                categoriesRepository.Delete(categoryToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
