using Microsoft.AspNetCore.Identity;

namespace UserServiceJWT.Entities
{
    public class User : IdentityUser
    {
        public double balance { get; set; }
        public List<Order> orders { get; set; }
        public User()
        {
            orders = new List<Order>();
        }
        public User(string userName, string email, double balance)
        {
            orders = new List<Order>();
            UserName = userName;
            Email = email;
            this.balance = balance;
        }
    }
}
