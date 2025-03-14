# Product Management API

## Overview

The **Product Management API** is a robust and scalable solution built on **ASP.NET Core 8** with a **Clean Architecture** approach. Designed with best practices in mind, it incorporates the **Unit of Work & Repository Pattern**, **CQRS with MediatR**, and strategic **manual garbage collection** for optimized performance. In addition, the API integrates **RabbitMQ** for reliable messaging and **Redis** for efficient caching, while utilizing **SQL Server** as the primary database.

This API is ideal for product managers, software developers, system architects, and DevOps professionals looking to streamline product management workflows in a modern, high-performance environment.

---

## Key Features

- **Clean Architecture:** Ensures clear separation of concerns for maintainability.
- **JWT Authentication & Authorization:** Secures access to API endpoints.
- **Entity Framework Core 8:** Simplifies database operations with powerful ORM capabilities.
- **CQRS with MediatR:** Distinguishes between command and query responsibilities.
- **AutoMapper:** Streamlines object-to-object mapping.
- **Redis Caching:** Minimizes database load by caching frequently accessed data.
- **RabbitMQ Messaging:** Supports event-driven architecture and reliable message queuing.
- **SQL Server:** Provides a robust, scalable data storage solution.
- **Unit of Work & Repository Pattern:** Abstracts database interactions for cleaner code.
- **Manual Garbage Collection Optimization:** Enhances performance by managing memory explicitly.

---

## Installation & Setup

### Prerequisites

Before getting started, ensure you have the following installed and configured:
- **.NET 8 SDK**
- **SQL Server**
- **Redis**
- **RabbitMQ**

### Step-by-Step Configuration

1. **Configure Connection Strings:**

   Update your `appsettings.json` with the correct database and Redis connection details:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=.;Initial Catalog=ProductDb;Integrated Security=True;TrustServerCertificate=True"
     }
   }
Run Database Migrations:

Apply the latest database schema changes by running:

bash
Copy
dotnet ef database update
Launch Redis & RabbitMQ:

If using Docker, run the following commands:

bash
Copy
docker run --name redis -d -p 6379:6379 redis
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
Start the Application:

Launch the API with:

bash
Copy
dotnet run
Access API Documentation:

Open your browser and navigate to http://localhost:5000/swagger to explore the API via Swagger.

API Endpoints
Products
Method	Endpoint	Description
GET	/api/products/getproducts	Retrieve a paginated list of products
GET	/api/products/getproduct/{id}	Retrieve details of a specific product by ID
POST	/api/products/CreateProductWithAutoEmailAndUserIdLoadingFromClaim	Create a new product (authentication required)
PUT	/api/products/updateproduct/{id}	Update an existing product (authentication required)
DELETE	/api/products/deleteproduct/{id}	Delete a product (authentication required)
Categories
Method	Endpoint	Description
GET	/api/category/GetAllCategories	Retrieve all product categories
POST	/api/category/CreateCategory	Create a new category (authentication required)
Authentication
Method	Endpoint	Description
POST	/api/account/login	User login
POST	/api/account/register	User registration
POST	/api/account/verification	Verify user with token
Architecture & Design
Layered Structure
API Layer: Handles incoming HTTP requests and outgoing responses.
Application Layer: Encapsulates business logic, CQRS commands, and MediatR handlers.
Infrastructure Layer: Manages data access, messaging via RabbitMQ, and caching with Redis.
Domain Layer: Contains core entities and domain-specific logic.
Design Patterns
Unit of Work & Repository Pattern: Provides a clean abstraction for database interactions.
CQRS with MediatR: Segregates read and write operations to improve maintainability.
Manual Garbage Collection: Strategically invokes GC.Collect() to manage memory and enhance performance.
Messaging with RabbitMQ
For sending verification emails, RabbitMQ is configured in appsettings.json:

json
Copy
{
  "RabbitMq": {
    "HostName": "localhost",
    "Port": 5672,
    "QueueName": "verification_queue"
  }
}
A dedicated service (RabbitMqService) is responsible for managing message dispatch.

Performance Enhancements
Redis Caching: Improves response times by reducing redundant database queries.
Rate Limiting: Protects the API from excessive requests.
EF Core Optimizations: Ensures efficient database connection handling.
Who Can Benefit
This API is tailored for:

Product Managers: Seeking efficient solutions for managing product data.
Software Developers: Interested in modern .NET practices and scalable architecture.
System Architects: Designing robust and maintainable systems.
DevOps Professionals: Focused on deploying and managing high-performance APIs.
Students & Researchers: Exploring industry best practices in software design and optimization.
Technologies & Skills
.NET 8 Web API
SQL Server & EF Core 8
CQRS & MediatR
JWT Authentication & Identity
Redis Caching
RabbitMQ Messaging
Unit of Work & Repository Pattern
Manual Garbage Collection Optimization
License
This project is open-source and distributed under the MIT License. You are free to use, modify, and distribute it.

Contact
For further questions or support, please reach out at:
mnavaienezhad@gmail.com

Copy





