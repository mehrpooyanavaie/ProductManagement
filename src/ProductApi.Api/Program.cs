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
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.RateLimiting;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.ConfigureRabbitMqServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureApplicationDbContextService(builder.Configuration);
builder.Services.ConfigureUnitOfWorkService();
builder.Services.ConfigurMediatRServices();
builder.Services.ConfigureMappingService();
builder.Services.ConfigureRedisServices(builder.Configuration);
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var userId = context.User.Identity?.IsAuthenticated == true
            ? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            : "anonymous";

        return RateLimitPartition.GetTokenBucketLimiter(userId ?? "anonymous", _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 100,
            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
            TokensPerPeriod = 10,
            QueueLimit = 0,
            AutoReplenishment = true
        });
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");
    };
});



builder.Services.AddEndpointsApiExplorer();

AddSwagger(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
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

        o.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = " ProductApi Api"
        });
    });
}
