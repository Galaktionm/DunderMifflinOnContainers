using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserServiceJWT.Entities;

namespace UserServiceJWT.Controllers
{
    [Route("api/userAdmin")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminUserController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public AdminUserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> users=userManager.Users.ToList();
            return Ok(users);

        }


        [HttpPost("{email}")]
        public async Task<IActionResult> RemoveUser(String email)
        {
            User user=await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result=await userManager.DeleteAsync(user);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
