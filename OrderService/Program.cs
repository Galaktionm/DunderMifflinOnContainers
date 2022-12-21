using System.Net;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OrderService;
using OrderService.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddGrpcClient<ScrantonBranch.ScrantonGrpcService.ScrantonGrpcServiceClient>(options =>
{
    options.Address = new Uri("http://scrantonbranch:5122");
});
builder.Services.AddGrpcClient<NashuaBranch.NashuaGrpcService.NashuaGrpcServiceClient>(options =>
{
    options.Address = new Uri("http://nashuabranch:5008");
});

builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("HangfireDB")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, 5023, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddHangfire(config =>config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireDb")));
builder.Services.AddHangfireServer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGrpcService<OrderGrpcService>();
app.UseHangfireDashboard();

app.Run();
