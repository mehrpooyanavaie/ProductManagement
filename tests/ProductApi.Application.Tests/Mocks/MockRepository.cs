using ProductApi.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Domain.Entities;


namespace ProductApi.Application.Tests.Mocks
{
    public static class MockProductRepository
    {
        public static Mock<IUnitOfWork> GetProductRepository()
        {
            var products = new List<Product>()
            {
                new Product
                {
                Id = 1,
                ManufactureEmail = "mnavaienezhad@gmail.com",
                Name = "Test1",
                ManufacturePhone = "09129994444",
                ProduceDate = new DateTime(2012, 12, 25, 10, 30, 50),
                UserId = "mpnv168"
                },
                new Product
                {
                Id = 2 ,
                ManufactureEmail = "uglyyducklingg@gmail.com",
                Name = "Test2",
                ManufacturePhone = "09129992222",
                ProduceDate = new DateTime(2012, 12, 25, 10, 30, 50),
                UserId = "msnvzh"
                }
            };

            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(r => r.ProductRepository.GetAllAsync()).ReturnsAsync(products);

            mockRepo.Setup(r => r.ProductRepository.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) =>
                {
                    products.Add(product);
                    return product.Id;
                });


            return mockRepo;
        }
    }
}
