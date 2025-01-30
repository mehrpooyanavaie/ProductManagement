# ProductManagement

## 🌍 Introduction

This is a **Product Management API** built using **ASP.NET Core 8** with a **Clean Architecture** approach.  
It follows **best practices** like **Unit of Work & Repository pattern**, **CQRS with MediatR**, and **manual garbage collection management** for optimized performance.  
The project also includes **RabbitMQ** for message queuing and **Redis** for caching.  
The database used is **SQL Server**.

---

## 🚀 Features

- ✅ **Clean Architecture** (Separation of Concerns)
- ✅ **Authentication & Authorization** with **JWT**
- ✅ **Entity Framework Core 8** for database management
- ✅ **MediatR** for **CQRS pattern**
- ✅ **AutoMapper** for object mapping
- ✅ **Redis** for caching products data
- ✅ **RabbitMQ** for event-driven messaging
- ✅ **SQL Server as the Database**
- ✅ **Unit of Work & Repository Pattern** for database abstraction
- ✅ **Manual Garbage Collection Optimization** for better performance

---

## 🔧 Installation & Setup

### Prerequisites

- .NET 8 SDK  
- SQL Server  
- Redis  
- RabbitMQ  

### Configuration

1. **Update `appsettings.json`** with your database and Redis connection settings.

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=ProductDb;Integrated Security=True;TrustServerCertificate=True"
}
Run Migrations:
dotnet ef database update

Start Redis & RabbitMQ If using Docker, run:
docker run --name redis -d -p 6379:6379 redis
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

Run the Application:
dotnet run

Access the API via Swagger:
📌 http://localhost:5000/swagger

📡 API Endpoints
🛍️ Products
Method	Endpoint	Description
GET	/api/products/getproducts	Get paginated list of products
GET	/api/products/getproduct/{id}	Get product by ID
POST	/api/products/CreateProductWithAutoEmailAndUserIdLoadingFromClaim	Create a new product (Authenticated)
PUT	/api/products/updateproduct/{id}	Update product (Authenticated)
DELETE	/api/products/deleteproduct/{id}	Delete product (Authenticated)
📦 Categories
Method	Endpoint	Description
GET	/api/category/GetAllCategories	Get all categories
POST	/api/category/CreateCategory	Create a new category (Authenticated)
👤 Authentication
Method	Endpoint	Description
POST	/api/account/login	User login
POST	/api/account/register	User registration
POST	/api/account/verification	Verify user with token
🏛️ Architecture Overview
This project follows Clean Architecture, ensuring separation of concerns and scalability.

✅ Layers:
API Layer - Handles HTTP requests and responses.
Application Layer - Contains business logic, CQRS commands, and MediatR handlers.
Infrastructure Layer - Manages database access, messaging (RabbitMQ), and caching (Redis).
Domain Layer - Contains core entities and domain logic.
✅ Design Patterns:
Unit of Work & Repository Pattern for better database management.
CQRS with MediatR for better separation of queries and commands.
Manual Garbage Collection Optimization using GC.Collect() for better memory management.
🔄 Message Queue (RabbitMQ)
RabbitMQ is used to send verification emails. Configuration is done in appsettings.json:

json
"RabbitMq": {
    "HostName": "localhost",
    "Port": 5672,
    "QueueName": "verification_queue"
}
The RabbitMqService is responsible for sending messages.

⚡ Performance Optimization
Redis Caching - Reduces database queries for frequently accessed data.
Rate Limiting - Prevents excessive API requests.
Manual Garbage Collection - GC.Collect() is used strategically to free up memory when needed.
EF Core Optimizations - Efficiently manages database connections.
🎯 Skills & Tools Demonstrated
✅ .NET 8 Web API
✅ SQL Server & EF Core 8
✅ CQRS & MediatR
✅ JWT Authentication & Identity
✅ Redis Caching
✅ RabbitMQ Messaging
✅ Unit of Work & Repository Pattern
✅ Manual Garbage Collection Optimization

📜 License
This project is open-source and free to use under the MIT License.

