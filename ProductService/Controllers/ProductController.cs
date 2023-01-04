using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services;

using System.Linq;

namespace ProductService.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseService service;
        public ProductController(DatabaseService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllItemsPaginatedAsync([FromQuery] int pageSize = 20, [FromQuery] int pageIndex = 0)
        {
            var list = await service.GetAsync();
            return list.Skip(pageIndex * pageSize).Take(pageSize);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Product>> GetAllItemsAsync()
        {
            var list = await service.GetBySentAsync();
            return list;
        }

        [HttpPost]
        public async Task<List<Product>> GetProductsByNameAync(ProductSearch search)
        {
            var searchResult=await service.GetByNameAsync(search.query);
            return searchResult;
        }
    }

    public class ProductSearch
    {
        public string query { get; set; }

        public ProductSearch(string query)
        {
            this.query= query;
        }
    }
}
