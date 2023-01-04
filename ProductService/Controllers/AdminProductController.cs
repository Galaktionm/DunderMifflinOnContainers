using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashuaBranch;
using ProductService.Services;
using ScrantonBranch;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProductController : ControllerBase
    {
        private readonly ScrantonBranch.ScrantonGrpcService.ScrantonGrpcServiceClient scrantonClient;
        private readonly NashuaBranch.NashuaGrpcService.NashuaGrpcServiceClient nashuaClient;
        private readonly DatabaseService databaseService;

        public AdminProductController(ScrantonGrpcService.ScrantonGrpcServiceClient scrantonClient, 
            NashuaGrpcService.NashuaGrpcServiceClient nashuaClient, DatabaseService databaseService)
        {
            this.scrantonClient = scrantonClient;
            this.nashuaClient = nashuaClient;
            this.databaseService=databaseService;
        }

        [HttpPost("/scranton")]
        public async Task<IActionResult> SendToScranton()
        {
            ScrantonBranch.OrderedProducts orderedProducts=new ScrantonBranch.OrderedProducts();
            var products=await this.databaseService.GetAsync();
            foreach(Product product in products)
            {
                orderedProducts.Products.Add(MapToScrantonOrderedProduct(product, 100));
            }

            scrantonClient.SaveProducts(orderedProducts);
            return Ok(orderedProducts);

        }

        [HttpPost("/nashua")]
        public async Task<IActionResult> SendToNashua()
        {
            NashuaBranch.OrderedProducts orderedProducts = new NashuaBranch.OrderedProducts();
            var products = await this.databaseService.GetAsync();
            foreach (Product product in products)
            {
                orderedProducts.Products.Add(MapToNashuaOrderedProduct(product, 100));
            }

            nashuaClient.SaveProducts(orderedProducts);
            return Ok(orderedProducts);

        }


        public ScrantonBranch.OrderedProduct MapToScrantonOrderedProduct(Product produt, int amount)
        {
            return new ScrantonBranch.OrderedProduct
            {
                Id = produt.id,
                Name = produt.name,
                Price = produt.price,
                Amount = amount,
                AdditionalInfo = produt.additionalInfo,
                Manufacturer = produt.manufacturer,
            };
        }

        public NashuaBranch.OrderedProduct MapToNashuaOrderedProduct(Product produt, int amount)
        {
            return new NashuaBranch.OrderedProduct
            {
                Id = produt.id,
                Name = produt.name,
                Price = produt.price,
                Amount = amount,
                AdditionalInfo = produt.additionalInfo,
                Manufacturer = produt.manufacturer,
            };
        }

    }
}
