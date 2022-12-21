using System.Net;
using Aggregator;
using Aggregator.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddSingleton<RedisConnectionService>();
builder.Services.AddScoped<RedisBasketRepository>();

builder.Services.AddGrpcClient<OrderService.Order.OrderClient>(options =>
{
    options.Address = new Uri("http://orderservice:5023");
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, 5098, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddCors(options =>
 options.AddPolicy(name: "AngularPolicy",
 cfg => {
     cfg.AllowAnyHeader();
     cfg.AllowAnyMethod();
     cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
 }));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWTParams:Issuer"],
        ValidAudience = builder.Configuration["JWTParams:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
   GetBytes(builder.Configuration["JWTParams:SecurityKey"]))
    };
});



var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var redis=scope.ServiceProvider.GetService<RedisConnectionService>();
    IDatabase db=redis.connection.GetDatabase();
    var server = redis.connection.GetServer("redis:6379");
    foreach(var key in server.Keys())
    {
        db.KeyDelete(key);
    }
}

app.UseCors("AngularPolicy");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
