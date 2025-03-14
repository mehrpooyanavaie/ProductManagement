```markdown
# ProductManagement

## 🌍 Introduction

This is a **Product Management API** built using **ASP.NET Core 8** with a **Clean Architecture** approach. It follows **best practices** such as the **Unit of Work & Repository pattern**, **CQRS with MediatR**, and **manual garbage collection management** for optimized performance. The project also includes **RabbitMQ** for message queuing and **Redis** for caching. The database used is **SQL Server**.

This API is designed to streamline product management processes and is ideal for product managers, software developers, system architects, and DevOps professionals seeking scalable and efficient solutions.

---

## 🚀 Features

- ✅ **Clean Architecture** (Separation of Concerns)
- ✅ **Authentication & Authorization** with **JWT**
- ✅ **Entity Framework Core 8** for database management
- ✅ **MediatR** for the **CQRS pattern**
- ✅ **AutoMapper** for object mapping
- ✅ **Redis** for caching product data
- ✅ **RabbitMQ** for event-driven messaging
- ✅ **SQL Server** as the database
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

1. **Update `appsettings.json`** with your database and Redis connection settings:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=.;Initial Catalog=ProductDb;Integrated Security=True;TrustServerCertificate=True"
     }
   }
   ```

2. **Run Migrations:**

   ```bash
   dotnet ef database update
   ```

3. **Start Redis & RabbitMQ:**

   If using Docker, run the following commands:

   ```bash
   docker run --name redis -d -p 6379:6379 redis
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
   ```

4. **Run the Application:**

   ```bash
   dotnet run
   ```

5. **Access the API via Swagger:**

   Open your browser and navigate to:  
   [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 📡 API Endpoints

### 🛍️ Products

| Method | Endpoint                                                                     | Description                                  |
| ------ | ---------------------------------------------------------------------------- | -------------------------------------------- |
| GET    | `/api/products/getproducts`                                                  | Get paginated list of products               |
| GET    | `/api/products/getproduct/{id}`                                              | Get product by ID                            |
| POST   | `/api/products/CreateProductWithAutoEmailAndUserIdLoadingFromClaim`            | Create a new product (Authenticated)         |
| PUT    | `/api/products/updateproduct/{id}`                                           | Update product (Authenticated)               |
| DELETE | `/api/products/deleteproduct/{id}`                                           | Delete product (Authenticated)               |

### 📦 Categories

| Method | Endpoint                         | Description                               |
| ------ | -------------------------------- | ----------------------------------------- |
| GET    | `/api/category/GetAllCategories` | Get all categories                        |
| POST   | `/api/category/CreateCategory`   | Create a new category (Authenticated)     |

### 👤 Authentication

| Method | Endpoint                        | Description                   |
| ------ | ------------------------------- | ----------------------------- |
| POST   | `/api/account/login`            | User login                    |
| POST   | `/api/account/register`         | User registration             |
| POST   | `/api/account/verification`     | Verify user with token        |

---

## 🏛️ Architecture Overview

This project follows Clean Architecture, ensuring separation of concerns and scalability.

### **Layers:**
- **API Layer:** Handles HTTP requests and responses.
- **Application Layer:** Contains business logic, CQRS commands, and MediatR handlers.
- **Infrastructure Layer:** Manages database access, messaging (RabbitMQ), and caching (Redis).
- **Domain Layer:** Contains core entities and domain logic.

### **Design Patterns:**
- **Unit of Work & Repository Pattern:** For better database management.
- **CQRS with MediatR:** For better separation of queries and commands.
- **Manual Garbage Collection Optimization:** Uses `GC.Collect()` for improved memory management.

### **Message Queue (RabbitMQ):**

RabbitMQ is used to send verification emails. Configuration is done in `appsettings.json`:

   ```json
   {
     "RabbitMq": {
       "HostName": "localhost",
       "Port": 5672,
       "QueueName": "verification_queue"
     }
   }
   ```

The `RabbitMqService` is responsible for sending messages.

### **Performance Optimization:**
- **Redis Caching:** Reduces database queries for frequently accessed data.
- **Rate Limiting:** Prevents excessive API requests.
- **Manual Garbage Collection:** `GC.Collect()` is used strategically to free up memory.
- **EF Core Optimizations:** Efficiently manages database connections.

---

## 👥 Who Can Benefit

This project is ideal for:
- **Product Managers:** Seeking an efficient API to manage product data.
- **Software Developers:** Interested in implementing Clean Architecture, CQRS, and modern .NET practices.
- **System Architects:** Designing scalable systems with optimal performance.
- **DevOps Professionals:** Looking to deploy and manage high-performance API solutions.
- **Students & Researchers:** Exploring best practices in software architecture and performance optimization.

---

## 🎯 Skills & Tools Demonstrated

- ✅ .NET 8 Web API
- ✅ SQL Server & EF Core 8
- ✅ CQRS & MediatR
- ✅ JWT Authentication & Identity
- ✅ Redis Caching
- ✅ RabbitMQ Messaging
- ✅ Unit of Work & Repository Pattern
- ✅ Manual Garbage Collection Optimization

---

## 📜 License

This project is open-source and free to use under the MIT License.

---

## 📬 Contact

For further questions, please feel free to email me at **mnavaienezhad@gmail.com**.
```
