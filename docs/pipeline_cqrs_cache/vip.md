# Cấu hình PipeLine Cache với CQRS

<pre>
📁 Application
│
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


## 1. Interface `ICachedQuery

```csharp
// Application/Common/Interfaces/ICachedQuery.cs
public interface ICachedQuery
{
    string GetCacheKey();
    TimeSpan GetExpiration();
}

```

## 2. Query Example

```csharp
// Application/Menu/Queries/GetMenuPaginateQuery.cs
public class GetMenuPaginateQuery : IRequest<MenuResult>, ICachedQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string GetCacheKey()
        => $"DDDApp:menu:page={PageNumber}&size={PageSize}";

    public TimeSpan GetExpiration()
        => TimeSpan.FromMinutes(5);
}

```

## 4. Handler: chỉ tập trung vào logic **Get**

```csharp
// Application/Menu/Queries/GetMenuPaginateQueryHandler.cs
public class GetMenuPaginateQueryHandler : IRequestHandler<GetMenuPaginateQuery, MenuResult>
{
    private readonly IMenuRepository _menuRepository;

    public GetMenuPaginateQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuResult> Handle(GetMenuPaginateQuery request, CancellationToken cancellationToken)
    {
        var menus = await _menuRepository.GetMenusPaginateAsync(request.PageNumber, request.PageSize);
        return new MenuResult(request.PageSize, request.PageNumber, menus.ConvertMenuCommandResult());
    }
}

```

## 5. Tạo pipeline get/set cache tự động

```csharp
// Application/Common/Behaviors/CacheBehavior.cs
public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IGenericCacheService _cacheService;

    public CacheBehavior(IGenericCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ICachedQuery cachedQuery)
        {
            return await next(); // Bỏ qua nếu không implement ICachedQuery
        }

        return await _cacheService.GetOrAddAsync(
            cachedQuery.GetCacheKey(),
            next, // Gọi handler thực tế
            cachedQuery.GetExpiration(),
            cancellationToken);
    }
}

```

## 6. Cấu hình MediatR để dùng pipeline behavior trong DI

```csharp
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));

```

## ***NOTE: Tất cả các query nào cần dùng logic get/set cache chỉ cần kết thừa interface `ICachedQuery`
