




using E_CommerceWebsite.DTOs.Categories;
using E_CommerceWebsite.DTOs.Orders;
using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Service.ordersService;
using E_CommerceWebsite.Service.productService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_CommerceWebsite.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase
    {


        IOrdersService ordersService;
        public OrdersController(IOrdersService _ordersService)
        {
            this.ordersService = _ordersService; 
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {

            var Query = ordersService.GetAll();
            return Ok(Query);
        }

        [HttpGet]
        [Route("{id}")]  //this is a route to add id in   /Users/5
                         // as if i'm not use this attribute when i'm not sending id 
                         // binding will look for id and not found then put bellow id by default == 0
                         // so compiler will have an issue between choose which action GetAll or GetONE!!
        [Authorize(Roles = "Admin")]

        public IActionResult GetOne(int id)
        {
            var Query = ordersService.GetOne(id);
            return Ok(Query);
        }

        [Authorize(Roles = "User")]

        [HttpPost]
         
        public IActionResult Create(AddOredersDTO addOredersDTO)
          
        {
            var createdOrders = ordersService.create(addOredersDTO);
            if(createdOrders!=null)  
            {

                var orderId = createdOrders.OrdersID;
                var response = new { orderId, status = "Success" };
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
        public IActionResult Update(UpdateOrederStateDTO updateOrederStateDTO, int id)
        {
            var updatedOrders = ordersService.Update(updateOrederStateDTO, id);
            if(updatedOrders!=null)
            {
                var orderid = updatedOrders.OrdersID;
                var response = new { orderid, Status = "updated" };
                return Ok(response);
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
            
            if (ordersService.Delete(id))
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
