// Decompiled with JetBrains decompiler
// Type: DistantWorlds.MusicPlayer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.IO;
using System.Timers;
using System.Windows.Media;

namespace DistantWorlds
{
    public class MusicPlayer
    {
        public delegate void SetVolumeDelegate(double volume);

        public delegate void StopDelegate();

        public delegate void PauseDelegate();

        public delegate void ResumeDelegate();

        public delegate void PlayMusicDelegate();

        public delegate void PlayMusicFileDelegate(string filePath);

        public delegate void SetFadeVolumeDelegate(double volume);

        public delegate void StartThemeDelegate();

        internal MediaPlayer mediaPlayer_0;

        public SetVolumeDelegate VolumeDelegate;

        public StopDelegate StopMethodDelegate;

        public PauseDelegate PauseMethodDelegate;

        public ResumeDelegate ResumeMethodDelegate;

        public PlayMusicDelegate PlayMusicMethodDelegate;

        public PlayMusicFileDelegate PlayMusicFileMethodDelegate;

        public SetFadeVolumeDelegate FadeVolumeDelegate;

        public StartThemeDelegate StartThemeMethodDelegate;

        private Main main_0;

        private string string_0;

        private string string_1;

        private string string_2;

        private string[] string_3;

        private Timer timer_0;

        private double double_0;

        private double double_1;

        private int int_0;

        private MusicFadeFinishAction musicFadeFinishAction_0;

        private double double_2;

        private double double_3;

        private bool bool_0;

        private Random random_0;

        public bool IsPlaying
        {
            get
            {
                if (mediaPlayer_0.Position.TotalSeconds > 0.0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsInitiatingFade => bool_0;

        public double Volume => double_2;

        ~MusicPlayer()
        {
            timer_0.Stop();
        }

        public MusicPlayer(Main mainForm, string folder, string themeMusic):base()
        {
            
            double_0 = -1.0;
            double_1 = 0.02;
            main_0 = mainForm;
            string_1 = themeMusic;
            string_2 = folder;
            string_3 = Directory.GetFiles(folder, "*.mp3");
            if (string_3 != null && string_3.Length != 0)
            {
                if (!File.Exists(string_2 + string_1))
                {
                    throw new ApplicationException("Music folder does not contain Theme music: " + string_1);
                }
                random_0 = new Random((int)DateTime.Now.Ticks);
                timer_0 = new Timer();
                timer_0.Interval = 50.0;
                timer_0.Elapsed += timer_0_Elapsed;
                timer_0.Stop();
                mediaPlayer_0 = new MediaPlayer();
                StartThemeMethodDelegate = StartThemeInternal;
                VolumeDelegate = SetVolume;
                StopMethodDelegate = Stop;
                PauseMethodDelegate = Pause;
                ResumeMethodDelegate = ResumeMusic;
                PlayMusicMethodDelegate = method_1;
                PlayMusicFileMethodDelegate = method_0;
                FadeVolumeDelegate = SetFadeVolume;
                return;
            }
            throw new ApplicationException("Music folder does not contain any MP3 files");
        }

        public void Start()
        {
            mediaPlayer_0.MediaEnded += mediaPlayer_0_MediaEnded;
            string_0 = EbsZqjqvhZ();
            method_1();
        }

        public void Stop()
        {
            mediaPlayer_0.MediaEnded -= mediaPlayer_0_MediaEnded;
            mediaPlayer_0.Stop();
            mediaPlayer_0.Position = new TimeSpan(0L);
            timer_0.Stop();
        }

        public void Pause()
        {
            mediaPlayer_0.Pause();
        }

        public void StartTheme()
        {
            main_0.Invoke(StartThemeMethodDelegate);
        }

        public void StartThemeInternal()
        {
            mediaPlayer_0.MediaEnded += mediaPlayer_0_MediaEnded;
            string_0 = string_2 + string_1;
            method_1();
        }

        public void SetFadeVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                mediaPlayer_0.Volume = volume * 0.6;
            }
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                double_2 = volume;
                mediaPlayer_0.Volume = double_2 * 0.6;
            }
        }

