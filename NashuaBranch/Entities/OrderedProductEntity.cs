using System.ComponentModel.DataAnnotations;

namespace NashuaBranch.Entities
{
    public class OrderedProductEntity
    {
        [Key]
        public long id { get; set; }

        public string product_id { get; set; }

        public long order_id { get; set; }

        public string name { get; set; }

        public string? manufacturer { get; set; }

        public double price { get; set; }

        public int amount { get; set; }

        public string? additionalInfo { get; set; }

        public OrderedProductEntity() { }

        public OrderedProductEntity(string name, double price, int amount, string additionalInfo = "", string manufacturer = "")
        {
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.additionalInfo = additionalInfo;
            this.manufacturer = manufacturer;
        }

        public OrderedProductEntity(string product_id, long order_id, string name, double price, int amount, 
            string additionalInfo = "", string manufacturer = "")
        {
            this.product_id = product_id;
            this.order_id = order_id;
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.additionalInfo = additionalInfo;
            this.manufacturer = manufacturer;
        }

    }
}
