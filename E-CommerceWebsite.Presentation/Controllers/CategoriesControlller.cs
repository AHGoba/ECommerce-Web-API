




using E_CommerceWebsite.DTOs.Categories;
using E_CommerceWebsite.DTOs.Orders;
using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Service.categoryService;
using E_CommerceWebsite.Service.ordersService;
using E_CommerceWebsite.Service.productService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_CommerceWebsite.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesControlller : ControllerBase
    {


        ICategoriesService categoriesService;
        public CategoriesControlller(ICategoriesService _categoriesService )
        {
            this.categoriesService = _categoriesService; 
        }



        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            var categories = categoriesService.GetAll();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]  //this is a route to add id in   /Users/5
                         // as if i'm not use this attribute when i'm not sending id 
                         // binding will look for id and not found then put bellow id by default == 0
                         // so compiler will have an issue between choose which action GetAll or GetONE!!
        [Authorize(Roles = "Admin,User")]

        public IActionResult GetOne(int id)
        {
            var Query = categoriesService.GetOne(id);
            return Ok(Query);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]

        public IActionResult Create(AddCategoryDTO category)
        //Any is a method iterate over all users to see and do the action
        {
            if (categoriesService.Create(category))  //if true it will exceute this implement
            {
                return Ok("Successfully created");
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
        public IActionResult Update(AddCategoryDTO category, int id)
        {

            if (categoriesService.Update(category, id))
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
            
            if (categoriesService.Delete(id))
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
