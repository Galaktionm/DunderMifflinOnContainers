using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService
{
    public class Product
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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

    }  


}