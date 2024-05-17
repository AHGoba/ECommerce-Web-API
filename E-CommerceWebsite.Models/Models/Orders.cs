using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Models.Models
{
    public class Orders
    {
        public int OrdersID { get; set; }
        //public int CustomerId { get; set; }  //this Property i will use to can relate between 


        public string State { get; set; }  //here i want the admin who can change the state not user 
                                      // so here i will make an dto to put for any order the user make without state

        public Customer Customer { get; set; }           // 1 Costomer : many Orders
        public List<OrdersProducts> OredersProducts { get; set; }

    }
}
