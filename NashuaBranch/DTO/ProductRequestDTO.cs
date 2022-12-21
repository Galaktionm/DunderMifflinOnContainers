namespace NashuaBranch.DTO
{
    public class ProductRequestDTO
    {
        public string id { get; set; }

        public string name { get; set; }

        public int amount { get; set; }

        public double price { get; set; }

        public bool continueIfAbsent { get; set; } = true;

        public ProductRequestDTO() { }

        public ProductRequestDTO(string id, string name, int amount, double price)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
            this.price = price;
        }
    }
}
