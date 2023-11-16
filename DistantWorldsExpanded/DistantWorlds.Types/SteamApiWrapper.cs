using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public class SteamAPIWrapper
    {
        private const string _PipeName = "DWSteamWrapperCommunication";

        private static bool _Initialized = false;
        private static NamedPipeServerStream _pipeServer = new NamedPipeServerStream(_PipeName, PipeDirection.Out);
        //private static StreamWriter _sW = new (_pipServer);
        private static StreamString _streamManager;

        static SteamAPIWrapper()
        {
            _streamManager = new StreamString(_pipeServer);
            _pipeServer.WaitForConnection();
            //_sW.AutoFlush = true;
        }

        public static void Initialize(string crashLogPath)
        {
            var command= JObject.FromObject(new {Name = nameof(Initialize), Data = crashLogPath });
            _streamManager.WriteString(command.ToString());
            //_sW.WriteLine(command);
            _Initialized = true;
        }

        public static void SetAchievementIfNecessary(Achievement achievement)
        {
            if (!_Initialized || achievement == null)
                return;
            var command = JObject.FromObject(new { Name = nameof(SetAchievementIfNecessary), Data = JsonConvert.SerializeObject(achievement, Formatting.None) });
            _streamManager.WriteString(command.ToString());
            //_sW.WriteLine(command);
        }

        //private static string ResolveAchievementName(Achievement achievement)
        //{
        //    string str = string.Empty;
        //    if (achievement != null && achievement.Type != AchievementType.AchieveAllRaceVictoryConditions)
        //        str = Galaxy.ResolveDescription(achievement);
        //    return str;
        //}

        //private static unsafe void SetAchievementIfNecessary(string achievementName)
        //{
        //    try
        //    {
        //        object achievementStatuse = SteamAPIWrapper._AchievementStatuses[(object)achievementName];
        //        if (achievementStatuse is bool flag)
        //            return;
        //        IntPtr hglobal1 = IntPtr.Zero;
        //        try
        //        {
        //            hglobal1 = Marshal.StringToHGlobalAnsi(achievementName);
        //            SteamAPIWrapper._DWSteamAPI.GetAchievement((sbyte*)(void*)hglobal1, &flag);
        //        }
        //        finally
        //        {
        //            if (hglobal1 != IntPtr.Zero)
        //                Marshal.FreeHGlobal(hglobal1);
        //        }
        //        SteamAPIWrapper._AchievementStatuses[(object)achievementName] = (object)flag;
        //        if (flag)
        //            return;
        //        IntPtr hglobal2 = IntPtr.Zero;
        //        try
        //        {
        //            hglobal2 = Marshal.StringToHGlobalAnsi(achievementName);
        //            SteamAPIWrapper._DWSteamAPI.SetAchievement((sbyte*)(void*)hglobal2);
        //        }
        //        finally
        //        {
        //            if (hglobal2 != IntPtr.Zero)
        //                Marshal.FreeHGlobal(hglobal2);
        //        }
        //        SteamAPIWrapper._DWSteamAPI.StoreStats();
        //        SteamAPIWrapper._AchievementStatuses[(object)achievementName] = (object)true;
        //    }
        //    catch (Exception ex)
        //    {
        //        SteamAPIWrapper.WriteSteamError(nameof(SetAchievementIfNecessary), ex.ToString(), SteamAPIWrapper._CrashLogPath);
        //    }
        //}

        public static void ResetAllStatsAndAchievements()
        {
            if (!_Initialized)
                return;
            var command = JObject.FromObject(new { Name = nameof(ResetAllStatsAndAchievements), Data = "" });
            _streamManager.WriteString(command.ToString());
            //_sW.WriteLine(command);
        }

        public static void Shutdown()
        {
            var command = JObject.FromObject(new { Name = nameof(Shutdown), Data = "" });
            _streamManager.WriteString(command.ToString());
            //_sW.WriteLine(command);
        }

        private class StreamString
        {
            private Stream ioStream;
            private UnicodeEncoding streamEncoding;

            public StreamString(Stream ioStream)
            {
                this.ioStream = ioStream;
                streamEncoding = new UnicodeEncoding();
            }

            public string ReadString()
            {
                int len = 0;

                len = ioStream.ReadByte() * 256;
                len += ioStream.ReadByte();
                byte[] inBuffer = new byte[len];
                ioStream.Read(inBuffer, 0, len);

                return streamEncoding.GetString(inBuffer);
            }

            public int WriteString(string outString)
            {
                byte[] outBuffer = streamEncoding.GetBytes(outString);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = (int)UInt16.MaxValue;
                }
                ioStream.WriteByte((byte)(len / 256));
                ioStream.WriteByte((byte)(len & 255));
                ioStream.Write(outBuffer, 0, len);
                ioStream.Flush();

                return outBuffer.Length + 2;
            }
        }
    }
}
