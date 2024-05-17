using E_CommerceWebsite.Context;


using Microsoft.EntityFrameworkCore;



namespace E_CommerceWebsite.Repository
{
    public class GenericRapository<T> : IGenericRapository<T> where T : class 
                                    //here i'm using this constrain as i will make it to be any class 
                                                      // only accept class (product, Orders,.....)
    {
        DbSet<T> db;
        ECommerceContext context;
        public GenericRapository(ECommerceContext ecommerceContextObject)
        {
            context = ecommerceContextObject;   //using injection inversion
            db = context.Set<T>(); //here i'm using it so i can deal with context.(anydbset) i have 
        }

        public IQueryable<T> GetAll()
        {
            return db;
        }   

    
        public void Create(T Entity)   // T here refer to a class (orderrs or dto)
        {
            db.Add(Entity);
            context.SaveChanges();
        }

        public T FindOneForUdpdateOrDelete(int entity)  // meaning id here
        {
            return db.Find(entity);          //Find here return an user of specific id
        }

        public void Delete(T entity)
        {
            db.Remove(entity);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
