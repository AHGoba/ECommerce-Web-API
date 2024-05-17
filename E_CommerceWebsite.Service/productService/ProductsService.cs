using E_CommerceWebsite.Models.Models;
using E_CommerceWebsite.Repositories.HighLevel;
using E_CommerceWebsite.Repository;
using E_CommerceWebsite.DTOs;
using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.DTOs.Categories;
using Goba.Repository;

namespace E_CommerceWebsite.Service.productService
{
    public class ProductsService : IProductService
    {
        IProductsRepository ProductsRepository;  //dealing with high level of repository
        ICategoriesRepository CategoriesRepository;
        public ProductsService(IProductsRepository _productsRepository, ICategoriesRepository _categoriesRepository)
        {
            ProductsRepository = _productsRepository; //here when i'm creating obeject from service 
            CategoriesRepository = _categoriesRepository; //i will gave it object from (Repository that take
                                                          //an object from(context))
        }



        //////////////////////////////////////// GetAll
        ///

        public List<GetAllProductsDTO> GetAll()
        {
            var Query = ProductsRepository.GetAll().Select(p => new GetAllProductsDTO
            {
                ProductsID = p.ProductsID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoriesId = p.Categories.CategoriesId     //زى العادى انى عايز من الازبجكت دا ال id 
            }).ToList();
            return Query;
        }

        //////////////////////////////////////// GetOne
        ///

        public List<GetAllProductsDTO> GetOne(int id)
        {
            var Query = ProductsRepository.GetAll().Where(p => p.ProductsID == id).Select(p => new GetAllProductsDTO
            {
                ProductsID = p.ProductsID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoriesId = p.Categories.CategoriesId
            }).ToList();
            return Query;
        }

        //////////////////////////////////////// Create Product
        ///

        public Products Create(AddOrDeleteProductsDTO _product)
        {
            // First find category based on id of category >>> user will sent 
            var category = CategoriesRepository.GetAll().Where(ca => ca.CategoriesId == _product.CategoriesId)
                .FirstOrDefault();
            if (category == null)
            {
                //does not exist
                return null;
            }
            // Second create 
            if (!ProductsRepository.GetAll().Any(p => p.Name == _product.Name))
            {
                Products products = new Products()
                {
                    Name = _product.Name,
                    Description = _product.Description,
                    Price = _product.Price,
                    //Categories = new Categories() { CategoriesId = _product.CategoriesId }
                    Categories = category // as here i'm adding this product to the category already exist 
                };
                ProductsRepository.Create(products);
                return products;
            }
            else
            {
                return null;
            }
        }








        //////////////////////////////////////// Update Product
        ///

        public bool Update(AddOrDeleteProductsDTO _product, int id)
        {
            var productToUpdate = ProductsRepository.FindOneForUdpdateOrDelete(id);//Here the tracker track him
            if (productToUpdate != null)
            {
                productToUpdate.Name = _product.Name;
                productToUpdate.Description = _product.Description;
                productToUpdate.Price = _product.Price;
                ProductsRepository.SaveChanges(); // as tracker see him and want to save the change i made to him 
                return true;
            }
            else
            {
                return false;
            }
        }

        //////////////////////////////////////// Delete Product
        ///


        public bool Delete(int id)
        {
            var productToDelete = ProductsRepository.FindOneForUdpdateOrDelete(id);
            if (productToDelete != null)
            {
                ProductsRepository.Delete(productToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }


    }

}