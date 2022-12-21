using Grpc.Core;
using Hangfire;

namespace OrderService.Grpc
{
    public class OrderGrpcService : Order.OrderBase
    {
        private readonly ScrantonBranch.ScrantonGrpcService.ScrantonGrpcServiceClient scrantonClient;
        private readonly NashuaBranch.NashuaGrpcService.NashuaGrpcServiceClient nashuaClient;
        private static String client = "Scranton";

        public OrderGrpcService(
            ScrantonBranch.ScrantonGrpcService.ScrantonGrpcServiceClient scrantonClient,
            NashuaBranch.NashuaGrpcService.NashuaGrpcServiceClient nashuaClient)
        {
            this.scrantonClient = scrantonClient;
            this.nashuaClient = nashuaClient;
            RecurringJob.AddOrUpdate("GetNOrdersJob", () => SetCurrentClientBranch(), Cron.Minutely);
        }


        public override async Task<ProcessResult> ProcessOrder(OrderedProducts products, ServerCallContext context)
        {
            var x = client;
            if (client.Equals("Scranton"))
            {
                var result=SendToScranton(products);
                if (result.Result == false)
                {
                    return SendToNashua(products);
                }
                return result;
            } else
            {
                var nashuaResult=SendToNashua(products);
                if (nashuaResult.Result == false)
                {
                    return SendToScranton(products);
                }
                return nashuaResult;
            }
        }

        private ProcessResult SendToScranton(OrderedProducts products) {
            var checkResult = scrantonClient.PlaceOrder(MapToScrantonBranchOrderedProducts(products));
            if (checkResult.Result == false) {
                return new ProcessResult {
                    Result = false,
                    Message = checkResult.Message,
                };
            }
            return new ProcessResult {
                Result = true,
                Message = checkResult.Message,
            };
        }

        private ProcessResult SendToNashua(OrderedProducts products)
        {
            var checkResult = nashuaClient.PlaceOrder(MapToNashuaBranchOrderedProducts(products));
            if (checkResult.Result == false)
            {
                return new ProcessResult
                {
                    Result = false,
                    Message = checkResult.Message,
                };
            }
            return new ProcessResult
            {
                Result = true,
                Message = checkResult.Message,
            };
        }


        private ScrantonBranch.OrderedProducts MapToScrantonBranchOrderedProducts(OrderedProducts products)
        {
            ScrantonBranch.OrderedProducts orderedProducts = new ScrantonBranch.OrderedProducts();
            orderedProducts.UserId = products.UserId;
            foreach (var product in products.Products)
            {
                orderedProducts.Products.Add(new ScrantonBranch.OrderedProduct
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = product.Amount,
                    Manufacturer = product.Manufacturer,
                    AdditionalInfo = product.AdditionalInfo,
                });
            };
            return orderedProducts;
        }

        private NashuaBranch.OrderedProducts MapToNashuaBranchOrderedProducts(OrderedProducts products)
        {
            NashuaBranch.OrderedProducts orderedProducts = new NashuaBranch.OrderedProducts();
            orderedProducts.UserId = products.UserId;
            foreach(var product in products.Products) {
                orderedProducts.Products.Add(new NashuaBranch.OrderedProduct
                {                  
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = product.Amount,
                    Manufacturer = product.Manufacturer,
                    AdditionalInfo = product.AdditionalInfo,
                });                       
            };         
            return orderedProducts;
        }

        public void SetCurrentClientBranch()
        {
            try
            {
                var datetime = DateTime.Now;
                var scrantonNumber = scrantonClient.GetOrderNumber(
                    new ScrantonBranch.NOrdersRequest
                    {
                        Message = $"Number of orders requested at {datetime}"
                    });
                var nashuaNumber = nashuaClient.GetOrderNumber(
                    new NashuaBranch.NOrdersRequest
                    {
                        Message = $"Number of orders requested at {datetime}"
                    });
                if (scrantonNumber.Number > nashuaNumber.Number)
                {
                    client = "Nashua";

                }
                else
                {
                    client = "Scranton";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
