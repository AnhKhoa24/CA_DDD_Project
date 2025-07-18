namespace Infrastructure.Cache;

public class CacheServiceSetting
{
   public const int DefaultCacheTime = 10;
   public const string SectionName = "CacheSettings";
   public string Configuration { get; set; } = null!;
   public string InstanceName { get; set; } = null!;
}