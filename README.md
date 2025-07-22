# AK DDD + CA Template

Ứng dụng thử nghiệm mô hình **Domain-Driven Design (DDD)** kết hợp **Clean Architecture**. Mục tiêu là xây dựng kiến trúc rõ ràng, dễ bảo trì, tách bạch các tầng và tuân thủ nguyên tắc SOLID, giúp phát triển, mở rộng và bảo trì dự án lâu dài.

Dưới đây là **hình minh họa mô hình DDD** mà dự án này áp dụng

<p align="center">
  <img src="docs/ddd_layers.png" alt="DDD Layer" width="400"/>
</p>

---

## 📂 Cấu trúc thư mục

<pre>
📁 Database
📁 Requests
├── 📁 Authentication
|    └── 🌐 Login.http 
│   .... // To do...
📁 src
├── 📁 Api
│   ├── 📁 Common
│   ├── 📁 Controllers

├── 📁 Application
├── 📁 Contracts
├── 📁 Domain
├── 📁 Infrastructure

├── 📁 Menu
│   ├── 📁 Commands
│   │   └── CreateMenuCommand.cs
│   │   └── ...
│   ├── 📁 Queries
│   │   └── GetMenuPaginateQuery.cs        👈 implements ICachedQuery
│   │   └── GetMenuPaginateQueryHandler.cs
│
├── 📁 Common
│   ├── Interfaces
│   │   └── ICachedQuery.cs                ✅ Interface đánh dấu Query cần cache
│   ├── Behaviors
│   │   └── CacheBehavior.cs               ✅ PipelineBehavior tự động cache
</pre>

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

## Chạy dự án

🔧 Từ thư mục gốc, chạy thẳng:

```
dotnet run --project ./Api/
```

🔁 Hoặc để tự động reload khi thay đổi code:

```bash
dotnet watch run --project ./Api/
```

## Kiểm soát lỗi toàn cục

Cấu hình tại:

```
📁 src  
├── 📁 Api  
│   ├── 📁 Controller  
│        └── 📝 ErrorsController.cs 
│   └── ⚙️ Program.cs
```

Dùng controller ErrorsController làm middleware khi dự án ném ra 1 ngoại lệ không biết trước

```csharp
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        // return Problem(detail: exception?.Message);   //Development mode only
        //To do,... ex: save error 
        return Problem();
    }
}
```

Đăng ký trong Program.cs

```csharp
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    ....
    app.Run();
}
```

## Mapping với Mapster

📁 src
├── 📁 Api
│   ├── 📁 Common
│        └── 📁 Mapping
│                  └── 📝 MenuMapping.cs

Mapping dữ dễ dàng với Mapster:

```csharp
public class MenuMappingConfig : IRegister
{
   public void Register(TypeAdapterConfig config)
   {
      config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
         .Map(dest => dest.HostId, src => src.HostId)
         .Map(dest => dest, src => src.Request);

      config.NewConfig<(UpdateMenuRequest Request, string HostId), UpdateMenuCommand>()
         .Map(dest => dest.HostId, src => src.HostId)
         .Map(dest => dest, src => src.Request);
      //To do ...........
   }
}
```

Sử dụng dễ dàng:

```cshap
var command = _mapper.Map<CreateMenuCommand>((request, hostId));
```
