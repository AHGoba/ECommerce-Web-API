using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Repository
{
    public interface IGenericRapository<T>  //T here is a class 
    {

        IQueryable<T> GetAll();


        void Create(T Entity);    

        T FindOneForUdpdateOrDelete(int entity);

        void Delete(T entity);

        void SaveChanges();
    }
}
