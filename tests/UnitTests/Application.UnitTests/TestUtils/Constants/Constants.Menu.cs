namespace UnitTests.Application.UnitTests.TestUtils.Constants;


public static partial class Constants
{
   public static class Menu
   {
      public const string HostId = "xxxx-ddddd-xxxx-dddd";
      public const string Name = "Menu Name";
      public const string Description = "Menu Description";
      public const string SectionName = "Menu Section Name";
      public const string SectionDescription = "Menu Section Description";
      public const string ItemName = "Menu Item Name";
      public const string ItemDescription = "Menu Item Description";
      public static string SectionNameFromIndex(int index = 0) => $"{SectionName} {index}";
      public static string SectionDescriptionFromIndex(int index = 0) => $"{SectionDescription} {index}";
      public static string ItemNameFromIndex(int index) => $"{ItemName} {index}";
      public static string ItemDescriptionFromIndex(int index) => $"{ItemDescription} {index}";
   }
}