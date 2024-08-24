using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Identity;
using System.Text;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Infrastructure.UnitOfWork;
using MediatR;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using ProductApi.Application.Mapping;
using ProductApi.Application.ProductsFeatures;
using ProductApi.Infrastructure.RabbitMq;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.ConfigureRabbitMqServices();
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureApplicationDbContextService(builder.Configuration);
builder.Services.ConfigureUnitOfWorkService();
builder.Services.ConfigurMediatRServices();
builder.Services.ConfigureMappingService();
// builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
AddSwagger(builder.Services);
// builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);
// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(ProductApi.Application.Products.Commands.CreateProductCommand).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(o =>
    {
        o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 1234sddsw'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        o.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

        o.SwaggerDoc("v1",new OpenApiInfo()
        {
            Version = "v1",
            Title = " ProductApi Api"
        });
    });
}
