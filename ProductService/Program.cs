using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using ProductService;
using ProductService.Grpc;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, 5152, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();
builder.Services.AddControllers();  

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

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
 options.AddPolicy(name: "AngularPolicy",
 cfg => {
     cfg.AllowAnyHeader();
     cfg.AllowAnyMethod();
     cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
 }));

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("ProductDatabase"));

builder.Services.AddSingleton<DatabaseService>();

var app = builder.Build();


using(var scope = app.Services.CreateScope())
{
    var databaseService = scope.ServiceProvider.GetService<DatabaseService>();
    databaseService.DropAll();
    Product product = new Product("White Paper", 20, 400, "500 sheets in one box", "Paper mill");
    Product product2 = new Product("White Paper Premium", 50, 200, "300 sheets in one box. Excellent for prestigious businesses", "Paper mill");
    Product product3 = new Product("Colored Paper", 30, 300, "80 sheets, 8 colors, 10 each", "PaperFactory");
    Product product4 = new Product("Colored Paper Extra", 50, 200, "800 sheets, 8 colors, 100 each", "PaperFactory");
    await databaseService.CreateAsync(product);
    await databaseService.CreateAsync(product2);
    await databaseService.CreateAsync(product3);
    await databaseService.CreateAsync(product4);
}

app.UseCors("AngularPolicy");

app.MapControllers();

app.MapGrpcService<ProductGrpcService>();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
