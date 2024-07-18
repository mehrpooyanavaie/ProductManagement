using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Infrastructure.Persistence;
using System.Text;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Infrastructure.UnitOfWork;
using MediatR;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // Add Identity services
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();

// // Configure JWT authentication
// // builder.Services.AddAuthentication(options =>
// // {
// //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
// //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// // })
// // .AddJwtBearer(options =>
// // {
//     // options.TokenValidationParameters = new TokenValidationParameters
//     // {
//     //     ValidateIssuer = true,
//     //     ValidateAudience = true,
//     //     ValidateLifetime = true,
//     //     ValidateIssuerSigningKey = true,
//     //     ValidIssuer = builder.Configuration["Jwt:Issuer"],
//     //     ValidAudience = builder.Configuration["Jwt:Audience"],
//     //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//     // };
// // });


// builder.Services.AddAuthorization();

// // builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductApi.Application.Products.Commands.CreateProductCommandHandler).Assembly));
// builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);
// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);

// // Register application services
// builder.Services.AddTransient<ProductApi.Application.Interfaces.IProductRepository,ProductApi.Infrastructure.Repositories.ProductRepository>();

// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// var tokenValidationParameters = new TokenValidationParameters()
// {
//     ValidateIssuerSigningKey = true,
//     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"])),
//     ValidateIssuer = true,
//     ValidIssuer = builder.Configuration["JWT:Issuer"],
//     ValidateAudience = true,
//     ValidAudience = builder.Configuration["JWT:Audience"],
//     ValidateLifetime = true,
//     ClockSkew = TimeSpan.Zero
// };
// builder.Services.AddSingleton(tokenValidationParameters);
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//         options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//     };
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = tokenValidationParameters;
// });

// app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllers();

// app.Run();
// // "Issuer": "http://localhost:41684"

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);

// Add Application services
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
