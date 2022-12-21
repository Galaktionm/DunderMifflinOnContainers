using System.ComponentModel.DataAnnotations;
using UserService;

namespace UserServiceJWT.Entities
{
    public class Order
    {
        [Key]
        public long order_id { get; set; }

        public string user_id { get; set; }

        public List<OrderedProduct> orderedProducts { get; set; }

        public Order()
        {
            orderedProducts = new List<OrderedProduct>();
        }

        public Order(long order_id, string user_id)
        {
            orderedProducts=new List<OrderedProduct>();
            this.order_id=order_id;
            this.user_id=user_id;
        }

        public Order(long order_id, List<OrderedProduct> orderedProducts)
        {
            this.order_id = order_id;
            this.orderedProducts = orderedProducts;
        }
    }
}
