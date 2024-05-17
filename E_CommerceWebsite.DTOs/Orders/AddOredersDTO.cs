using E_CommerceWebsite.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DTOs.Orders
{
    public class AddOredersDTO
    {
        public string CustomerId { get; set; }
        //public int ProductId { get; set; }
        //public int Quantity { get; set; }
        public string state { get; set; }
        public List<OrdersProductsDTO> OrdersProductsDTO { get; set; }
        public AddOredersDTO()
        {
            state = "Pending";   // here i'm making this constructor
                                 // as i want it to be all time pending when creating
        }

    }
}
