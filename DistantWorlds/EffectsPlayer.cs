// Decompiled with JetBrains decompiler
// Type: DistantWorlds.EffectsPlayer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using Microsoft.DirectX.DirectSound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class EffectsPlayer
    {
        private object object_0;

        private Main main_0;

        private Device device_0;

        private List<SecondaryBuffer> list_0;

        private Hashtable hashtable_0;

        private double double_0;

        private string string_0;

        private string string_1;

        private Random random_0;

        public double Volume
        {
            get
            {
                return double_0;
            }
            set
            {
                double_0 = value;
            }
        }

        public EffectsPlayer(Main parent, string applicationStartupPath, string customizationSetName, Device soundDevice):base()
        {
            Class7.VEFSJNszvZKMZ();
            object_0 = new object();
            list_0 = new List<SecondaryBuffer>();
            hashtable_0 = new Hashtable();
            double_0 = 0.7;
            lock (object_0)
            {
                main_0 = parent;
                device_0 = soundDevice;
                list_0 = new List<SecondaryBuffer>();
                Initialize(applicationStartupPath, customizationSetName);
                random_0 = new Random((int)DateTime.Now.Ticks);
            }
        }

        public void Initialize(string applicationStartupPath, string customizationSetName)
        {
            Clear();
            string_0 = applicationStartupPath;
            string_1 = customizationSetName;
            method_0(applicationStartupPath, customizationSetName);
        }

        public void Clear()
        {
            lock (object_0)
            {
                foreach (DictionaryEntry item in hashtable_0)
                {
                    if (item.Value is SecondaryBuffer)
                    {
                        SecondaryBuffer secondaryBuffer = (SecondaryBuffer)item.Value;
                        if (secondaryBuffer != null && !secondaryBuffer.Disposed && !secondaryBuffer.Status.Playing && !secondaryBuffer.Status.Looping)
                        {
                            secondaryBuffer.Dispose();
                        }
                    }
                }
                hashtable_0.Clear();
            }
        }

        private void method_0(string string_2, string string_3)
        {
            lock (object_0)
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
                List<MemoryStream> list2 = new List<MemoryStream>();
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
                    MemoryStream memoryStream = method_1(text3);
                    if (memoryStream != null)
                    {
                        list2.Add(memoryStream);
                        list3.Add(list[i]);
                    }
                }
                for (int j = 0; j < list2.Count; j++)
                {
                    if (!hashtable_0.ContainsKey(list3[j]))
                    {
                        SecondaryBuffer value = method_2(list2[j], device_0);
                        hashtable_0.Add(list3[j], value);
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

        private MemoryStream method_1(string string_2)
        {
            MemoryStream memoryStream = null;
            try
            {
                if (File.Exists(string_2))
                {
                    using FileStream fileStream = new FileStream(string_2, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    memoryStream = new MemoryStream(buffer);
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
                memoryStream = null;
            }
            if (memoryStream != null)
            {
                return memoryStream;
            }
            string text = string.Format(TextResolver.GetText("Could not load required sound"), string_2);
            MessageBox.Show(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(-1);
            return null;
        }

        private SecondaryBuffer method_2(Stream stream_0, Device device_1)
        {
            BufferDescription bufferDescription = new BufferDescription();
            bufferDescription.ControlPan = true;
            bufferDescription.ControlVolume = true;
            bufferDescription.ControlEffects = false;
            if (stream_0.Length < 20000L)
            {
                bufferDescription.ControlEffects = false;
            }
            bufferDescription.ControlFrequency = false;
            bufferDescription.Control3D = false;
            bufferDescription.DeferLocation = true;
            stream_0.Position = 0L;
            return new SecondaryBuffer(stream_0, bufferDescription, device_1);
        }

        private SecondaryBuffer method_3(string string_2, Device device_1)
        {
            BufferDescription bufferDescription = new BufferDescription();
            bufferDescription.ControlPan = true;
            bufferDescription.ControlVolume = true;
            bufferDescription.ControlEffects = false;
            bufferDescription.ControlFrequency = false;
            bufferDescription.Control3D = false;
            bufferDescription.DeferLocation = true;
            return new SecondaryBuffer(string_2, bufferDescription, device_1);
        }

        public void ClearFinishedBuffers()
        {
            lock (object_0)
            {
                List<SecondaryBuffer> list = new List<SecondaryBuffer>();
                for (int i = 0; i < list_0.Count; i++)
                {
                    SecondaryBuffer secondaryBuffer = list_0[i];
                    try
                    {
                        if (secondaryBuffer != null && !secondaryBuffer.Disposed)
                        {
                            BufferStatus status = secondaryBuffer.Status;
                            if (!status.Playing && !status.Looping)
                            {
                                secondaryBuffer.Dispose();
                                list.Add(secondaryBuffer);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        list.Add(secondaryBuffer);
                    }
                }
                foreach (SecondaryBuffer item in list)
                {
                    list_0.Remove(item);
                }
            }
        }

        private SecondaryBuffer method_4(string string_2)
        {
            BufferDescription bufferDescription = new BufferDescription();
            bufferDescription.ControlPan = true;
            bufferDescription.ControlVolume = true;
            bufferDescription.ControlEffects = false;
            bufferDescription.ControlFrequency = false;
            bufferDescription.Control3D = false;
            bufferDescription.DeferLocation = true;
            SecondaryBuffer secondaryBuffer = new SecondaryBuffer(string_2, bufferDescription, device_0);
            list_0.Add(secondaryBuffer);
            return secondaryBuffer;
        }

        public SoundEffectRequest ResolveIonStrike(double balance, double distance)
        {
            string filename = "ion_strike.wav";
            double num = 1.8;
            distance = Math.Min(1.0, distance);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = filename;
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * num * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveWeapon(Component component, double balance, double distance)
        {
            string filename = string.Empty;
            double num = 0.23;
            if (component != null)
            {
                filename = component.SoundEffectFilename;
            }
            distance = Math.Min(1.0, distance);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = filename;
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * num * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveFighterWeapon(string effectFilename, ComponentType type, double balance, double distance)
        {
            string filename = string.Empty;
            double num = 0.19;
            switch (type)
            {
                case ComponentType.WeaponBeam:
                    filename = effectFilename;
                    break;
                case ComponentType.WeaponTorpedo:
                    filename = effectFilename;
                    num = 0.25;
                    break;
                case ComponentType.WeaponMissile:
                    filename = effectFilename;
                    num = 0.25;
                    break;
            }
            distance = Math.Min(1.0, distance);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = filename;
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * num * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveAmbientEffect(int soundScheme, double balance, double distance, out int nextEffectOffset)
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            string filename = string.Empty;
            double num = double_0 * 0.7;
            nextEffectOffset = 4000;
            int num2 = 0;
            switch (soundScheme)
            {
                case 0:
                    switch (random_0.Next(0, 3))
                    {
                        case 0:
                            filename = "ambient1_voice1.wav";
                            nextEffectOffset = 5500;
                            break;
                        case 1:
                            filename = "ambient1_voice2.wav";
                            nextEffectOffset = 10800;
                            break;
                        case 2:
                            filename = "ambient1_voice3.wav";
                            nextEffectOffset = 6600;
                            break;
                    }
                    break;
                case 1:
                    switch (random_0.Next(0, 4))
                    {
                        case 0:
                            filename = "ambient2_voice1.wav";
                            nextEffectOffset = 9200;
                            break;
                        case 1:
                            filename = "ambient2_voice2.wav";
                            nextEffectOffset = 9200;
                            break;
                        case 2:
                            filename = "ambient2_voice3.wav";
                            nextEffectOffset = 8200;
                            break;
                        case 3:
                            filename = "ambient2_voice4.wav";
                            nextEffectOffset = 9200;
                            break;
                    }
                    break;
                case 2:
                    switch (random_0.Next(0, 3))
                    {
                        case 0:
                            filename = "ambient3_energy1.wav";
                            nextEffectOffset = 8400;
                            break;
                        case 1:
                            filename = "ambient3_energy2.wav";
                            nextEffectOffset = 8400;
                            break;
                        case 2:
                            filename = "ambient3_energy3.wav";
                            nextEffectOffset = 11400;
                            break;
                    }
                    break;
                case 3:
                    switch (random_0.Next(0, 5))
                    {
                        case 0:
                        case 1:
                            filename = "ambient4_boom1.wav";
                            nextEffectOffset = 5500;
                            break;
                        case 2:
                        case 3:
                            filename = "ambient4_boom2.wav";
                            nextEffectOffset = 6500;
                            break;
                        case 4:
                            filename = "ambient4_boom3.wav";
                            nextEffectOffset = 8500;
                            break;
                    }
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = num * distance;
            soundEffectRequest.Filename = filename;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveAttackClick()
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double volume = double_0 * 1.0;
            soundEffectRequest.Filename = "attack_click.wav";
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = volume;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveImportantMessage()
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double volume = double_0 * 0.7;
            soundEffectRequest.Filename = "message_major.wav";
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = volume;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveMessage(EmpireMessageType messageType)
        {
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            string filename = string.Empty;
            double volume = double_0 * 0.7;
            switch (messageType)
            {
                case EmpireMessageType.BattleUnderAttack:
                case EmpireMessageType.IncomingEnemyFleet:
                    filename = "message_alarm.wav";
                    volume = double_0 * 0.4;
                    break;
                case EmpireMessageType.Informational:
                case EmpireMessageType.ShipMissionComplete:
                case EmpireMessageType.ShipNeedsRefuelling:
                    filename = "message_minor.wav";
                    break;
                case EmpireMessageType.CharacterDeath:
                case EmpireMessageType.CharacterMissionFailure:
                case EmpireMessageType.ColonyLost:
                case EmpireMessageType.ColonyRebelling:
                case EmpireMessageType.RequestHonorMutualDefense:
                case EmpireMessageType.BlockadeInitiated:
                case EmpireMessageType.Revolution:
                case EmpireMessageType.GeneralWarning:
                case EmpireMessageType.GeneralBadEvent:
                case EmpireMessageType.GeneralDecision:
                case EmpireMessageType.ColonyFacilityCancelled:
                case EmpireMessageType.ColonyWonderBegun:
                case EmpireMessageType.ColonyDestroyed:
                case EmpireMessageType.ResearchCriticalBreakthrough:
                case EmpireMessageType.ResearchCriticalFailure:
                case EmpireMessageType.ShipBaseBoardedLost:
                case EmpireMessageType.PirateSmugglerDetected:
                case EmpireMessageType.PlanetaryFacilityDestroyed:
                case EmpireMessageType.RaidVictim:
                    filename = "message_major.wav";
                    break;
                case EmpireMessageType.DiplomaticRelationChange:
                case EmpireMessageType.ProposeDiplomaticRelation:
                case EmpireMessageType.AcceptDiplomaticRelation:
                case EmpireMessageType.RefuseDiplomaticRelation:
                case EmpireMessageType.RemoveColoniesFromSystem:
                case EmpireMessageType.StopMissionsAgainstUs:
                case EmpireMessageType.StopAttacks:
                case EmpireMessageType.LeaveSystem:
                case EmpireMessageType.RequestJointWar:
                case EmpireMessageType.RequestJointTradeSanctions:
                case EmpireMessageType.RequestStopWar:
                case EmpireMessageType.RequestLiftTradeSanctions:
                case EmpireMessageType.GiveGift:
                case EmpireMessageType.ShipBaseCompleted:
                case EmpireMessageType.ShipBasePurchased:
                case EmpireMessageType.NewColony:
                case EmpireMessageType.NewColonyFailed:
                case EmpireMessageType.ResearchBreakthrough:
                case EmpireMessageType.BattleAttacking:
                case EmpireMessageType.CharacterAppearance:
                case EmpireMessageType.CharacterMissionAccomplished:
                case EmpireMessageType.EmpireDiscovered:
                case EmpireMessageType.ColonyGained:
                case EmpireMessageType.ColonyDefended:
                case EmpireMessageType.EmpireDefeated:
                case EmpireMessageType.BlockadeCancelled:
                case EmpireMessageType.ExplorationRuins:
                case EmpireMessageType.ExplorationBuiltObject:
                case EmpireMessageType.ExplorationHabitat:
                case EmpireMessageType.ExplorationLocation:
                case EmpireMessageType.GalacticHistory:
                case EmpireMessageType.SellInfoUnmetEmpire:
                case EmpireMessageType.SellInfoIndependentColony:
                case EmpireMessageType.SellInfoSystemMap:
                case EmpireMessageType.SellInfoRuins:
                case EmpireMessageType.SellInfoDebrisField:
                case EmpireMessageType.SellInfoRestrictedArea:
                case EmpireMessageType.SellInfoPlanetDestroyer:
                case EmpireMessageType.PirateOfferProtection:
                case EmpireMessageType.CancelPirateProtection:
                case EmpireMessageType.RestrictedResourceDiscovered:
                case EmpireMessageType.RestrictedResourceTradingAllowed:
                case EmpireMessageType.RestrictedResourceTradingBlocked:
                case EmpireMessageType.OfferTrade:
                case EmpireMessageType.ShipNeedsRepair:
                case EmpireMessageType.RemoveForcesFromSystem:
                case EmpireMessageType.GeneralNeutralEvent:
                case EmpireMessageType.GeneralGoodEvent:
                case EmpireMessageType.HistoryOfferLocationHint:
                case EmpireMessageType.HistoryOfferStoryClue:
                case EmpireMessageType.ColonyFacilityCompleted:
                case EmpireMessageType.ColonyShipMissionCancelled:
                case EmpireMessageType.StoryMessage:
                case EmpireMessageType.AdvisorSuggestion:
                case EmpireMessageType.MilitaryRefuelingAllowed:
                case EmpireMessageType.MilitaryRefuelingBlocked:
                case EmpireMessageType.MiningRightsAllowed:
                case EmpireMessageType.MiningRightsBlocked:
                case EmpireMessageType.CharacterSkillTraitChange:
                case EmpireMessageType.GalacticNewsNet:
                case EmpireMessageType.ShipBaseBoardedCaptured:
                case EmpireMessageType.PirateAttackMissionAvailable:
                case EmpireMessageType.PirateAttackMissionCompleted:
                case EmpireMessageType.PirateAttackMissionFailed:
                case EmpireMessageType.PirateDefendMissionFailed:
                case EmpireMessageType.PirateDefendMissionAvailable:
                case EmpireMessageType.PirateDefendMissionCompleted:
                case EmpireMessageType.PirateSmugglingMissionAvailable:
                case EmpireMessageType.PirateSmugglingMissionCompleted:
                case EmpireMessageType.ShipBaseScrapped:
                case EmpireMessageType.ConstructionResourceShortage:
                case EmpireMessageType.RaidBonuses:
                case EmpireMessageType.PlanetaryFacilityDamaged:
                    filename = "message_standard.wav";
                    break;
            }
            soundEffectRequest.Filename = filename;
            soundEffectRequest.Balance = 0.0;
            soundEffectRequest.Volume = volume;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveHyperjumpEntry(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = "Hyperjump_Enter.wav";
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * 0.45 * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveHyperjumpExit(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = "Hyperjump_Exit.wav";
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * 0.5 * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveStar(HabitatType starType, double balance, double distance)
        {
            int num = random_0.Next(0, 2);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            double num2 = 1.0;
            string filename = string.Empty;
            int num3 = 0;
            switch (starType)
            {
                case HabitatType.MainSequence:
                    filename = "star_basic1.wav";
                    if (num == 1)
                    {
                        filename = "star_basic2.wav";
                    }
                    num3 = 23;
                    num2 = 0.7;
                    break;
                case HabitatType.RedGiant:
                case HabitatType.SuperGiant:
                    filename = "star_bass1.wav";
                    if (num == 1)
                    {
                        filename = "star_bass2.wav";
                    }
                    num3 = 25;
                    num2 = 0.7;
                    break;
                case HabitatType.WhiteDwarf:
                    filename = "star_hollow1.wav";
                    if (num == 1)
                    {
                        filename = "star_hollow2.wav";
                    }
                    num3 = 27;
                    num2 = 0.5;
                    break;
                case HabitatType.Neutron:
                    filename = "star_ring1.wav";
                    if (num == 1)
                    {
                        filename = "star_ring2.wav";
                    }
                    num3 = 29;
                    num2 = 1.2;
                    break;
                case HabitatType.BlackHole:
                    filename = "star_intense1.wav";
                    if (num == 1)
                    {
                        filename = "star_intense2.wav";
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
            soundEffectRequest.Filename = filename;
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * 1.2 * distance * num2;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveMining(double balance, double distance)
        {
            int num = random_0.Next(0, 4);
            double num2 = double_0 * 0.22;
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
                    num2 = double_0 * 0.5;
                    break;
            }
            distance = Math.Min(1.0, distance);
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = num2 * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveThunder(double balance, double distance)
        {
            int num = random_0.Next(0, 3);
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
            soundEffectRequest.Volume = double_0 * 1.3 * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveConstruction(double balance, double distance)
        {
            int num = random_0.Next(0, 5);
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
            soundEffectRequest.Volume = double_0 * 0.7 * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveGasMining(double balance, double distance)
        {
            distance = Math.Min(1.0, distance);
            int num = random_0.Next(0, 3);
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
            soundEffectRequest.Volume = double_0 * 0.5 * distance;
            return soundEffectRequest;
        }

        public void PlayAlert(double balance)
        {
        }

        public SoundEffectRequest ResolvePlanetExplosion(int size, double balance, double distance)
        {
            string empty = string.Empty;
            double num = 0.75;
            distance = Math.Min(1.0, distance);
            empty = "planetExplosion.wav";
            num = 1.0;
            num = Math.Min(1.0, num);
            num = 2.1;
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = empty;
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * num * distance;
            return soundEffectRequest;
        }

        public SoundEffectRequest ResolveExplosion(int size, double balance, double distance)
        {
            string empty = string.Empty;
            double num = 0.75;
            distance = Math.Min(1.0, distance);
            if (size < 100)
            {
                empty = random_0.Next(0, 3) switch
                {
                    0 => "explosion_small.wav",
                    1 => "explosion_small2.wav",
                    _ => "explosion_small3.wav",
                };
                num = 0.75;
            }
            else
            {
                empty = random_0.Next(0, 3) switch
                {
                    0 => "explosion.wav",
                    1 => "explosion2.wav",
                    _ => "explosion3.wav",
                };
                num = 0.9;
                if (size > 110)
                {
                    num = 2.5;
                }
            }
            num = Math.Min(1.0, num);
            SoundEffectRequest soundEffectRequest = new SoundEffectRequest();
            soundEffectRequest.Filename = empty;
            soundEffectRequest.Balance = balance;
            soundEffectRequest.Volume = double_0 * num * distance;
            return soundEffectRequest;
        }

        public void PlayEffect(string filename, double balance)
        {
            PlayEffect(filename, balance, double_0, 0);
        }

        public void PlayEffect(string filename, double balance, double volume, int frequency)
        {
            try
            {
                SecondaryBuffer secondaryBuffer = null;
                object obj = hashtable_0[filename];
                if (obj != null && obj is SecondaryBuffer)
                {
                    lock (object_0)
                    {
                        secondaryBuffer = ((SecondaryBuffer)obj).Clone(device_0);
                        list_0.Add(secondaryBuffer);
                    }
                }
                else
                {
                    string empty = string.Empty;
                    if (!string.IsNullOrEmpty(string_1))
                    {
                        empty = string_0 + "\\Customization\\" + string_1 + "\\sounds\\effects\\" + filename;
                        if (!File.Exists(empty))
                        {
                            empty = string_0 + "\\sounds\\effects\\" + filename;
                        }
                    }
                    else
                    {
                        empty = string_0 + "\\sounds\\effects\\" + filename;
                        if (!File.Exists(empty))
                        {
                            empty = string.Empty;
                        }
                    }
                    if (!string.IsNullOrEmpty(empty))
                    {
                        secondaryBuffer = method_4(empty);
                    }
                }
                if (secondaryBuffer != null && !secondaryBuffer.Disposed)
                {
                    int val = (int)(balance * 3000.0);
                    volume += 0.05;
                    volume = Math.Min(1.0, volume);
                    int val2 = (int)Math.Pow(10.0, (1.0 - volume) / 0.25) * -1;
                    val2 = Math.Min(0, Math.Max(val2, -10000));
                    val = (secondaryBuffer.Pan = Math.Max(0, Math.Min(val, 10000)));
                    secondaryBuffer.Volume = val2;
                    if (frequency > 0)
                    {
                        secondaryBuffer.Frequency = frequency;
                    }
                    secondaryBuffer.Play(int.MaxValue, BufferPlayFlags.LocateInSoftware);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
