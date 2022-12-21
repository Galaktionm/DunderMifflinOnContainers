namespace Aggregator.DTO
{
    public class CustomerBasket
    {
        public string userId { get; set; }

        public List<BasketProduct> products { get; set; }

        public CustomerBasket() {
            products = new List<BasketProduct>();
        }

        public CustomerBasket(string userId, List<BasketProduct> products)
        {
            this.userId = userId;
            this.products = products;
        }
    }
}
