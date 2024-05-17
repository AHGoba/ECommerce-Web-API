using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Models.Models
{
    public class OrdersProducts
    {
        public int OrdersID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
    }
}
