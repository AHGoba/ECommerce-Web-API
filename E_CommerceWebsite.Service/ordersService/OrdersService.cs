using E_CommerceWebsite.DTOs.Orders;
using E_CommerceWebsite.DTOs.Products;
using E_CommerceWebsite.Models.Models;
using E_CommerceWebsite.Repositories.HighLevel;
using E_CommerceWebsite.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Service.ordersService 
{
    public class OrdersService : IOrdersService
    {
        IOrdersRepository OrdersRepository;
        IProductsRepository productsRepository;
        ICustomersRepository CustomersRepository;
        public OrdersService(IOrdersRepository _ordersRepository, IProductsRepository _productsRepository, ICustomersRepository _customersRepository)
        {
            OrdersRepository = _ordersRepository;
            productsRepository = _productsRepository;
            CustomersRepository = _customersRepository;
        }


        //////////////////////////////////////// GetAll
        ///

        public List<GetAllOrdersDTO> GetAll()
        {
            var Query = OrdersRepository.GetAll().Select(or => new GetAllOrdersDTO
            {
                Customerid = or.Customer.Id, //as there is no id in the class of customer بس موجود فى اليوزر
                OrdersID = or.OrdersID,
                State = or.State,
                //////////////// Here below i have an issue why casting explicity? 
                ProductsQuantity = (List<GetQuantityProductForOrdersDTO>)or.OredersProducts.Select(ord => new GetQuantityProductForOrdersDTO
                {
                    ProductID = ord.ProductID,
                    Quantity = ord.Quantity
                })
            }).ToList();
            return Query;
        }

        //////////////////////////////////////// Getone
        ///
        public List<GetAllOrdersDTO> GetOne(int id)
        {
            var Query = OrdersRepository.GetAll().Where(or => or.OrdersID == id).Select(or => new GetAllOrdersDTO
            {
                Customerid = or.Customer.Id, //as there is no id in the class of customer بس موجود فى اليوزر
                OrdersID = or.OrdersID,
                State = or.State,
                //////////////// Here below i have an issue why casting explicity? 
                ProductsQuantity = (List<GetQuantityProductForOrdersDTO>)or.OredersProducts.Select(ord => new GetQuantityProductForOrdersDTO
                {
                    ProductID = ord.ProductID,
                    Quantity = ord.Quantity
                })
            }).ToList();
            return Query;
        }


        //////////////////////////////////////// Create orders 
        /// to make an order the product id must be exist first as if product doesn't exist how to make an order ? 

        //public Orders create(AddOredersDTO addOredersDTO)
        //{
        //    // validate product that must be exist and get it to add to the order
        //    var product = productsRepository.GetAll().Where(pr => pr.ProductsID == addOredersDTO.ProductId)
        //        .FirstOrDefault();
        //    if (product == null)
        //    {
        //        return null;
        //    }

        //    //get customer to add to the order (relate customer to the order)
        //    //as customer inherit from identityuser so i can deal with all fileds in the indentity including id
        //    var customer = CustomersRepository.GetAll().Where(cus => cus.Id == addOredersDTO.CustomerId)
        //        .FirstOrDefault();
        //    //  create 
        //    Orders orders = new Orders()
        //    {

        //        Customer = customer,
        //        State = addOredersDTO.state,
        //        OredersProducts = addOredersDTO.OrdersProducts.Select(p => new OrdersProducts
        //        {
        //            ProductID = p.ProductId,
        //            Quantity = p.Quantity,
        //            Orders = null
        //            //Products = product // Set the product for the order product

        //        }).ToList()
        //    };
        //    OrdersRepository.Create(orders);
        //    return orders;
        //}


        public Orders create(AddOredersDTO addOredersDTO)
        {
            // Get customer to add to the order
            var customer = CustomersRepository.GetAll().FirstOrDefault(cus => cus.Id == addOredersDTO.CustomerId);
            if (customer == null)
            {
                return null; // Customer not found
            }

            // Create the order
            Orders orders = new Orders()
            {
                Customer = customer,
                State = addOredersDTO.state,
                OredersProducts = new List<OrdersProducts>(),
                
            };

            //validate each product i send 
            //AS ordersProductDTO is a list of (productId and quantity)

            foreach (var orderProduct in addOredersDTO.OrdersProductsDTO)
            {
                // Validate product that must exist and get it to relate to order
                var product = productsRepository.GetAll().FirstOrDefault(pr => pr.ProductsID == orderProduct.ProductId);
                if (product == null)
                {
                    return null; // Product not found
                }

                // Add the product to the order
                orders.OredersProducts.Add(new OrdersProducts
                {
                    ProductID = orderProduct.ProductId,
                    Quantity = orderProduct.Quantity,
                    Orders = orders, // Set the order for the order product
                    Products = product // Set the product for the order product
                });
            }

            // Assuming OrdersRepository is used to save the order to the database
            OrdersRepository.Create(orders);
            return orders;
        }



        //////////////////////////////////////// update an existing order 
        /// 

        public Orders Update(UpdateOrederStateDTO updateOrederStateDTO, int id)
        {
            //check existing of order
            var orderToUpdate = OrdersRepository.GetAll().Where(or => or.OrdersID == id)
                .FirstOrDefault();//Here the tracker track him
            if (orderToUpdate != null)
            {
                orderToUpdate.State = updateOrederStateDTO.State;
                OrdersRepository.SaveChanges();
                return orderToUpdate;
            }
            else
            {
                return null;
            }

        }




        //////////////////////////////////////// delete an existing order 
        /// 

        public bool Delete(int id)
        {
            var orderToDelete = OrdersRepository.GetAll().Where(or => or.OrdersID == id)
                .FirstOrDefault();//Here the tracker track him
            if (orderToDelete != null)
            {

                OrdersRepository.Delete(orderToDelete);
                return true;
            }
            else
            {
                return false;
            }

        }
    }


}
