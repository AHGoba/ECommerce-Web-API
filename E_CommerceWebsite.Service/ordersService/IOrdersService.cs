using E_CommerceWebsite.DTOs.Orders;
using E_CommerceWebsite.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Service.ordersService
{
    public interface IOrdersService
    {
        List<GetAllOrdersDTO> GetAll();
        List<GetAllOrdersDTO> GetOne(int id);
        Orders create(AddOredersDTO addOredersDTO);
        Orders Update(UpdateOrederStateDTO updateOrederStateDTO, int id);
        bool Delete(int id);
    }
}
