using System.Runtime.InteropServices;

namespace DistantWorlds; 

public static class UxTheme {

  public enum SetThemeAppPropertiesFlags : uint {
    AllowNonClient = 0x00000001,
    AllowControls = 0x00000002,
    AllowWebContent = 0x00000004
  }
  
  private const string DllName = "UxTheme";

  [DllImport(DllName)]
  public static extern void SetThemeAppProperties(SetThemeAppPropertiesFlags flags);


}