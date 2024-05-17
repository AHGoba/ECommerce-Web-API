using E_CommerceWebsite.DTOs.AcountUser;
using E_CommerceWebsite.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceWebsite.Presentation.Controllers.Accountcontrolles
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // identity package provides a class of (user) that i ccan use already 
        // so to can deal and make an CRUD opertaion(service) on this class  
        //UserManager is a class provided by ASP.NET Core Identity that provides functionality for managing users,
        //such as creating, updating, and deleting user accounts.
        //<IdentityUser> is a generic type parameter that specifies the type of user object managed by
        //the UserManager. In this case, IdentityUser is the default user class provided by ASP.NET Core Identity.
        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IConfiguration configuration { get; }

        public AccountController(UserManager<IdentityUser> _userManager ,RoleManager<IdentityRole> _roleManager, IConfiguration _configuration)
        {
            UserManager = _userManager; // here it's the services (Methods) that i will use to check and do operation in each action
            configuration = _configuration;
            RoleManager = _roleManager;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAndLoginDto registerAndLoginDto)
        // here i want to send data of user to create so
        // we don't use the tables that we have in table (ASp users) 
        //انما بنستخدم الدى تى او لان انا مش عايز ادخل كل البياانات بتاعة اليوزر 
        {
            // check if user not exist
            var ExistUser = await UserManager.FindByEmailAsync(registerAndLoginDto.Email);
            if (ExistUser != null)
                return Ok("Already exist");
            // create user
            Customer identity = new Customer() //here i make a new object (new user) by using customer object 
                                    // that inherit from 
            {
                UserName = registerAndLoginDto.UserName,
                Email = registerAndLoginDto.Email,
            };

            //create in Db using userManager (services including raposatory) that came from identity package
            //Here i put password to be hashed(not known) by the package 
            var res = await UserManager.CreateAsync(identity, registerAndLoginDto.Password);
            if (!res.Succeeded)
            {
                return Ok(res.Errors.ElementAt(0));
            }
            else
            {
                //AddToRoleAsync here is a method that add user to a specific role that already exist 
                //in table of Roles (it add in the usersroles table)
                // it must be exist in the table of Roles as (addtoroleasync is dealing with a role exist in table)
                await UserManager.AddToRoleAsync(identity, "User");
                return Ok("Created");
            }


        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> login(RegisterAndLoginDto registerAndLoginDto)
        {
            //check valid (existing email ?) 
            var user = await UserManager.FindByEmailAsync(registerAndLoginDto.Email); //this return the user if 
                                                                                     //exist in the db
            //if no return not unauthorized
            if (user == null) 
                return Unauthorized();

            //Check if (user) i got have (password) i sent in the body
            var checkPass = await UserManager.CheckPasswordAsync(user, registerAndLoginDto.Password);
            if (checkPass == false)
            {
                return Unauthorized();
            }
            //// if yes valid : return with token 

            // first get role of the user
            var userRoles = await UserManager.GetRolesAsync(user);  //it's a list as for each user 
                                                                    //it can hold more than one role 

            // about claims : 
            // Claims are used to make authorization decisions and can represent characteristics such as roles
            //,permissions, or personal information about the user.
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                //new Claim(ClaimTypes.Role , "Admin") 
            };
            foreach (var role in userRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role)); //Claims is a list of options that i'm added to token
            }
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]));
            var Settoken = new JwtSecurityToken(     //sett >>> setting of token (what will hold?)
                 //For more secure i can use (issuer and audience) that used to limit who can deal with me 
                 issuer: configuration["jwt:issuer"],
                 audience: configuration["jwt:audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)

                );
            var token = new JwtSecurityTokenHandler().WriteToken(Settoken);
            return Ok(token); 
        }
    }
}