        public void SetVolume(SoundVolume volume)
        {
            switch (volume)
            {
                case SoundVolume.Mute:
                    double_2 = 0.0;
                    break;
                case SoundVolume.Faint:
                    double_2 = 0.1;
                    break;
                case SoundVolume.Soft:
                    double_2 = 0.3;
                    break;
                case SoundVolume.Normal:
                    double_2 = 0.5;
                    break;
                case SoundVolume.Loud:
                    double_2 = 0.75;
                    break;
                case SoundVolume.Maximum:
                    double_2 = 1.0;
                    break;
            }
            main_0.Invoke(VolumeDelegate, double_2);
        }

        private void mediaPlayer_0_MediaEnded(object sender, EventArgs e)
        {
            string_0 = EbsZqjqvhZ();
            method_1();
        }

        public void ResumeMusic()
        {
            bool_0 = false;
            mediaPlayer_0.Play();
        }

        private void method_0(string string_4)
        {
            bool_0 = false;
            mediaPlayer_0.Open(new Uri(string_4));
            SetVolume(double_2);
            mediaPlayer_0.Play();
        }

        private void method_1()
        {
            bool_0 = false;
            string string_ = string_0;
            method_0(string_);
        }

        public void FadeResume()
        {
            bool_0 = false;
            double_3 = 0.0;
            double_0 = 1.0;
            musicFadeFinishAction_0 = MusicFadeFinishAction.Resume;
            mediaPlayer_0.Volume = double_3;
            mediaPlayer_0.Play();
            timer_0.Start();
        }

        public void FadePause()
        {
            bool_0 = true;
            double_3 = double_2;
            double_0 = -1.0;
            musicFadeFinishAction_0 = MusicFadeFinishAction.Pause;
            timer_0.Start();
        }

        public void FadeStop()
        {
            bool_0 = true;
            double_3 = double_2;
            double_0 = -1.0;
            musicFadeFinishAction_0 = MusicFadeFinishAction.Stop;
            timer_0.Start();
        }

        public void ForceSwitch()
        {
            double_3 = double_2;
            double_0 = -1.0;
            musicFadeFinishAction_0 = MusicFadeFinishAction.StartNewMusic;
            timer_0.Start();
        }

        private void timer_0_Elapsed(object sender, ElapsedEventArgs e)
        {
            double num = double_3;
            double val = Math.Sqrt(double_3 + 0.1) * double_1;
            val = Math.Max(0.005, val);
            num += double_0 * val;
            bool flag = false;
            double num2 = 0.0;
            if (double_0 > 0.0)
            {
                num2 = double_2;
                if (num >= num2)
                {
                    flag = true;
                    num = num2;
                }
            }
            else if (num <= num2)
            {
                flag = true;
                num = num2;
            }
            if (flag)
            {
                bool_0 = false;
                int_0++;
                if (int_0 > 20)
                {
                    int_0 = 0;
                    switch (musicFadeFinishAction_0)
                    {
                        case MusicFadeFinishAction.StartNewMusic:
                            string_0 = EbsZqjqvhZ();
                            main_0.Invoke(PlayMusicMethodDelegate);
                            main_0.Invoke(VolumeDelegate, double_2);
                            break;
                        case MusicFadeFinishAction.Stop:
                            main_0.Invoke(StopMethodDelegate);
                            break;
                        case MusicFadeFinishAction.Pause:
                            main_0.Invoke(PauseMethodDelegate);
                            break;
                    }
                    musicFadeFinishAction_0 = MusicFadeFinishAction.StartNewMusic;
                    timer_0.Stop();
                    return;
                }
            }
            double_3 = num;
            main_0.BeginInvoke(FadeVolumeDelegate, double_3);
        }

        private string EbsZqjqvhZ()
        {
            string text = string_0;
            while (string.IsNullOrEmpty(text) || (string_3.Length > 1 && text == string_0))
            {
                int num = random_0.Next(0, string_3.Length);
                text = string_3[num];
            }
            return text;
        }
    }
}
