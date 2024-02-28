using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using DistantWorlds.Types;
using Ionic.Zip;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Multimedia;
//using Microsoft.DirectX.DirectSound;

namespace DistantWorlds
{
    // Token: 0x02000049 RID: 73
    public class EffectsPlayer
    {

        // Token: 0x04000A08 RID: 2568
        private object @lock = new();

        // Token: 0x04000A09 RID: 2569
        private Main main_0;

        // Token: 0x04000A0A RID: 2570
        //private Device device_0;

        // Token: 0x04000A0B RID: 2571
        private List<SoundEffectInstance> list_0 = new();

        // Token: 0x04000A0C RID: 2572
        private Dictionary<string, SoundEffect> sfxBank = new();

        // Token: 0x04000A0D RID: 2573
        private double double_0 = 0.7;

        // Token: 0x04000A0E RID: 2574
        private string string_0;

        // Token: 0x04000A0F RID: 2575
        private string string_1;

        // Token: 0x04000A10 RID: 2576
        private Random random_0;

        // Token: 0x17000030 RID: 48
        // (get) Token: 0x0600096A RID: 2410 RVA: 0x001351A8 File Offset: 0x001351A8
        // (set) Token: 0x0600096B RID: 2411 RVA: 0x001351B0 File Offset: 0x001351B0
        public double Volume
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        // Token: 0x0600096C RID: 2412 RVA: 0x001351BC File Offset: 0x001351BC
        public EffectsPlayer(Main parent, string applicationStartupPath, string customizationSetName)
        {
            this.main_0 = parent;
            this.list_0 = new();
            this.Initialize(applicationStartupPath, customizationSetName);
            this.random_0 = new Random((int)DateTime.Now.Ticks);
        }

        // Token: 0x0600096D RID: 2413 RVA: 0x0013526C File Offset: 0x0013526C
        public void Initialize(string applicationStartupPath, string customizationSetName)
        {
            this.Clear();
            this.string_0 = applicationStartupPath;
            this.string_1 = customizationSetName;
            this.method_0(applicationStartupPath, customizationSetName);
        }

        // Token: 0x0600096E RID: 2414 RVA: 0x0013528C File Offset: 0x0013528C
        public void Clear()
        {
            lock (this.@lock)
            {
                foreach (var entry in this.sfxBank)
                {
                    entry.Value.Dispose();
                }
                sfxBank.Clear();
            }
        }

