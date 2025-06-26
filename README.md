# 🏡 MagicVilla API

**MagicVilla API** is a RESTful service built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**. It provides endpoints for managing villa property data, including full CRUD functionality. This project demonstrates clean controller structure, DTO usage, and basic logging for educational and production-ready API development.

---

## 🚀 Features

- Full CRUD operations for Villa entities
- Clean separation using DTOs and Models
- Logging service for API activity
- JSON Patch support for partial updates
- SQL Server database integration via Entity Framework
- Model validation with clear error responses
- REST conventions with proper HTTP status codes

---

## 🛠️ Tech Stack

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JSON Patch / REST API  
- C#  
- Dependency Injection  
- Swagger (optional for testing)

---

## 📦 How to Run

1. Clone the repository:

```bash
git clone https://github.com/xatrarana/MagicVilla.git
cd MagicVilla
```
### 🧾 Setup Instructions

1. **Update the connection string** in `appsettings.json` to point to your local SQL Server instance.

2. **Apply migrations** to create the database:

```bash
dotnet ef database update
```
3. Start the application:

```bash
dotnet run
```
4. The API will be available at:

```bash
https://localhost:{port}/api/VillaAPI
```
