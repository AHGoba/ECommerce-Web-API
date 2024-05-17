
using E_CommerceWebsite.DTOS.AcountUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebsite.Presentation.Controllers.Accountcontrolles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public RolesController( UserManager<IdentityUser>_userManager, RoleManager<IdentityRole> _roleManager)
        {
            UserManager = _userManager;
            RoleManager = _roleManager;   
        }

        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(RollesDTo rollesDto)
 
        {
            // check if role not exist (check on database)
            var ExistRole = await RoleManager.FindByNameAsync(rollesDto.RoleName);
            if (ExistRole != null)
                return Ok("Already exist");
            // create a new role 
            IdentityRole identity = new IdentityRole() //here i make a new object (new role)
            {
                Name = rollesDto.RoleName
            };

            //create in Db using userManager (services including raposatory) that came from identity package
            var res = await RoleManager.CreateAsync(identity);
            if (!res.Succeeded)
            {

                return Ok(res.Errors.ElementAt(0));
            }
            return Ok("Created");

        }

        //Here i will sent in body email and role 
        //email : to get the user from (UserManager<IdentityUser>) 
        //role :to change it's role to the role i will sent OR   to add a role 
        // from 

        [HttpPost("ChangeOrAddUserRole")]
        public async Task<IActionResult> ChangeUserRole(ChangeRoleDTO changeRoleDTO)
        {
            var user = await UserManager.FindByEmailAsync(changeRoleDTO.Email);
            if (user == null)
            {
                return BadRequest("There is no user with this Email!.....");
            }
            else
            {
                // here i wanna make a validation that he enter an role that already exist 
                    await UserManager.AddToRoleAsync(user, changeRoleDTO.Role);
                    return Ok($"Role '{changeRoleDTO.Role}' added to the user '{user.UserName}' seccessfully");
            }
        }

    }
}
