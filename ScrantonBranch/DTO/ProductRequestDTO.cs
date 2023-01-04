namespace ScrantonBranch.DTO
{
    public class ProductRequestDTO
    {
        public string id { get; set; }

        public string name { get; set; }

        public int amount { get; set; }


        public bool continueIfAbsent { get; set; } = true;

        public ProductRequestDTO() { }

        public ProductRequestDTO(string id, string name, int amount)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
        }
    }
}
