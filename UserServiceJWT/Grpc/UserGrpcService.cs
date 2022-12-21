using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using UserService;
using UserServiceJWT.Entities;

namespace UserServiceJWT.Grpc
{
    public class UserGrpcService : UserGrpc.UserGrpcBase
    {
        private readonly UserManager<User> manager;
        private readonly DatabaseContext databaseContext;
        public UserGrpcService(UserManager<User> manager, DatabaseContext databaseContext)
        {
            this.manager=manager;
            this.databaseContext=databaseContext;
        }

        public async override Task<BalanceValidationResponse> ValidateBalance(BalanceValidationRequest request, ServerCallContext context)
        {
            var user=await manager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                var userBalance = user.balance;
                if (userBalance >= request.Payment)
                {
                    user.balance-=request.Payment;
                    await manager.UpdateAsync(user);
                    return new BalanceValidationResponse
                    {
                        Result = true,
                        Message = $"Approved request: Payment of {request.Payment} dollars for user with email {user.Email}",
                        AmountChanged = request.Payment
                    };
                }
                else
                {
                    return new BalanceValidationResponse
                    {
                        Result = false,
                        Message = $"Denied request: Payment of {request.Payment} dollars for user with email {user.Email}",
                        AmountChanged = 0
                    };
                }
            }

            else
            {
                return new BalanceValidationResponse
                {
                    Result = false,
                    Message = "User not found",
                    AmountChanged = 0
                };
            }
        }

        public async override Task<OrderReceivedValidation> SaveOrder(OrderGrpc orderGrpc, ServerCallContext context)
        {
            var order=SaveOrderToDatabase(orderGrpc);
            if(order != null)
            {
              
                var user=await manager.FindByIdAsync(orderGrpc.UserId);
                user.orders.Add(order);
                var x=await manager.UpdateAsync(user);              
                return new OrderReceivedValidation
                {
                    Result = true,
                    Message = "Order received"
                };
            }

            return new OrderReceivedValidation
            {
                Result = false,
                Message = "There was an error receiving the order"
            };



        }

        private Order SaveOrderToDatabase(OrderGrpc orderGrpc)
        {
                Order order = new Order(orderGrpc.OrderId, orderGrpc.UserId);
                databaseContext.Orders.Add(order);
                databaseContext.SaveChanges();

                List<OrderedProduct> orderedProducts = new List<OrderedProduct>();
                foreach (var product in orderGrpc.Products)
                {
                    orderedProducts.Add(new OrderedProduct(product.Name, product.Price, product.Amount,
                        product.Manufacturer, product.AdditionalInfo, orderGrpc.OrderId));
                }

                order.orderedProducts = orderedProducts;
                databaseContext.Update(order);
                databaseContext.SaveChanges();
                return order;
            } 
        }

    }

