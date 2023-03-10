using Grpc.Core;
using NashuaBranch.Entities;
using UserService;

namespace NashuaBranch.Grpc
{
    public class NashuaBranchGrpcService : NashuaGrpcService.NashuaGrpcServiceBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserGrpc.UserGrpcClient userGrpcClient;

        public NashuaBranchGrpcService(DatabaseContext database, UserGrpc.UserGrpcClient userGrpcClient)
        {
            databaseContext = database;
            this.userGrpcClient = userGrpcClient;
        }

        public async override Task<CheckResult> PlaceOrder(OrderedProducts products, ServerCallContext context)
        {
            List<OrderedProductEntity> orderedProductEntities = new List<OrderedProductEntity>();
            foreach (var product in products.Products)
            {
                var productFromDb = databaseContext.Products.
                    FirstOrDefault(p => p.id == product.Id && p.name == product.Name);
                if (productFromDb == null || productFromDb.available < product.Amount)
                {
                    return new CheckResult
                    {
                        Result = false,
                        Message = $"Product {product.Name} is not available",
                    };
                }
                else
                {
                    orderedProductEntities.Add(MapToOrderedProductEntity(product));
                }
            }
            var validationResult = await ValidatePayment(orderedProductEntities, products.UserId);
            if (validationResult.Result)
            {
                Order order = new Order(products.UserId, orderedProductEntities);
                databaseContext.Add(order);
                databaseContext.SaveChanges();                
                var saveOrderResponse = userGrpcClient.SaveOrder(MapToUserServiceOrderGrpc(order));
                if (saveOrderResponse.Result)
                {
                    return new CheckResult
                    {
                        Result = true,
                        Message = "Order placed successfully at Nashua branch"
                    };
                }
                else
                {
                    return new CheckResult
                    {
                        Result = false,
                        Message = "Placing order failed due to errors in user process"
                    };
                }
            }
            else
            {
                return new CheckResult
                {
                    Result = false,
                    Message = "Placing order failed. Check balance"
                };
            }
        }

        public override Task<CheckResult> SaveProducts(OrderedProducts orderedProducts, ServerCallContext context)
        {
            List<OrderedProductEntity> orderedProductsEntities = new List<OrderedProductEntity>();
            foreach (OrderedProduct orderedProduct in orderedProducts.Products)
            {
                orderedProductsEntities.Add(MapToOrderedProductEntity(orderedProduct));
            }

            foreach (OrderedProductEntity orderedProduct in orderedProductsEntities)
            {
                databaseContext.Add(orderedProduct);
            }
            databaseContext.SaveChanges();

            return Task.FromResult(new CheckResult
            {
                Result = true,
                Message = "ProductsSaved"
            });
        }


        public async Task<BalanceValidationResponse> ValidatePayment(List<OrderedProductEntity> products, string userId)
            {
            double price = 0;
            foreach (var product in products)
            {
                price += product.amount * product.price;
            }

            return userGrpcClient.ValidateBalance(new BalanceValidationRequest
            {
                UserId = userId,
                Payment = price
            });
        }

       public override Task<NOrders> GetOrderNumber(NOrdersRequest request, ServerCallContext context)
        {
            return Task.FromResult(new NOrders
            {
                Number = 1+databaseContext.Orders.Count()
            });         
        }

        private OrderedProductEntity MapToOrderedProductEntity(OrderedProduct grpcProduct)
        {
            OrderedProductEntity product = new OrderedProductEntity
            {
                product_id=grpcProduct.Id,
                name=grpcProduct.Name,
                amount=grpcProduct.Amount,
                price=grpcProduct.Price,
                manufacturer=grpcProduct.Manufacturer,
                additionalInfo=grpcProduct.AdditionalInfo,
            };
            return product;
        }

        private OrderGrpc MapToUserServiceOrderGrpc(Order order)
        {
            OrderGrpc orderGrpc = new OrderGrpc();
            orderGrpc.OrderId = order.order_id;
            orderGrpc.UserId = order.user_id;
            foreach (var product in order.products)
            {
                orderGrpc.Products.Add(new OrderedProductGrpc
                {
                    Id = product.product_id,
                    Name = product.name,
                    Amount = product.amount,
                    Price = product.price,
                    Manufacturer = product.manufacturer,
                    AdditionalInfo = product.additionalInfo,
                });
            }
            return orderGrpc;

        }

    }
}
