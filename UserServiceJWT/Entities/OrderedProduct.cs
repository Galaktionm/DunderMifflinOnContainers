using System.ComponentModel.DataAnnotations;

namespace UserServiceJWT.Entities
{
    public class OrderedProduct
    {
        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public int amount { get; set; }

        public string manufacturer { get; set; }

        public string additionalInfo { get; set; }

        public long order_id { get; set; }

        public OrderedProduct() { }

        public OrderedProduct(string name, double price, int amount, string manufacturer, string additionalInfo)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.manufacturer = manufacturer;
            this.additionalInfo = additionalInfo;
        }

        public OrderedProduct(string name, double price, int amount, string manufacturer, string additionalInfo, long order_id)
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.manufacturer = manufacturer;
            this.additionalInfo = additionalInfo;
            this.order_id = order_id;
        }
    }
}
