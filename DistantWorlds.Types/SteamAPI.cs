﻿// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SteamAPI
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using Steamworks;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DistantWorlds.Types
{
    public class SteamAPI
    {
        //private static DWSteamAPI _DWSteamAPI;
        private static bool _Initialized = false;
        private static string _CrashLogPath;
        private static Hashtable _AchievementStatuses = new Hashtable();
        private static uint _SteamAppId;
        private static readonly SteamAPI.Finalizer _Finalizer = new SteamAPI.Finalizer();

        //static SteamAPI() => SteamAPI._DWSteamAPI = new DWSteamAPI();

        public static void Initialize(string crashLogPath)
        {
            SteamAPI._Initialized = false;
            SteamAPI._CrashLogPath = crashLogPath;
            try
            {
                SteamClient.Init(261470);
                //SteamAPI._SteamAppId = SteamAPI._DWSteamAPI.GetAppID();
                //SteamClient.GetPlayerSteamLevel();

                if (!SteamClient.IsLoggedOn)
                    return;
                SteamUserStats.RequestCurrentStats();
                //SteamAPI._DWSteamAPI.RequestCurrentStats();
                SteamAPI._Initialized = true;
            }
            catch (Exception ex)
            {
                SteamAPI.WriteSteamError(nameof(Initialize), ex.ToString(), SteamAPI._CrashLogPath);
            }
        }

        public static void SetAchievementIfNecessary(Achievement achievement)
        {
            if (!SteamAPI._Initialized || achievement == null)
                return;
            string achievementName = SteamAPI.ResolveAchievementName(achievement);
            if (string.IsNullOrEmpty(achievementName))
            { return; }
            try
            {
                var ach = SteamUserStats.Achievements.FirstOrDefault(x => x.Name == achievementName);
                if (!string.IsNullOrEmpty(ach.Name))
                {
                    ach.Trigger(true);
                }
            }
            catch (Exception ex)
            {
                SteamAPI.WriteSteamError(nameof(SetAchievementIfNecessary), ex.ToString(), SteamAPI._CrashLogPath);
            }
        }

        public static string ResolveAchievementName(Achievement achievement)
        {
            string str = string.Empty;
            if (achievement != null && achievement.Type != AchievementType.AchieveAllRaceVictoryConditions)
                str = Galaxy.ResolveDescription(achievement);
            return str;
        }

        public static void ResetAllStatsAndAchievements()
        {
            if (!SteamAPI._Initialized)
                return;
            try
            {
                SteamUserStats.ResetAll(true);
            }
            catch (Exception ex)
            {
                SteamAPI.WriteSteamError(nameof(ResetAllStatsAndAchievements), ex.ToString(), SteamAPI._CrashLogPath);
            }
        }

        public static void Shutdown()
        {
            try
            {
                SteamClient.Shutdown();
                //SteamAPI._DWSteamAPI.Dispose();
                //SteamAPI._DWSteamAPI = (DWSteamAPI)null;
            }
            catch (Exception ex)
            {
                SteamAPI.WriteSteamError("Finalizer", ex.ToString(), SteamAPI._CrashLogPath);
            }
        }

        public static void WriteSteamError(string methodCall, string errorMessage, string folder)
        {
            try
            {
                if (string.IsNullOrEmpty(folder))
                    return;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                if (!Directory.Exists(folder))
                    return;
                using (FileStream fileStream = new FileStream(folder + "SteamCrashLog.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    {
                        string str = DateTime.Now.ToLongTimeString() + " " + methodCall + ": " + errorMessage;
                        streamWriter.Write(str);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private sealed class Finalizer
        {
            ~Finalizer() => SteamAPI.Shutdown();
        }
    }
}
