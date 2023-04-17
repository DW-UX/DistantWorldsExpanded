using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public class SteamWrapperSelector
    {
        private static bool _is64Bit = false;
        static SteamWrapperSelector()
        {
            _is64Bit = Environment.Is64BitProcess;
        }

        public static void Initialize(string crashLogPath)
        {
            if (_is64Bit)
            {
                SteamAPIWrapper.Initialize(crashLogPath);
            }
            else
            { SteamAPI.Initialize(crashLogPath); }
        }

        public static void SetAchievementIfNecessary(Achievement achievement)
        {
            if (_is64Bit)
            {
                SteamAPIWrapper.SetAchievementIfNecessary(achievement);
            }
            else
            { SteamAPI.SetAchievementIfNecessary(achievement); }
        }

        public static void ResetAllStatsAndAchievements()
        {
            if (_is64Bit)
            {
                SteamAPIWrapper.ResetAllStatsAndAchievements();
            }
            else
            { SteamAPI.ResetAllStatsAndAchievements(); }
        }

        public static void Shutdown()
        {
            if (_is64Bit)
            {
                SteamAPIWrapper.Shutdown();
            }
            else
            { SteamAPI.Shutdown(); }
        }

        private sealed class Finalizer
        {
            ~Finalizer()
            {
                if (Environment.Is64BitProcess)
                {
                    SteamAPIWrapper.Shutdown();
                }
                else
                { SteamAPI.Shutdown(); }
            }
        }
    }
}

