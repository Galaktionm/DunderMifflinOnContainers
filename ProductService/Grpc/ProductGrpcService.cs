using Grpc.Core;
using ProductService.Services;

namespace ProductService.Grpc
{
    public class ProductGrpcService : ProductGrpcProtoService.ProductGrpcProtoServiceBase
    {
        private readonly DatabaseService service;

        public ProductGrpcService(DatabaseService service)
        {
            this.service = service;

        }
        
        public override async Task<ResponseProducts> SendProducts(RequestedProducts request, ServerCallContext context)
        {
            ResponseProducts responseProducts = new ResponseProducts();
            foreach(var product in request.RequestedProductList)
            {
               var productFromDb=await service.GetByNameAsync(product.Name);
               foreach(Product dbProduct in productFromDb)
                {
                    if (dbProduct != null && dbProduct.available > product.Amount)
                    {
                        var productToAdd = MapToResponseProduct(dbProduct, product.Amount);
                        responseProducts.ResponseProductList.Add(productToAdd);

                    }
                    else
                    {
                        if (product.ContinueIfAbsent == true)
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                
            }
            return responseProducts;
        }


        private ResponseProduct MapToResponseProduct(Product product, int amount)
        {
            return new ResponseProduct
            {
                Id = product.id,
                Name = product.name,
                Price = product.price,
                Available = amount,
                AdditionalInfo = product.additionalInfo,
                Manufacturer = product.manufacturer,
            };
        }
    
    }
}
