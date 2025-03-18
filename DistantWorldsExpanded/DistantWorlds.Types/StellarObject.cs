// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.StellarObject
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public abstract class StellarObject : ISerializable
    {
        public double Xpos;
        public double Ypos;
        public short GameEventId = short.MinValue;
        public CargoList Cargo;
        public PopulationList Population;
        public Empire Empire;
        public Empire Owner;
        public DockingBayList DockingBays;
        public ConstructionQueue ConstructionQueue;
        public bool IsRefuellingDepot;
        public bool IsShipYard;
        public TroopList Troops;
        public CharacterList Characters;
        public string Name = string.Empty;
        public BuiltObjectList DockingBayWaitQueue;
        public float Stealth = 1f;
        //public double SortTag;
        public byte RaidCountdown;
        public StellarObjectList Attackers;
        public StellarObjectList Pursuers;
        public int FirepowerRaw;
        public short TopSpeed;
        public bool IsFunctional;
        public bool HasBeenDestroyed;
        public float CurrentSpeed;
        public float TargetHeading;
        public Habitat ParentHabitat;
        public BuiltObject ParentBuiltObject;
        public StellarObject CurrentTarget;
        public int Size;
        public StellarObject()
        {
        }

        public StellarObject(SerializationInfo info, StreamingContext context)
          : this()
        {
            using (MemoryStream input = new MemoryStream((byte[])info.GetValue("StO_D", typeof(byte[]))))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)input))
                {
                    this.Xpos = binaryReader.ReadDouble();
                    this.Ypos = binaryReader.ReadDouble();
                    this.GameEventId = binaryReader.ReadInt16();
                    this.IsRefuellingDepot = binaryReader.ReadBoolean();
                    this.IsShipYard = binaryReader.ReadBoolean();
                    this.Stealth = binaryReader.ReadSingle();
                    //this.SortTag = binaryReader.ReadDouble();
                    binaryReader.ReadDouble();
                    this.FirepowerRaw = binaryReader.ReadInt32();
                    this.TopSpeed = binaryReader.ReadInt16();
                    this.IsFunctional = binaryReader.ReadBoolean();
                    this.HasBeenDestroyed = binaryReader.ReadBoolean();
                    this.CurrentSpeed = binaryReader.ReadSingle();
                    this.TargetHeading = binaryReader.ReadSingle();
                    this.Size = binaryReader.ReadInt32();
                    this.RaidCountdown = binaryReader.ReadByte();
                    this.Name = binaryReader.ReadString();
                    binaryReader.Close();
                }
            }
            this.Cargo = (CargoList)info.GetValue("Cg", typeof(CargoList));
            this.Population = (PopulationList)info.GetValue("Po", typeof(PopulationList));
            this.Empire = (Empire)info.GetValue("Em", typeof(Empire));
            this.Owner = (Empire)info.GetValue("Ow", typeof(Empire));
            this.DockingBays = (DockingBayList)info.GetValue("DB", typeof(DockingBayList));
            this.ConstructionQueue = (ConstructionQueue)info.GetValue("CQ", typeof(ConstructionQueue));
            this.Troops = (TroopList)info.GetValue("Tr", typeof(TroopList));
            this.Characters = (CharacterList)info.GetValue("Ch", typeof(CharacterList));
            this.DockingBayWaitQueue = (BuiltObjectList)info.GetValue("DW", typeof(BuiltObjectList));
            this.Attackers = (StellarObjectList)info.GetValue("At", typeof(StellarObjectList));
            this.Pursuers = (StellarObjectList)info.GetValue("Pu", typeof(StellarObjectList));
            this.ParentHabitat = (Habitat)info.GetValue("PH", typeof(Habitat));
            this.ParentBuiltObject = (BuiltObject)info.GetValue("PB", typeof(BuiltObject));
            this.CurrentTarget = (StellarObject)info.GetValue("CT", typeof(StellarObject));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)output))
                {
                    binaryWriter.Write(this.Xpos);
                    binaryWriter.Write(this.Ypos);
                    binaryWriter.Write(this.GameEventId);
                    binaryWriter.Write(this.IsRefuellingDepot);
                    binaryWriter.Write(this.IsShipYard);
                    binaryWriter.Write(this.Stealth);
                    //binaryWriter.Write(this.SortTag);
                    binaryWriter.Write(0.0);
                    binaryWriter.Write(this.FirepowerRaw);
                    binaryWriter.Write(this.TopSpeed);
                    binaryWriter.Write(this.IsFunctional);
                    binaryWriter.Write(this.HasBeenDestroyed);
                    binaryWriter.Write(this.CurrentSpeed);
                    binaryWriter.Write(this.TargetHeading);
                    binaryWriter.Write(this.Size);
                    binaryWriter.Write(this.RaidCountdown);
                    binaryWriter.Write(this.Name);
                    binaryWriter.Flush();
                    binaryWriter.Close();
                    info.AddValue("StO_D", (object)output.ToArray());
                }
            }
            info.AddValue("Cg", (object)this.Cargo);
            info.AddValue("Po", (object)this.Population);
            info.AddValue("Em", (object)this.Empire);
            info.AddValue("Ow", (object)this.Owner);
            info.AddValue("DB", (object)this.DockingBays);
            info.AddValue("CQ", (object)this.ConstructionQueue);
            info.AddValue("Tr", (object)this.Troops);
            info.AddValue("Ch", (object)this.Characters);
            info.AddValue("DW", (object)this.DockingBayWaitQueue);
            info.AddValue("At", (object)this.Attackers);
            info.AddValue("Pu", (object)this.Pursuers);
            info.AddValue("PH", (object)this.ParentHabitat);
            info.AddValue("PB", (object)this.ParentBuiltObject);
            info.AddValue("CT", (object)this.CurrentTarget);
        }

        public abstract int CargoSpace { get; }

        public abstract int TroopCapacityRemaining { get; }

        public class SortStellarObject : IComparer<StellarObject>
        {
            int IComparer<StellarObject>.Compare(StellarObject x, StellarObject y)
            {
                throw new NotImplementedException();
                //x != null && y != null ? x.SortTag.CompareTo(y.SortTag) : 0;
            }
        }
    }
}
