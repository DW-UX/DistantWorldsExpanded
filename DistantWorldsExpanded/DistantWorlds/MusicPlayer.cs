// Decompiled with JetBrains decompiler
// Type: DistantWorlds.MusicPlayer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.IO;
using System.Timers;
using Microsoft.Xna.Framework.Media;

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

        public SetVolumeDelegate VolumeDelegate;

        public StopDelegate StopMethodDelegate;

        public PauseDelegate PauseMethodDelegate;

        public ResumeDelegate ResumeMethodDelegate;

        public PlayMusicDelegate PlayMusicMethodDelegate;

        public PlayMusicFileDelegate PlayMusicFileMethodDelegate;

        public SetFadeVolumeDelegate FadeVolumeDelegate;

        public StartThemeDelegate StartThemeMethodDelegate;

        private Main _mainForm;

        private string _themeMusicFile;

        private string _themeMusic;

        private string _folder;

        private string[] _songFiles;

        private Timer _timer;

        private double _fadeMode;

        private readonly double _fadeRatio;

        private int _musicFadeFinishWaitCounter;

        private MusicFadeFinishAction _musicFadeFinishAction;

        private double _Volume;

        private double _FadeVolume;

        private bool _IsInitiatingFade;

        private Random _random;

        public bool IsPlaying
        {
            get
            {
                if (MediaPlayer.PlayPosition.TotalSeconds > 0.0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsInitiatingFade => _IsInitiatingFade;

        public double Volume => _Volume;
        public double ActualVolume => MediaPlayer.Volume;

        private static readonly object _songLock = new object();
        private static Song SongFactory(string songName, Uri uri) { lock (_songLock) { return Song.FromUri(songName, uri); } }

        ~MusicPlayer()
        {
            _timer.Stop();
        }

        public MusicPlayer(Main mainForm, string folder, string themeMusic):base()
        {
            
            _fadeMode = -1.0;
            _fadeRatio = 0.02;
            _mainForm = mainForm;
            _themeMusic = themeMusic;
            _folder = folder;
            _songFiles = Directory.GetFiles(folder, "*.mp3");
            if (_songFiles != null && _songFiles.Length != 0)
            {
                if (!File.Exists(_folder + _themeMusic))
                {
                    throw new ApplicationException("Music folder does not contain Theme music: " + _themeMusic);
                }
                _random = new Random((int)DateTime.Now.Ticks);
                _timer = new Timer();
                _timer.Interval = 50.0;
                _timer.Elapsed += _timer_Elapsed;
                _timer.Stop();
                StartThemeMethodDelegate = StartThemeInternal;
                VolumeDelegate = SetVolume;
                StopMethodDelegate = Stop;
                PauseMethodDelegate = Pause;
                ResumeMethodDelegate = ResumeMusic;
                PlayMusicMethodDelegate = PlayMusic;
                PlayMusicFileMethodDelegate = PlayMusicFile;
                FadeVolumeDelegate = SetFadeVolume;
                return;
            }
            throw new ApplicationException("Music folder does not contain any MP3 files");
        }

        public void Start()
        {
            MediaPlayer.MediaStateChanged += mediaPlayer_MediaEnded;
            _themeMusicFile = PickNewSong();
            PlayMusic();
        }

        public void Stop()
        {
            MediaPlayer.MediaStateChanged -= mediaPlayer_MediaEnded;
            MediaPlayer.Stop();
        }

        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void StartTheme()
        {
            _mainForm.Invoke(StartThemeMethodDelegate);
        }

        public void StartThemeInternal()
        {
            MediaPlayer.MediaStateChanged += mediaPlayer_MediaEnded;
            _themeMusicFile = _folder + _themeMusic;
            PlayMusic();
        }

        public void SetFadeVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                MediaPlayer.Volume = (float)(volume * 0.6);
            }
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                _Volume = volume;
                MediaPlayer.Volume = (float)(_Volume * 0.6);
            }
        }

        public void SetVolume(SoundVolume volume)
        {
            switch (volume)
            {
                case SoundVolume.Mute:
                    _Volume = 0.0;
                    break;
                case SoundVolume.Faint:
                    _Volume = 0.1;
                    break;
                case SoundVolume.Soft:
                    _Volume = 0.3;
                    break;
                case SoundVolume.Normal:
                    _Volume = 0.5;
                    break;
                case SoundVolume.Loud:
                    _Volume = 0.75;
                    break;
                case SoundVolume.Maximum:
                    _Volume = 1.0;
                    break;
            }
            _mainForm.Invoke(VolumeDelegate, _Volume);
        }

        private void mediaPlayer_MediaEnded(object sender, EventArgs e) {
            Console.WriteLine($"state: {MediaPlayer.State}");

            if (MediaPlayer.State != MediaState.Stopped)
                return;

            _themeMusicFile = PickNewSong();
            PlayMusic();
        }

        public void ResumeMusic()
        {
            _IsInitiatingFade = false;
            MediaPlayer.Resume();
        }

        private void PlayMusicFile(string file)
        {
            _IsInitiatingFade = false;
            var songName = "<unknown>";
            try {
                songName = Path.GetFileNameWithoutExtension(file);
            }
            catch {
                // oh well
            }
            //MediaPlayer.Play(Song.FromUri(songName, new Uri(string_4)));
            MediaPlayer.Play(SongFactory(songName, new Uri(file)));
            
            SetVolume(_Volume);
            //MediaPlayer.Play();
        }

        private void PlayMusic()
        {
            _IsInitiatingFade = false;
            string themeFile = _themeMusicFile;
            PlayMusicFile(themeFile);
        }

        public void FadeResume()
        {
            _IsInitiatingFade = false;
            _FadeVolume = 0.0;
            _fadeMode = 1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Resume;
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.Resume();
            _timer.Start();
        }

        public void FadePause()
        {
            _IsInitiatingFade = true;
            _FadeVolume = _Volume;
            _fadeMode = -1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Pause;
            _timer.Start();
        }

        public void FadeStop()
        {
            _IsInitiatingFade = true;
            _FadeVolume = _Volume;
            _fadeMode = -1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Stop;
            _timer.Start();
        }

        public void ForceSwitch()
        {
            _FadeVolume = _Volume;
            _fadeMode = -1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.StartNewMusic;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            double volume_curr = _FadeVolume;
            double fade_delta = Math.Sqrt(_FadeVolume + 0.1) * _fadeRatio;
            fade_delta = Math.Max(0.005, fade_delta);
            volume_curr += _fadeMode * fade_delta;
            bool fade_complete = false;
            double volumn_target = 0.0;
            if (_fadeMode > 0.0)
            {
                volumn_target = _Volume;
                if (volume_curr >= volumn_target)
                {
                    fade_complete = true;
                    volume_curr = volumn_target;
                }
            }
            else if (volume_curr <= volumn_target)
            {
                fade_complete = true;
                volume_curr = volumn_target;
            }
            if (fade_complete)
            {
                _IsInitiatingFade = false;
                _musicFadeFinishWaitCounter++;
                if (_musicFadeFinishWaitCounter > 20)
                {
                    _musicFadeFinishWaitCounter = 0;
                    switch (_musicFadeFinishAction)
                    {
                        case MusicFadeFinishAction.StartNewMusic:
                            _themeMusicFile = PickNewSong();
                            _mainForm.Invoke(PlayMusicMethodDelegate);
                            _mainForm.Invoke(VolumeDelegate, _Volume);
                            break;
                        case MusicFadeFinishAction.Stop:
                            _mainForm.Invoke(StopMethodDelegate);
                            break;
                        case MusicFadeFinishAction.Pause:
                            _mainForm.Invoke(PauseMethodDelegate);
                            break;
                    }
                    _musicFadeFinishAction = MusicFadeFinishAction.StartNewMusic;
                    _timer.Stop();
                    return;
                }
            }
            _FadeVolume = volume_curr;
            _mainForm.BeginInvoke(FadeVolumeDelegate, _FadeVolume);
        }

        private string PickNewSong()
        {
            string text = _themeMusicFile;
            while (string.IsNullOrEmpty(text) || (_songFiles.Length > 1 && text == _themeMusicFile))
            {
                int num = _random.Next(0, _songFiles.Length);
                text = _songFiles[num];
            }
            return text;
        }
    }
}
