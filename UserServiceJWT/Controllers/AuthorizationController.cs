using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserServiceJWT;
using UserServiceJWT.DTO;
using UserServiceJWT.Entities;
using UserServiceJWT.Services;

namespace UserServiceJWT.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTService service;

        public AuthorizationController(DatabaseContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, JWTService JWTService)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            service = JWTService;
        }

        [HttpPost("account")]
        public async Task<IActionResult> GetUserAccount(UserRequest request)
        {
            var user = await userManager.FindByIdAsync(request.userId);          
            if (user != null)
            {
                List<Order> orders = new List<Order>();
                var orderQuery = from entity in context.Orders where entity.user_id == user.Id select entity;
                List<Order> orderQueryList=orderQuery.ToList();
                foreach(Order order in orderQueryList){
                    var orderedItemsQuery=from item in context.OrderedProducts where item.order_id==order.order_id select item;
                    Order createdOrder = new Order(order.order_id, order.user_id, orderedItemsQuery.ToList());
                    orders.Add(createdOrder);
                }
                
                return Ok(new UserResponse(user.UserName, user.Email, user.balance, orders));
            }
            return NoContent();

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await userManager.FindByEmailAsync(request.email) == null)
            {
                var user = new User(request.userName, request.email, 500);
                var result = await userManager.CreateAsync(user, request.password);

                if (result.Succeeded)
                {
                    var addRole = await userManager.AddToRoleAsync(user, "User");
                    if (addRole.Succeeded == false)
                    {
                        return BadRequest(new { message = "There was an error processing your request" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "There was an error processing your request" });
                }
            }
            else
            {
                return BadRequest(new { message = "User with such email already exists" });
            }

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.email);
            if (user == null
                || !await userManager.CheckPasswordAsync(user, loginRequest.password))
                return Unauthorized(new LoginResult()
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                });
            var secToken = await service.GetTokenAsync(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResult()
            {
                Success = true,
                Message = "Login successful",
                Token = jwt,
                UserId = user.Id
            });
        }

    }

    public class UserResponse
    {
        public string username { get; set; }

        public string email { get; set; }

        public double balance { get; set; }

        public List<Order> orders { get; set; }

        public UserResponse(string username, string email, double balance)
        {
            this.username = username;
            this.email = email;
            this.balance = balance;
            this.orders = new List<Order>();
        }

        public UserResponse(string username, string email, double balance, List<Order> orders)
        {
            this.username = username;
            this.email = email;
            this.balance = balance;
            this.orders = orders;
        }
    }

    public class UserRequest
    {
        public string userId { get; set; }

        public UserRequest(string userId)
        {
            this.userId = userId;
        }
    }
}
