using System.ComponentModel.DataAnnotations;

namespace NashuaBranch.Entities
{
    public class Order
    {
        [Key]
        public long order_id { get; set; }

        public string user_id { get; set; }

        public List<OrderedProductEntity> products { get; set; }

        public Order() { 
            products = new List<OrderedProductEntity>();
        } 

        public Order(string user_id, List<OrderedProductEntity> products)
        {
            this.user_id = user_id;
            this.products = products;                
        }

    }
}
