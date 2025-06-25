# 🛒 Mini E-commerce DDD

A clean, modular e-commerce API built with **.NET 8**, applying **Domain-Driven Design (DDD)** principles and a layered architecture for scalability and maintainability.

---

## 📦 Project Structure

```plaintext
Mini_Ecommerce_DDD/
├── Api/                     → Presentation Layer (Controllers, Middleware)
├── Application/             → Business Logic (Use Cases, Services, Interfaces)
├── Domain/                  → Core Domain (Entities, Value Objects, Enums)
├── Infrastructure/          → External Dependencies (EF Core, JWT, Email, etc.)
├── Contracts/               → DTOs and external-facing models
└── Requests/                → REST Client Test Files (.http)
```

Run

> dotnet run --project .\Api\
