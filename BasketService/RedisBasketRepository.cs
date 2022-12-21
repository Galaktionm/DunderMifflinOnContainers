using System.Text.Json;
using Aggregator.DTO;
using Aggregator.Services;
using StackExchange.Redis;

namespace Aggregator
{
    public class RedisBasketRepository
    {
        private readonly IDatabase database;

        public RedisBasketRepository(RedisConnectionService service)
        {
            database = service.connection.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var data = await database.StringGetAsync(id);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            var deseruakzedBasket = JsonSerializer.Deserialize<CustomerBasket>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return deseruakzedBasket;
        }

        public async Task<bool> UpdateBasketAsync(CustomerBasketRequest basket)
        {
            var customerBasket = await database.StringGetAsync(basket.userId);
            if (customerBasket.IsNullOrEmpty)
            {
                CustomerBasket newBasket = new CustomerBasket();
                newBasket.userId = basket.userId;
                newBasket.products.Add(basket.product);
                return await database.StringSetAsync(newBasket.userId,
                    JsonSerializer.Serialize(newBasket));

            }
            else
            {
                var currentBasket = JsonSerializer.Deserialize<CustomerBasket>(customerBasket);
                currentBasket.products.Add(basket.product);
                return await database.StringSetAsync(basket.userId, JsonSerializer.Serialize(currentBasket));
            }

            return false;
        }

        public async Task<bool> RemoveItemFromBasketAsync(CustomerBasketRequest request)
        {
            var customerBasket = await database.StringGetAsync(request.userId);
            if (customerBasket.IsNullOrEmpty)
            {
                return false;
            }
            else
            {
                var currentBasket = JsonSerializer.Deserialize<CustomerBasket>(customerBasket);
                var productForRemoval = currentBasket.products.Find(basketProduct => basketProduct.id == request.product.id);
                currentBasket.products.Remove(productForRemoval);
                return await database.StringSetAsync(request.userId, JsonSerializer.Serialize(currentBasket));
            }

        }

        public void DeleteBasket(string id)
        {
            database.KeyDelete(id);
        }


        public async Task<List<BasketProduct>> GetBasketProductsAsync(string id)
        {
            return (await GetBasketAsync(id)).products;
        }
    }

    public class CustomerBasketRequestInternal
    {
        public string userId { get; set; }

        public List<BasketProduct> products { get; set; }

        public CustomerBasketRequestInternal() { }

        public CustomerBasketRequestInternal(string userId, List<BasketProduct> products)
        {
            this.userId = userId;
            this.products = products;
        }
    }
}
