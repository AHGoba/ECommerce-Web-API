using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.Models.Models
{   // here i'm just using this table to be able :
    //عشان عايز اعمل علاقه بين اليوزر والاوردرات ليس الا 
    // 1 (User) : (Many Orders) 
    //وجدول اليوزر اللى هكريته من الباكدج مفهوش العلاقه دى لذلك هعمل الكاستمر دا عشان ينهرت منه
    //فايده العلاقه دى ان لو انا كنت عايز اجيب كل الاوردرات اللى عملها اليوزر الواحد
    public class Customer : IdentityUser
    {
        public List<Orders> orders { get; set; }
    }
}
