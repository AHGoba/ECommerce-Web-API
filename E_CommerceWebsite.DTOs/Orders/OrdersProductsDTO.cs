using E_CommerceWebsite.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DTOs.Orders
{
    public class OrdersProductsDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
