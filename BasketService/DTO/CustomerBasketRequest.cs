using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.DTO
{
    public class CustomerBasketRequest
    {
        public string userId { get; set; }

        public BasketProduct product { get; set; }

        public CustomerBasketRequest() { }

        public CustomerBasketRequest(string userId, BasketProduct product)
        {
            this.userId = userId;
            this.product = product;
        }
    }
}
