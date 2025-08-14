# AspNetCore8-Dapper-SqlServer-Api-Demo
A clean and simple CRUD API demo built using;DapperApiWithStoredProcedures  AspNetCore8DapperCrud  DotNet8DapperSqlApi  ProductApi-DapperDemo

# ASP.NET Core 8 Web API with Dapper and SQL Server

A clean and simple CRUD API demo built using **ASP.NET Core 8**, **Dapper**, and **SQL Server**, featuring dual support for **raw SQL queries** and **stored procedures**.

---

## 📌 Features

- ✅ ASP.NET Core 8 Web API
- ✅ Lightweight data access using [Dapper](https://github.com/DapperLib/Dapper)
- ✅ SQL Server integration
- ✅ Support for both raw SQL and stored procedures
- ✅ Dependency Injection and Repository pattern
- ✅ Swagger (OpenAPI) for API testing
- ✅ Clean, minimal, and easy to understand

---

## 📂 Project Structure

DapperDemoAPI/
├── Controllers/
│ └── ProductController.cs
├── Models/
│ └── Product.cs
├── Repository/
│ ├── IProductRepository.cs
│ └── ProductRepository.cs
├── appsettings.json
├── Program.cs

sql
Copy
Edit

---

## 💾 Database Setup

Run the following SQL to create the database and table:

```sql
CREATE DATABASE DapperDemoDB;
GO

USE DapperDemoDB;
GO

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Price DECIMAL(18,2)
);
📌 Stored Procedures (Optional)
sql
Copy
Edit
CREATE PROCEDURE GetAllProducts AS
BEGIN
    SELECT * FROM Products
END

CREATE PROCEDURE GetProductById @Id INT AS
BEGIN
    SELECT * FROM Products WHERE Id = @Id
END

CREATE PROCEDURE InsertProduct
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2),
    @NewId INT OUTPUT
AS
BEGIN
    INSERT INTO Products (Name, Price)
    VALUES (@Name, @Price)
    SET @NewId = SCOPE_IDENTITY()
END

CREATE PROCEDURE UpdateProduct
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(18,2)
AS
BEGIN
    UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id
END

CREATE PROCEDURE DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM Products WHERE Id = @Id
END
🔧 Configuration
In appsettings.json, update your connection string:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DapperDemoDB;Trusted_Connection=True;"
}
🚀 Running the API
Open the solution in Visual Studio 2022

Set DapperDemoAPI as the startup project

Press F5 to run the API

Visit: https://localhost:{port}/swagger

🌐 API Endpoints
Method	Endpoint	Description
GET	/api/product	Get all products
GET	/api/product/{id}	Get product by ID
POST	/api/product	Create new product
PUT	/api/product/{id}	Update product
DELETE	/api/product/{id}	Delete product

🧠 You can append ?useStoredProc=true to any endpoint to use stored procedures instead of raw SQL.

🛠️ Technologies Used
.NET 8

Dapper

SQL Server

Swagger / Swashbuckle


