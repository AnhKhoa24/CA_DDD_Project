# 🛒 Mini Ecommerce - DDD & Clean Architecture

Chào mừng bạn đến với dự án **Mini Ecommerce**, ứng dụng thử nghiệm mô hình **Domain-Driven Design (DDD)** kết hợp **Clean Architecture**. Mục tiêu là xây dựng kiến trúc rõ ràng, dễ bảo trì, tách bạch các tầng và tuân thủ nguyên tắc SOLID, giúp phát triển, mở rộng và bảo trì dự án lâu dài.

Dưới đây là **hình minh họa mô hình DDD** mà dự án này áp dụng


<p align="center">
  <img src="docs/ddd_layers.png" alt="DDD Layer" width="400"/>
</p>


---

## 📂 Cấu trúc thư mục

Mini_Ecommerce_DDD/

├── Api/                     → Presentation Layer (Controllers, Middleware)

├── Application/             → Business Logic (Use Cases, Services, Interfaces)

├── Domain/                  → Core Domain (Entities, Value Objects, Enums)

├── Infrastructure/          → External Dependencies (EF Core, JWT, Email, etc.)

├── Contracts/               → DTOs and external-facing models

└── Requests/                → REST Client Test Files (.http)

---

## ⚙️ Cài đặt cấu hình

1️⃣ Sau khi clone dự án, đổi tên file `appsettings.example.json` thành `appsettings.json`.

📄 **Nội dung mẫu appsettings.json:**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "Secret": "",
    "ExpiryMinutes": 60,
    "Issuer": "",
    "Audience": ""
  },
  "ConnectionStrings": {
    "Connection": ""
  }
}
```

🔎 **Mô tả các thông số cấu hình:**

* **Logging** : Thiết lập mức độ log cho ứng dụng và ASP.NET Core.
* **AllowedHosts** : Các domain được phép truy cập API.
* **JwtSettings** :
* `Secret`: Khóa bí mật ký JWT.
* `ExpiryMinutes`: Thời gian hết hạn token (phút).
* `Issuer`: Đơn vị phát hành token.
* `Audience`: Đối tượng sử dụng token.
* **ConnectionStrings.Connection** : Chuỗi kết nối database (SQL Server, PostgreSQL,...).

## 🚀 Chạy dự án

🔧 Từ thư mục gốc, chạy thẳng:

```
dotnet run --project ./Api/
```

🔁 Hoặc để tự động reload khi thay đổi code:

```bash
dotnet watch run --project ./Api/
```


## ✅ Chạy Unit Tests

Tại thư mục gốc:

```bash
dotnet run --project ./Api/
```
