// Decompiled with JetBrains decompiler
// Type: DistantWorlds.MusicPlayer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.IO;
using System.Timers;
// using Microsoft.Xna.Framework.Media;
using NAudio.Wave;
using System.Windows.Forms;

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

        private string _currentMusicFile;

        private string _themeMusic;

        private string _folder;

        private string[] _songFiles;

        private System.Timers.Timer _timer;

        private double _fadeMode;

        private readonly double _fadeRatio;

        private int _musicFadeFinishWaitCounter;

        private MusicFadeFinishAction _musicFadeFinishAction;

        private double _Volume;

        private double _FadeVolume;

        private bool _IsInitiatingFade;

        private bool _IsPlaying;

        private Random _random;

        // naudio
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        public bool IsPlaying
        {
            get
            {
                return _IsPlaying;
            }
        }

        public bool IsInitiatingFade => _IsInitiatingFade;

        public double Volume => _Volume;
        public double ActualVolume { get { if (audioFile != null) {return audioFile.Volume; } else { return 0.0; } } }

        ~MusicPlayer()
        {
            _timer.Stop();
            if (outputDevice != null)
            {
                outputDevice.Dispose();
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
            }
        }

        private bool _dummy = false;

        public MusicPlayer(Main mainForm, string folder, string themeMusic, bool dummy=false):base()
        {
            outputDevice = new WaveOutEvent();
            outputDevice.DesiredLatency = 100;
            _dummy = dummy;

            _IsPlaying = false;
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
                _timer = new System.Timers.Timer();
                _timer.Interval = 50.0;
                _timer.Elapsed += _timer_Elapsed;
                _timer.Stop();
                StartThemeMethodDelegate = StartThemeInternal;
                VolumeDelegate = SetVolume;
                StopMethodDelegate = Stop;
                PauseMethodDelegate = Pause;
                ResumeMethodDelegate = Resume;
                PlayMusicMethodDelegate = PlayMusic;
                PlayMusicFileMethodDelegate = PlayMusicFile;
                FadeVolumeDelegate = SetFadeVolume;
                return;
            }
            throw new ApplicationException("Music folder does not contain any MP3 files");
        }

        public void Start()
        {
            // outputDevice.PlaybackStopped += mediaPlayer_MediaEnded;
            _currentMusicFile = PickNewSong();
            PlayMusic();
        }

        public void Stop()
        {
            outputDevice.PlaybackStopped -= mediaPlayer_MediaEnded;
            outputDevice.Stop();
            outputDevice.Dispose();
            if (audioFile != null)
            { 
                audioFile.Dispose();
            }
            _IsPlaying = false;
        }

        public void Pause()
        {
            outputDevice.Pause();
        }

        public void StartTheme()
        {
            _mainForm.Invoke(StartThemeMethodDelegate);
        }

        public void StartThemeInternal()
        {
            // outputDevice.PlaybackStopped += mediaPlayer_MediaEnded;
            _currentMusicFile = _folder + _themeMusic;
            PlayMusic();
        }

        public void SetFadeVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                if (audioFile != null)
                { 
                    audioFile.Volume = (float)(volume * 0.6);
                }
            }
        }

        public void SetVolume(double volume)
        {
            if (volume >= 0.0 && volume <= 1.0)
            {
                _Volume = volume;
                if (audioFile != null)
                { 
                    audioFile.Volume = (float)(_Volume * 0.6);
                }
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
            if (outputDevice.PlaybackState == PlaybackState.Stopped) {
                 _currentMusicFile = PickNewSong();
                PlayMusic();
            } 
            /* else
            {
                Console.WriteLine("PlaybackState: " + outputDevice.PlaybackState);
            } */
        }

        public void Resume()
        {
            _IsInitiatingFade = false;
            if (_dummy) return;
            outputDevice.Play();
        }

        private void PlayMusicFile(string file)
        {
            _IsInitiatingFade = false;
            var songName = "<unknown>";
            try {
                songName = Path.GetFileNameWithoutExtension(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing music: {songName}\r\n{ex.ToString()}");
            }
            if (outputDevice != null)
            {
                outputDevice.Dispose();
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
            }

            outputDevice = new WaveOutEvent();
            outputDevice.DesiredLatency = 100;
            audioFile = new AudioFileReader(file);
            outputDevice.Init(audioFile);
            _IsPlaying = true;
            
            SetVolume(_Volume);
            if (_dummy) return;

            outputDevice.PlaybackStopped += mediaPlayer_MediaEnded;
            outputDevice.Play();
        }

        private void PlayMusic()
        {
            _IsInitiatingFade = false;
            string currentFile = _currentMusicFile;
            PlayMusicFile(currentFile);
        }

        public void FadeResume()
        {
            _IsInitiatingFade = false;
            _FadeVolume = 0.0;
            _fadeMode = 1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Resume;
            if (audioFile != null)
            { 
                audioFile.Volume = 0.0f;
            }
            if (_dummy) return;
            outputDevice.Play();
            _timer.Start();
        }

        public void FadePause()
        {
            _IsInitiatingFade = true;
            _FadeVolume = _Volume;
            _fadeMode = -1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Pause;
            _timer.Start();
            // outputDevice.Pause(); // debug
        }

        public void FadeStop()
        {
            _IsInitiatingFade = true;
            _FadeVolume = _Volume;
            _fadeMode = -1.0;
            _musicFadeFinishAction = MusicFadeFinishAction.Stop;
            _timer.Start();
            // outputDevice.Stop(); // debug
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
                            _currentMusicFile = PickNewSong();
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
            string text = _currentMusicFile;
            while (string.IsNullOrEmpty(text) || (_songFiles.Length > 1 && text == _currentMusicFile))
            {
                int num = _random.Next(0, _songFiles.Length);
                text = _songFiles[num];
            }
            return text;
        }
    }
}
