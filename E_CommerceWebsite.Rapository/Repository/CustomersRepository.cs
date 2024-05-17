using E_CommerceWebsite.Context;
using E_CommerceWebsite.Models;
using E_CommerceWebsite.Models.Models;
using E_CommerceWebsite.Repositories.HighLevel;
using E_CommerceWebsite.Repository;

namespace Goba.Repository
{
    // Here i'm using <<IOrdersRepository>> as when i'm dealing with 
    public class CustomersRepository : GenericRapository<Customer> , ICustomersRepository
    {
        // If Here i wanna deal with a new CRUD operation so i got context here 

        ECommerceContext context;
        public CustomersRepository(ECommerceContext _ECommerceContextObject) : base(_ECommerceContextObject) //sending an argument to
                                                                                                           //the parent as it created first 
        {
            context = _ECommerceContextObject;   //using injection inversion that will came from the controller
                                                 // instead of using (new GobaContext)
        }








        //GobaContext context = new GobaContext();

        //public IQueryable<Users> GetAll()
        //{
        //    return context.Users;   
        //}

        //public IQueryable<Users> GetOne()
        //{
        //    return context.Users;
        //}
        //public void Create(Users user)
        //{
        //    context.Users.Add(user);
        //    context.SaveChanges();
        //}

        //public Users FindOneForUdpdateOrDelete(int id) 
        //{
        //    return context.Users.Find(id);          //Find here return an user of specific id
        //}

        //public void Delete(Users user)
        //{
        //    context.Users.Remove(user); 
        //    context.SaveChanges();
        //}

        //public void SaveChanges()
        //{
        //    context.SaveChanges();
        //}


    }
}