        // Token: 0x0600096F RID: 2415 RVA: 0x00135370 File Offset: 0x00135370
        private void method_0(string string_2, string string_3)
        {
            lock (this.@lock)
            {
                string text = string_2 + "\\sounds\\effects\\";
                string text2 = string.Empty;
                if (!string.IsNullOrEmpty(string_3))
                {
                    text2 = string_2 + "\\Customization\\" + string_3 + "\\sounds\\effects\\";
                }
                List<string> list = ComponentDefinitionList.ResolveWeaponSoundEffectFilenames(Galaxy.ComponentDefinitionsStatic);
                list.Add("Explosion.wav");
                list.Add("Explosion2.wav");
                list.Add("Explosion3.wav");
                list.Add("Explosion_Small.wav");
                list.Add("Explosion_Small2.wav");
                list.Add("Explosion_Small3.wav");
                List<Stream> list2 = new List<Stream>();
                List<string> list3 = new List<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    string text3 = text + list[i];
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text3 = text2 + list[i];
                        if (!File.Exists(text3))
                        {
                            text3 = text + list[i];
                        }
                    }
                    Stream stream = this.method_1(text3);
                    if (stream != null)
                    {
                        list2.Add(stream);
                        list3.Add(list[i]);
                    }
                }
                for (int j = 0; j < list2.Count; j++)
                {
                    if (!this.sfxBank.ContainsKey(list3[j]))
                    {
                        var effect = this.LoadSoundEffect(list2[j]);
                        this.sfxBank.Add(list3[j], effect);
                    }
                }
                for (int k = 0; k < list2.Count; k++)
                {
                    list2[k].Close();
                    list2[k].Dispose();
                    list2[k] = null;
                }
                list2.Clear();
            }
        }

        // Token: 0x06000970 RID: 2416 RVA: 0x00135564 File Offset: 0x00135564
        private FileStream method_1(string string_2)
        {
            FileStream res = null;
            try
            {
                if (File.Exists(string_2))
                {
                    res = new FileStream(string_2, FileMode.Open, FileAccess.Read);
                }
            }
            catch (Exception)
            {

            }
            if (res != null)
            {
                return res;
            }
            string text = string.Format(TextResolver.GetText("Could not load required sound"), string_2);
            MessageBox.Show(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(-1);
            return null;
        }

        // Token: 0x06000971 RID: 2417 RVA: 0x00135610 File Offset: 0x00135610
        private SoundEffect LoadSoundEffect(Stream stream_0)
        {
            return SoundEffect.FromStream(stream_0);
        }

        // Token: 0x06000973 RID: 2419 RVA: 0x001356C8 File Offset: 0x001356C8
        public void ClearFinishedBuffers()
        {
            lock (this.@lock)
            {
                List<SoundEffectInstance> list = new();
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    var inst = this.list_0[i];
                    try
                    {
                        if (inst != null && !inst.IsDisposed)
                        {
                            var status = inst.State;
                            if (status == SoundState.Stopped)
                            {
                                inst.Dispose();
                                list.Add(inst);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        list.Add(inst);
                    }
                }
                foreach (var secondaryBuffer2 in list)
                {
                    this.list_0.Remove(secondaryBuffer2);
                }
            }
        }

        // Token: 0x06000974 RID: 2420 RVA: 0x001357CC File Offset: 0x001357CC
        private SoundEffectInstance CreateSoundEffectInstance(string string_2)
        {
            var inst = sfxBank[string_2].CreateInstance();
            this.list_0.Add(inst);
            return inst;
        }

        // Token: 0x06000975 RID: 2421 RVA: 0x00135824 File Offset: 0x00135824
        public SoundEffectRequest ResolveIonStrike(double balance, double distance)
        {
            string text = "ion_strike.wav";
            double num = 1.8;
            distance = Math.Min(1.0, distance);
            return new SoundEffectRequest
            {
                Filename = text,
                Balance = balance,
                Volume = this.double_0 * num * distance
            };
        }

        // Token: 0x06000976 RID: 2422 RVA: 0x00135878 File Offset: 0x00135878
        public SoundEffectRequest ResolveWeapon(Component component, double balance, double distance)
        {
            string text = string.Empty;
            double num = 0.23;
            if (component != null)
            {
                text = component.SoundEffectFilename;
            }
            distance = Math.Min(1.0, distance);
            return new SoundEffectRequest
            {
                Filename = text,
                Balance = balance,
                Volume = this.double_0 * num * distance
            };
        }

        // Token: 0x06000977 RID: 2423 RVA: 0x001358D8 File Offset: 0x001358D8
        public SoundEffectRequest ResolveFighterWeapon(string effectFilename, ComponentType type, double balance, double distance)
        {
            string text = string.Empty;
            double num = 0.19;
            switch (type)
            {
                case (ComponentType)1:
                    text = effectFilename;
                    break;
                case (ComponentType)2:
                    text = effectFilename;
                    num = 0.25;
                    break;
                case (ComponentType)4:
                    text = effectFilename;
                    num = 0.25;
                    break;
            }
            distance = Math.Min(1.0, distance);
            return new SoundEffectRequest
            {
                Filename = text,
                Balance = balance,
                Volume = this.double_0 * num * distance
            };
        }

        // Token: 0x06000978 RID: 2424 RVA: 0x00135968 File Offset: 0x00135968
        public SoundEffectRequest ResolveAmbientEffect(int soundScheme, double balance, double distance, out int nextEffectOffset)
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            string text = string.Empty;
            double num = this.double_0 * 0.7;
            nextEffectOffset = 4000;
            switch (soundScheme)
            {
                case 0:
                    switch (this.random_0.Next(0, 3))
                    {
                        case 0:
                            text = "ambient1_voice1.wav";
                            nextEffectOffset = 5500;
                            break;
                        case 1:
                            text = "ambient1_voice2.wav";
                            nextEffectOffset = 10800;
                            break;
                        case 2:
                            text = "ambient1_voice3.wav";
                            nextEffectOffset = 6600;
                            break;
                    }
                    break;
                case 1:
                    switch (this.random_0.Next(0, 4))
                    {
                        case 0:
                            text = "ambient2_voice1.wav";
                            nextEffectOffset = 9200;
                            break;
                        case 1:
                            text = "ambient2_voice2.wav";
                            nextEffectOffset = 9200;
                            break;
                        case 2:
                            text = "ambient2_voice3.wav";
                            nextEffectOffset = 8200;
                            break;
                        case 3:
                            text = "ambient2_voice4.wav";
                            nextEffectOffset = 9200;
                            break;
                    }
                    break;
                case 2:
                    switch (this.random_0.Next(0, 3))
                    {
                        case 0:
                            text = "ambient3_energy1.wav";
                            nextEffectOffset = 8400;
                            break;
                        case 1:
                            text = "ambient3_energy2.wav";
                            nextEffectOffset = 8400;
                            break;
                        case 2:
                            text = "ambient3_energy3.wav";
                            nextEffectOffset = 11400;
                            break;
                    }
                    break;
                case 3:
                    switch (this.random_0.Next(0, 5))
                    {
                        case 0:
                        case 1:
                            text = "ambient4_boom1.wav";
                            nextEffectOffset = 5500;
                            break;
                        case 2:
                        case 3:
                            text = "ambient4_boom2.wav";
                            nextEffectOffset = 6500;
                            break;
                        case 4:
                            text = "ambient4_boom3.wav";
                            nextEffectOffset = 8500;
                            break;
                    }
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = num * distance;
            soundEffectRequest.Filename = text;
            return soundEffectRequest;
        }

        // Token: 0x06000979 RID: 2425 RVA: 0x00135B74 File Offset: 0x00135B74
        public SoundEffectRequest ResolveAttackClick()
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double num = this.double_0 * 1.0;
            soundEffectRequest.Filename = "attack_click.wav";
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = num;
            return soundEffectRequest;
        }

        // Token: 0x0600097A RID: 2426 RVA: 0x00135BBC File Offset: 0x00135BBC
        public SoundEffectRequest ResolveImportantMessage()
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double num = this.double_0 * 0.7;
            soundEffectRequest.Filename = "message_major.wav";
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = num;
            return soundEffectRequest;
        }

        // Token: 0x0600097B RID: 2427 RVA: 0x00135C04 File Offset: 0x00135C04
        public SoundEffectRequest ResolveMessage(EmpireMessageType messageType)
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            string text = string.Empty;
            double num = this.double_0 * 0.7;
            switch (messageType)
            {
                case (EmpireMessageType)1:
                case (EmpireMessageType)2:
                case (EmpireMessageType)3:
                case (EmpireMessageType)4:
                case (EmpireMessageType)5:
                case (EmpireMessageType)6:
                case (EmpireMessageType)7:
                case (EmpireMessageType)8:
                case (EmpireMessageType)9:
                case (EmpireMessageType)10:
                case (EmpireMessageType)11:
                case (EmpireMessageType)12:
                case (EmpireMessageType)13:
                case (EmpireMessageType)15:
                case (EmpireMessageType)16:
                case (EmpireMessageType)17:
                case (EmpireMessageType)18:
                case (EmpireMessageType)19:
                case (EmpireMessageType)21:
                case (EmpireMessageType)23:
                case (EmpireMessageType)25:
                case (EmpireMessageType)27:
                case (EmpireMessageType)28:
                case (EmpireMessageType)30:
                case (EmpireMessageType)32:
                case (EmpireMessageType)35:
                case (EmpireMessageType)36:
                case (EmpireMessageType)37:
                case (EmpireMessageType)38:
                case (EmpireMessageType)39:
                case (EmpireMessageType)40:
                case (EmpireMessageType)41:
                case (EmpireMessageType)42:
                case (EmpireMessageType)43:
                case (EmpireMessageType)44:
                case (EmpireMessageType)45:
                case (EmpireMessageType)46:
                case (EmpireMessageType)47:
                case (EmpireMessageType)48:
                case (EmpireMessageType)49:
                case (EmpireMessageType)51:
                case (EmpireMessageType)52:
                case (EmpireMessageType)53:
                case (EmpireMessageType)54:
                case (EmpireMessageType)57:
                case (EmpireMessageType)58:
                case (EmpireMessageType)61:
                case (EmpireMessageType)62:
                case (EmpireMessageType)64:
                case (EmpireMessageType)65:
                case (EmpireMessageType)66:
                case (EmpireMessageType)69:
                case (EmpireMessageType)70:
                case (EmpireMessageType)71:
                case (EmpireMessageType)73:
                case (EmpireMessageType)74:
                case (EmpireMessageType)75:
                case (EmpireMessageType)76:
                case (EmpireMessageType)77:
                case (EmpireMessageType)80:
                case (EmpireMessageType)81:
                case (EmpireMessageType)83:
                case (EmpireMessageType)84:
                case (EmpireMessageType)85:
                case (EmpireMessageType)86:
                case (EmpireMessageType)87:
                case (EmpireMessageType)88:
                case (EmpireMessageType)89:
                case (EmpireMessageType)90:
                case (EmpireMessageType)93:
                case (EmpireMessageType)94:
                case (EmpireMessageType)95:
                case (EmpireMessageType)97:
                    text = "message_standard.wav";
                    break;
                case (EmpireMessageType)14:
                case (EmpireMessageType)55:
                case (EmpireMessageType)56:
                    text = "message_minor.wav";
                    break;
                case (EmpireMessageType)20:
                case (EmpireMessageType)22:
                    text = "message_alarm.wav";
                    num = this.double_0 * 0.4;
                    break;
                case (EmpireMessageType)24:
                case (EmpireMessageType)26:
                case (EmpireMessageType)29:
                case (EmpireMessageType)31:
                case (EmpireMessageType)33:
                case (EmpireMessageType)34:
                case (EmpireMessageType)50:
                case (EmpireMessageType)59:
                case (EmpireMessageType)60:
                case (EmpireMessageType)63:
                case (EmpireMessageType)67:
                case (EmpireMessageType)68:
                case (EmpireMessageType)72:
                case (EmpireMessageType)78:
                case (EmpireMessageType)79:
                case (EmpireMessageType)82:
                case (EmpireMessageType)91:
                case (EmpireMessageType)92:
                case (EmpireMessageType)96:
                    text = "message_major.wav";
                    break;
            }
            soundEffectRequest.Filename = text;
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = num;
            return soundEffectRequest;
        }

        // Token: 0x0600097C RID: 2428 RVA: 0x00135E0C File Offset: 0x00135E0C
        public SoundEffectRequest ResolveHyperjumpEntry(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            return new SoundEffectRequest
            {
                Filename = "Hyperjump_Enter.wav",
                Balance = balance,
                Volume = this.double_0 * 0.45 * distance
            };
        }

        // Token: 0x0600097D RID: 2429 RVA: 0x00135E5C File Offset: 0x00135E5C
        public SoundEffectRequest ResolveHyperjumpExit(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            return new SoundEffectRequest
            {
                Filename = "Hyperjump_Exit.wav",
                Balance = balance,
                Volume = this.double_0 * 0.5 * distance
            };
        }

        // Token: 0x0600097E RID: 2430 RVA: 0x00135EAC File Offset: 0x00135EAC
        public SoundEffectRequest ResolveStar(HabitatType starType, double balance, double distance)
        {
            int num = this.random_0.Next(0, 2);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double num2 = 1.0;
            string text = string.Empty;
            int num3 = 0;
            switch (starType)
            {
                case (HabitatType)1:
                    text = "star_basic1.wav";
                    if (num == 1)
                    {
                        text = "star_basic2.wav";
                    }
                    num3 = 23;
                    num2 = 0.7;
                    break;
                case (HabitatType)2:
                case (HabitatType)3:
                    text = "star_bass1.wav";
                    if (num == 1)
                    {
                        text = "star_bass2.wav";
                    }
                    num3 = 25;
                    num2 = 0.7;
                    break;
                case (HabitatType)4:
                    text = "star_hollow1.wav";
                    if (num == 1)
                    {
                        text = "star_hollow2.wav";
                    }
                    num3 = 27;
                    num2 = 0.5;
                    break;
                case (HabitatType)5:
                    text = "star_ring1.wav";
                    if (num == 1)
                    {
                        text = "star_ring2.wav";
                    }
                    num3 = 29;
                    num2 = 1.2;
                    break;
                case (HabitatType)6:
                    text = "star_intense1.wav";
                    if (num == 1)
                    {
                        text = "star_intense2.wav";
                    }
                    num3 = 31;
                    num2 = 1.0;
                    break;
            }
            if (num3 == 0)
            {
                return null;
            }
            num3 += num;
            soundEffectRequest.Filename = text;
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = this.double_0 * 1.2 * distance * num2;
            return soundEffectRequest;
        }

        // Token: 0x0600097F RID: 2431 RVA: 0x00135FF0 File Offset: 0x00135FF0
        public SoundEffectRequest ResolveMining(double balance, double distance)
        {
            int num = this.random_0.Next(0, 4);
            double num2 = this.double_0 * 0.22;
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            switch (num)
            {
                case 0:
                    soundEffectRequest.Filename = "Mining_1.wav";
                    break;
                case 1:
                    soundEffectRequest.Filename = "Mining_2.wav";
                    break;
                case 2:
                    soundEffectRequest.Filename = "Mining_3.wav";
                    break;
                case 3:
                    soundEffectRequest.Filename = "mining_4.wav";
                    num2 = this.double_0 * 0.5;
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = num2 * distance;
            return soundEffectRequest;
        }

        // Token: 0x06000980 RID: 2432 RVA: 0x001360A4 File Offset: 0x001360A4
        public SoundEffectRequest ResolveThunder(double balance, double distance)
        {
            int num = this.random_0.Next(0, 3);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            switch (num)
            {
                case 0:
                    soundEffectRequest.Filename = "thunder1.wav";
                    break;
                case 1:
                    soundEffectRequest.Filename = "thunder2.wav";
                    break;
                case 2:
                    soundEffectRequest.Filename = "thunder3.wav";
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = this.double_0 * 1.3 * distance;
            return soundEffectRequest;
        }

        // Token: 0x06000981 RID: 2433 RVA: 0x00136134 File Offset: 0x00136134
        public SoundEffectRequest ResolveConstruction(double balance, double distance)
        {
            int num = this.random_0.Next(0, 5);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            switch (num)
            {
                case 0:
                    soundEffectRequest.Filename = "construction.wav";
                    break;
                case 1:
                    soundEffectRequest.Filename = "construction_2.wav";
                    break;
                case 2:
                    soundEffectRequest.Filename = "construction_3.wav";
                    break;
                case 3:
                    soundEffectRequest.Filename = "construction_4.wav";
                    break;
                case 4:
                    soundEffectRequest.Filename = "construction_5.wav";
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = this.double_0 * 0.7 * distance;
            return soundEffectRequest;
        }

        // Token: 0x06000982 RID: 2434 RVA: 0x001361E4 File Offset: 0x001361E4
        public SoundEffectRequest ResolveGasMining(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            int num = this.random_0.Next(0, 3);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            switch (num)
            {
                case 0:
                    soundEffectRequest.Filename = "GasMining1.wav";
                    break;
                case 1:
                    soundEffectRequest.Filename = "GasMining2.wav";
                    break;
                case 2:
                    soundEffectRequest.Filename = "gasmining3.wav";
                    break;
            }
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = this.double_0 * 0.5 * distance;
            return soundEffectRequest;
        }

        // Token: 0x06000983 RID: 2435 RVA: 0x00136274 File Offset: 0x00136274
        public void PlayAlert(double balance)
        {
        }

        // Token: 0x06000984 RID: 2436 RVA: 0x00136278 File Offset: 0x00136278
        public SoundEffectRequest ResolvePlanetExplosion(int size, double balance, double distance)
        {
            string text = string.Empty;
            distance = Math.Min(1.0, distance);
            text = "planetExplosion.wav";
            double num = 1.0;
            num = Math.Min(1.0, num);
            num = 2.1;
            return new SoundEffectRequest
            {
                Filename = text,
                Balance = balance,
                Volume = this.double_0 * num * distance
            };
        }

        // Token: 0x06000985 RID: 2437 RVA: 0x001362F8 File Offset: 0x001362F8
        public SoundEffectRequest ResolveExplosion(int size, double balance, double distance)
        {
            string text = string.Empty;
            distance = Math.Min(1.0, distance);
            double num2;
            if (size < 100)
            {
                int num = this.random_0.Next(0, 3);
                if (num == 0)
                {
                    text = "explosion_small.wav";
                }
                else if (num == 1)
                {
                    text = "explosion_small2.wav";
                }
                else
                {
                    text = "explosion_small3.wav";
                }
                num2 = 0.75;
            }
            else
            {
                int num3 = this.random_0.Next(0, 3);
                if (num3 == 0)
                {
                    text = "explosion.wav";
                }
                else if (num3 == 1)
                {
                    text = "explosion2.wav";
                }
                else
                {
                    text = "explosion3.wav";
                }
                num2 = 0.9;
                if (size > 110)
                {
                    num2 = 2.5;
                }
            }
            num2 = Math.Min(1.0, num2);
            return new SoundEffectRequest
            {
                Filename = text,
                Balance = balance,
                Volume = this.double_0 * num2 * distance
            };
        }

        // Token: 0x06000986 RID: 2438 RVA: 0x001363E0 File Offset: 0x001363E0
        //public void PlayEffect(string filename, double balance)
        //{
        //    this.PlayEffect(filename, balance, this.double_0, 0);
        //}

        // Token: 0x06000987 RID: 2439 RVA: 0x001363F4 File Offset: 0x001363F4
        public void PlayEffect(string filename, double balance, double volume, int frequency)
        {
            try
            {
                SoundEffectInstance secondaryBuffer = null;
                SoundEffect obj = null;
                if (this.sfxBank.ContainsKey(filename))
                { obj = this.sfxBank[filename]; }

                if (obj != null && obj is SoundEffect)
                {
                    lock (this.@lock)
                    {
                        secondaryBuffer = obj.CreateInstance();
                        this.list_0.Add(secondaryBuffer);
                    }
                }
                else
                {
                    string text = string.Empty;
                    if (!string.IsNullOrEmpty(this.string_1))
                    {
                        text = string.Concat(new string[] { this.string_0, "\\Customization\\", this.string_1, "\\sounds\\effects\\", filename });
                        if (!File.Exists(text))
                        {
                            text = this.string_0 + "\\sounds\\effects\\" + filename;
                        }
                    }
                    else
                    {
                        text = this.string_0 + "\\sounds\\effects\\" + filename;
                        if (!File.Exists(text))
                        {
                            text = string.Empty;
                        }
                    }
                    if (!string.IsNullOrEmpty(text))
                    {
                        this.sfxBank[filename] = LoadSoundEffect(method_1(text));
                        secondaryBuffer = this.sfxBank[filename].CreateInstance();
                    }
                }
                if (secondaryBuffer != null && !secondaryBuffer.IsDisposed)
                {
                    //int num = (int)(balance * 3000.0);
                    //volume += 0.05;
                    //volume = Math.Min(1.0, volume);
                    //int num2 = (int)Math.Pow(10.0, (1.0 - volume) / 0.25) * -1;
                    //num2 = Math.Min(0, Math.Max(num2, -10000));
                    //num = Math.Max(0, Math.Min(num, 10000));
                    secondaryBuffer.Pan = (float)Math.Clamp(balance, -1, 1);
                    secondaryBuffer.Volume = (float)volume;
                    if (frequency > 0)
                    {
                        secondaryBuffer.Pitch = frequency;
                    }
                    secondaryBuffer.Play();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
