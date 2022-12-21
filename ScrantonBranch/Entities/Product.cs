using System.ComponentModel.DataAnnotations;

namespace ScrantonBranch.Entities
{
    public class Product
    {
        [Key]
        public string id { get; set; }

        public string name { get; set; }

        public string? manufacturer { get; set; }

        public double price { get; set; }

        public int? available { get; set; }

        public string? additionalInfo { get; set; }

        public Product() { }

        public Product(string name, double price, int available, string additionalInfo = "", string manufacturer = "")
        {
            this.name = name;
            this.price = price;
            this.available = available;
            this.additionalInfo = additionalInfo;
            this.manufacturer = manufacturer;
        }

        public Product(string id, string name, double price, int available, string additionalInfo = "", string manufacturer = "")
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.available = available;
            this.additionalInfo = additionalInfo;
            this.manufacturer = manufacturer;
        }

    }
}
