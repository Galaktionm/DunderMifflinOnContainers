using ScrantonBranch.DTO;
using ScrantonBranch.Entities;

namespace ScrantonBranch.Services
{
    public class ProductRequestService
    {
        private readonly ProductService.ProductGrpcProtoService.ProductGrpcProtoServiceClient client;
        private readonly DatabaseContext dbContext;
        public ProductRequestService(DatabaseContext dbContext, 
            ProductService.ProductGrpcProtoService.ProductGrpcProtoServiceClient client)
        {
            this.client = client;
            this.dbContext = dbContext;
        }

        public void SaveProducts(List<ProductRequestDTO> request)
        {
            var response=client.SendProducts(MapToProductServiceRequestedProducts(request));
            if (response != null)
            {
                foreach(var item in response.ResponseProductList)
                {
                   var productFromDb=dbContext.Products.Find(item.Id);
                    if (productFromDb == null)
                    {
                        dbContext.Products.Add(MapToNativeProduct(item));
                        dbContext.SaveChanges();
                    } else
                    {
                        productFromDb=MapToNativeProduct(item);
                        dbContext.Products.Update(productFromDb);
                        dbContext.SaveChanges();
                    }
                }
            }
        }


        private ProductService.RequestedProducts MapToProductServiceRequestedProducts(List<ProductRequestDTO> request)
        {
            ProductService.RequestedProducts requestedProductList = 
                new ProductService.RequestedProducts();

            foreach(ProductRequestDTO product in request)
            {
                requestedProductList.RequestedProductList.Add(
                    new ProductService.RequestedProduct
                    {
                        Id = product.id,
                        Name = product.name,
                        Price = product.price,
                        Amount = product.amount,
                        ContinueIfAbsent = product.continueIfAbsent,
                    });
            }

            return requestedProductList;
        }

        private Product MapToNativeProduct(ProductService.ResponseProduct responseProduct)
        {
            return new Product(responseProduct.Id, responseProduct.Name, responseProduct.Price,
                responseProduct.Available, responseProduct.AdditionalInfo, responseProduct.Manufacturer);
        }



    }
}
