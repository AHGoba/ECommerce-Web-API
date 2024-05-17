using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DTOs.Orders
{
    public class GetAllOrdersDTO
    {
        public int OrdersID { get; set; }
        public string State { get; set; }
        public string Customerid {  get; set; }    // i made it (srting) as it's in the table of users is a string

        public List<GetQuantityProductForOrdersDTO> ProductsQuantity { get; set; }

    }
}