You said:
میشه ذکر کنی برای ریت لیمیت از سواشل باکل استفاده میشه
Reasoned for 13 seconds
markdown
Copy
# Product Management API

## Overview

The **Product Management API** is a robust and scalable solution built on **ASP.NET Core 8** with a **Clean Architecture** approach. Designed with best practices in mind, it incorporates the **Unit of Work & Repository Pattern**, **CQRS with MediatR**, and strategic **manual garbage collection** for optimized performance. In addition, the API integrates **RabbitMQ** for reliable messaging and **Redis** for efficient caching, while utilizing **SQL Server** as the primary database.

This API is ideal for product managers, software developers, system architects, and DevOps professionals looking to streamline product management workflows in a modern, high-performance environment.

---

## Key Features

- **Clean Architecture:** Ensures clear separation of concerns for maintainability.
- **JWT Authentication & Authorization:** Secures access to API endpoints.
- **Entity Framework Core 8:** Simplifies database operations with powerful ORM capabilities.
- **CQRS with MediatR:** Distinguishes between command and query responsibilities.
- **AutoMapper:** Streamlines object-to-object mapping.
- **Redis Caching:** Minimizes database load by caching frequently accessed data.
- **RabbitMQ Messaging:** Supports event-driven architecture and reliable message queuing.
- **SQL Server:** Provides a robust, scalable data storage solution.
- **Unit of Work & Repository Pattern:** Abstracts database interactions for cleaner code.
- **Manual Garbage Collection Optimization:** Enhances performance by managing memory explicitly.

---

## Installation & Setup

### Prerequisites

Before getting started, ensure you have the following installed and configured:
- **.NET 8 SDK**
- **SQL Server**
- **Redis**
- **RabbitMQ**

### Step-by-Step Configuration

1. **Configure Connection Strings:**

   Update your `appsettings.json` with the correct database and Redis connection details:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=.;Initial Catalog=ProductDb;Integrated Security=True;TrustServerCertificate=True"
     }
   }
Run Database Migrations:

Apply the latest database schema changes by running:

bash
Copy
dotnet ef database update
Launch Redis & RabbitMQ:

If using Docker, run the following commands:

bash
Copy
docker run --name redis -d -p 6379:6379 redis
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
Start the Application:

Launch the API with:

bash
Copy
dotnet run
Access API Documentation:

Open your browser and navigate to http://localhost:5000/swagger to explore the API via Swagger.

API Endpoints
Products
Method	Endpoint	Description
GET	/api/products/getproducts	Retrieve a paginated list of products
GET	/api/products/getproduct/{id}	Retrieve details of a specific product by ID
POST	/api/products/CreateProductWithAutoEmailAndUserIdLoadingFromClaim	Create a new product (authentication required)
PUT	/api/products/updateproduct/{id}	Update an existing product (authentication required)
DELETE	/api/products/deleteproduct/{id}	Delete a product (authentication required)
Categories
Method	Endpoint	Description
GET	/api/category/GetAllCategories	Retrieve all product categories
POST	/api/category/CreateCategory	Create a new category (authentication required)
Authentication
Method	Endpoint	Description
POST	/api/account/login	User login
POST	/api/account/register	User registration
POST	/api/account/verification	Verify user with token
Architecture & Design
Layered Structure
API Layer: Handles incoming HTTP requests and outgoing responses.
Application Layer: Encapsulates business logic, CQRS commands, and MediatR handlers.
Infrastructure Layer: Manages data access, messaging via RabbitMQ, and caching with Redis.
Domain Layer: Contains core entities and domain-specific logic.
Design Patterns
Unit of Work & Repository Pattern: Provides a clean abstraction for database interactions.
CQRS with MediatR: Segregates read and write operations to improve maintainability.
Manual Garbage Collection: Strategically invokes GC.Collect() to manage memory and enhance performance.
Messaging with RabbitMQ
For sending verification emails, RabbitMQ is configured in appsettings.json:

json
Copy
{
  "RabbitMq": {
    "HostName": "localhost",
    "Port": 5672,
    "QueueName": "verification_queue"
  }
}
A dedicated service (RabbitMqService) is responsible for managing message dispatch.

Performance Enhancements
Redis Caching: Improves response times by reducing redundant database queries.
Rate Limiting: Protects the API from excessive requests, implemented using Swashbuckle.
EF Core Optimizations: Ensures efficient database connection handling.
Who Can Benefit
This API is tailored for:

Product Managers: Seeking efficient solutions for managing product data.
Software Developers: Interested in modern .NET practices and scalable architecture.
System Architects: Designing robust and maintainable systems.
DevOps Professionals: Focused on deploying and managing high-performance APIs.
Students & Researchers: Exploring industry best practices in software design and optimization.
Technologies & Skills
.NET 8 Web API
SQL Server & EF Core 8
CQRS & MediatR
JWT Authentication & Identity
Redis Caching
RabbitMQ Messaging
Unit of Work & Repository Pattern
Manual Garbage Collection Optimization
License
This project is open-source and distributed under the MIT License. You are free to use, modify, and distribute it.

Contact
For further questions or support, please reach out at:
mnavaienezhad@gmail.com

Copy
