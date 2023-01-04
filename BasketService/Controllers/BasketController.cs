using Aggregator.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly RedisBasketRepository redis;
        private readonly OrderService.Order.OrderClient client;
        public BasketController(RedisBasketRepository repo, OrderService.Order.OrderClient client)
        {
            redis = repo;
            this.client = client;
        }

        [HttpGet("{id}")]
        public async Task<CustomerBasket> GetCustomerBasket(string id)
        {
            return await redis.GetBasketAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(CustomerBasketRequest customerBasket)
        {
            var x = customerBasket.product;
            await redis.UpdateBasketAsync(customerBasket);
            return Ok(new { Message = "Item Added" });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PlaceOrder(string id)
        {
            var basket = await GetCustomerBasket(id);
            if (basket != null)
            {
                var processResult = client.ProcessOrder(MapToOrderedProduct(basket.products, id));
                if (processResult.Result == true)
                {
                    redis.DeleteBasket(id);
                    return Ok(new {message=processResult.Message, result=true});
                } else
                {
                    return BadRequest(new { message = processResult.Message, result=false});
                }
                
            }
            return BadRequest(new { message = "The cart is not created", result=false });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> DeleteItemFromBasketOrder(CustomerBasketRequest request)
        {
            if (await redis.RemoveItemFromBasketAsync(request))
            {
                return Ok(new { message = "Item deleted" });
            }

            return BadRequest(new { message = "There was an error processing your request" });

        }

        private OrderService.OrderedProducts MapToOrderedProduct(List<BasketProduct> products, string user_id)
        {
            OrderService.OrderedProducts orderServiceProducts = new OrderService.OrderedProducts();
            orderServiceProducts.UserId = user_id;
            foreach (var product in products)
            {
                orderServiceProducts.Products.Add(new OrderService.OrderedProduct()
                {
                    Id = product.id,
                    Name = product.name,
                    Price = product.price,
                    Amount = product.amount,
                    Manufacturer = product.manufacturer,
                    AdditionalInfo = product.additionalInfo,
                });
            }

            return orderServiceProducts;
        }

    }

    public class StringRequest
    {
        public string request { get; set; }

        public StringRequest(string request)
        {
            this.request = request;
        }
    }
}
