using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Models.Models
{
    public class Products
    {
        public int ProductsID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        //public int Quantity { get; set; }
        public Categories Categories { get; set; }

        public List<OrdersProducts> OredersProducts { get; set; }    
    }
}
