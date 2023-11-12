// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SteamAPI
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

#if WIN32
extern alias w32;
#elif WIN64
extern alias w64;
#else
extern alias w64;
extern alias w32;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DistantWorlds.Types {

  public class SteamAPI {

    //private static DWSteamAPI _DWSteamAPI;
    private static bool _Initialized = false;

    private static string _CrashLogPath;

    private static Hashtable _AchievementStatuses = new Hashtable();

    private static uint _SteamAppId;

    private static readonly SteamAPI.Finalizer _Finalizer = new SteamAPI.Finalizer();

    private static readonly bool Is64Bit = Environment.Is64BitProcess;

    private static readonly Action<uint, bool> _SteamClientInit
#if WIN32
      = w32.Steamworks.SteamClient.Init;
#elif WIN64
      = w64.Steamworks.SteamClient.Init;
#else
      = Is64Bit
        ? w64.Steamworks.SteamClient.Init
        : w32.Steamworks.SteamClient.Init;
#endif

    private static readonly Func<bool, bool> _SteamUserStatsResetAll
#if WIN32
      = w32.Steamworks.SteamUserStats.ResetAll;
#elif WIN64
      = w64.Steamworks.SteamUserStats.ResetAll;
#else
      = Is64Bit
        ? w64.Steamworks.SteamUserStats.ResetAll
        : w32.Steamworks.SteamUserStats.ResetAll;
#endif

    private static readonly Func<bool> _SteamUserStatsRequestCurrentStats
#if WIN32
      = w32.Steamworks.SteamUserStats.RequestCurrentStats;
#elif WIN64
      = w64.Steamworks.SteamUserStats.RequestCurrentStats;
#else
      = Is64Bit
        ? w64.Steamworks.SteamUserStats.RequestCurrentStats
        : w32.Steamworks.SteamUserStats.RequestCurrentStats;
#endif

    private static bool IsLoggedOn
#if WIN32
      => w32.Steamworks.SteamClient.IsLoggedOn;
#elif WIN64
      => w64.Steamworks.SteamClient.IsLoggedOn;
#else
      => Is64Bit
        ? w64.Steamworks.SteamClient.IsLoggedOn
        : w32.Steamworks.SteamClient.IsLoggedOn;
#endif

    //static SteamAPI() => SteamAPI._DWSteamAPI = new DWSteamAPI();

    public static void Initialize(string crashLogPath) {
      SteamAPI._Initialized = false;
      SteamAPI._CrashLogPath = crashLogPath;
      try {
        _SteamClientInit(261470, true);
        //SteamAPI._SteamAppId = SteamAPI._DWSteamAPI.GetAppID();
        //SteamClient.GetPlayerSteamLevel();

        if (!IsLoggedOn)
          return;

        _SteamUserStatsRequestCurrentStats();
        //SteamAPI._DWSteamAPI.RequestCurrentStats();
        SteamAPI._Initialized = true;
      }
      catch (Exception ex) {
        SteamAPI.WriteSteamError(nameof(Initialize), ex.ToString(), SteamAPI._CrashLogPath);
      }
    }

    public static void SetAchievementIfNecessary(Achievement achievement) {
      if (!SteamAPI._Initialized || achievement == null)
        return;

      string achievementName = SteamAPI.ResolveAchievementName(achievement);
      if (string.IsNullOrEmpty(achievementName)) { return; }

      try {
        var ach = Achievements.FirstOrDefault(x => x.Name == achievementName);
        if (!string.IsNullOrEmpty(ach.Name)) {
          ach.Trigger(true);
        }
      }
      catch (Exception ex) {
        SteamAPI.WriteSteamError(nameof(SetAchievementIfNecessary), ex.ToString(), SteamAPI._CrashLogPath);
      }
    }

    private static IEnumerable<dynamic> Achievements
#if WIN32
    => (dynamic)w32.Steamworks.SteamUserStats.Achievements;
#elif WIN64
    => (dynamic)w64.Steamworks.SteamUserStats.Achievements;
#else
      => Is64Bit
        ? (dynamic)w64.Steamworks.SteamUserStats.Achievements
        : (dynamic)w32.Steamworks.SteamUserStats.Achievements;
#endif

    public static string ResolveAchievementName(Achievement achievement) {
      string str = string.Empty;
      if (achievement != null && achievement.Type != AchievementType.AchieveAllRaceVictoryConditions)
        str = Galaxy.ResolveDescription(achievement);
      return str;
    }

    public static void ResetAllStatsAndAchievements() {
      if (!SteamAPI._Initialized)
        return;

      try {
        _SteamUserStatsResetAll(true);
      }
      catch (Exception ex) {
        SteamAPI.WriteSteamError(nameof(ResetAllStatsAndAchievements), ex.ToString(), SteamAPI._CrashLogPath);
      }
    }

    public static void Shutdown() {
      try {
#if WIN32
        w32.Steamworks.SteamClient.Shutdown();
#elif WIN64
        w64.Steamworks.SteamClient.Shutdown();
#else
        if (Is64Bit)
          w64.Steamworks.SteamClient.Shutdown();
        else
          w32.Steamworks.SteamClient.Shutdown();
#endif
        //SteamAPI._DWSteamAPI.Dispose();
        //SteamAPI._DWSteamAPI = (DWSteamAPI)null;
      }
      catch (Exception ex) {
        SteamAPI.WriteSteamError("Finalizer", ex.ToString(), SteamAPI._CrashLogPath);
      }
    }

    public static void WriteSteamError(string methodCall, string errorMessage, string folder) {
      try {
        if (string.IsNullOrEmpty(folder))
          return;

        if (!Directory.Exists(folder))
          Directory.CreateDirectory(folder);
        if (!Directory.Exists(folder))
          return;

        using (FileStream fileStream = new FileStream(folder + "SteamCrashLog.txt", FileMode.Append, FileAccess.Write)) {
          using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream)) {
            string str = DateTime.Now.ToLongTimeString() + " " + methodCall + ": " + errorMessage;
            streamWriter.Write(str);
          }
        }
      }
      catch (Exception ex) {
      }
    }

    private sealed class Finalizer {

      ~Finalizer() => SteamAPI.Shutdown();

    }

  }

}