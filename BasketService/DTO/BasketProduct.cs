using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.DTO
{
    public class BasketProduct
    {
        public string id { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public int amount { get; set; }

        public string manufacturer { get; set; }

        public string additionalInfo { get; set; }

        public BasketProduct() { }

        public BasketProduct(string id, string name, double price, int amount, string manufacturer = "", string additionalInfo = "")
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.amount = amount;
            this.manufacturer = manufacturer;
            this.additionalInfo = additionalInfo;
        }

        public override string ToString()
        {
            return id + " " + name;
        }
    }
}
