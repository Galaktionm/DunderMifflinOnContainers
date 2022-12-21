using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ScrantonBranch;
using ScrantonBranch.DTO;
using ScrantonBranch.Grpc;
using ScrantonBranch.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, 5122, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpcClient<ProductService.ProductGrpcProtoService.ProductGrpcProtoServiceClient>(options =>
{
    options.Address = new Uri("http://productservice:5152");
});

builder.Services.AddGrpcClient<UserService.UserGrpc.UserGrpcClient>(options =>
{
    options.Address = new Uri("http://userservicejwt:5015");
});

builder.Services.AddScoped<ProductRequestService>();


var app = builder.Build();


app.MapGrpcService<ScrantonBranchGrpcService>();


using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
     if (dbContext.Database.GetPendingMigrations().Any())
     {
        dbContext.Database.Migrate();
     }
    var service = scope.ServiceProvider.GetService<ProductRequestService>();
    List<ProductRequestDTO> dtoList=new List<ProductRequestDTO>();
    dtoList.Add(new ProductRequestDTO("", "White", 200, 20));
    service.SaveProducts(dtoList);
}

app.Run();
