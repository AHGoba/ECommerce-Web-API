




using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Service.productService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_CommerceWebsite.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {



        IProductService productService;
        public ProductsController(IProductService _productService)
        {
            this.productService = _productService; 
        }



        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {

            var Query = productService.GetAll();
            return Ok(Query);
        }

        [HttpGet]
        [Route("{id}")]  //this is a route to add id in   /Users/5
                         // as if i'm not use this attribute when i'm not sending id 
                         // binding will look for id and not found then put bellow id by default == 0
                         // so compiler will have an issue between choose which action GetAll or GetONE!!
        [Authorize(Roles = "Admin,User")]

        public IActionResult GetOne(int id)
        {
            var Query = productService.GetOne(id);
            return Ok(Query);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
         
        public IActionResult Create(AddOrDeleteProductsDTO _product)
          
        {
            var createdProduct = productService.Create(_product);
            if(createdProduct!=null)  
            {

                var productId = createdProduct.ProductsID;
                var response = new { productId, status = "Created" };
                return Ok(response);
            }
            else
            {   
                return BadRequest("already exist");
            }
        }

        [Authorize(Roles = "Admin")]

        //update using Put 
        [HttpPut]
        [Route("{id}")] //Must be the same name in the parameter below as 
        public IActionResult Update(AddOrDeleteProductsDTO _product, int id)
        {
            
            if (productService.Update(_product , id))
            {
                                  
                return Ok("Successfully updated");
            }
            else
            {
                return NotFound();
            }

        }


        [Authorize(Roles = "Admin")]

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete( int id)
        {
            
            if (productService.Delete(id))
            {
                return Ok("Successfully Deleted");
            }
            else
            {
                return NotFound();
            }

        }

    }
}
