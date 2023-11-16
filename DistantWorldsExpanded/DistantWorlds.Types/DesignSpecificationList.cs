// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DesignSpecificationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DesignSpecificationList : SyncList<DesignSpecification>, ISerializable
  {
    public DesignSpecificationList()
    {
    }

    public DesignSpecificationList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num1 = (int) binaryReader.ReadByte();
          for (int index1 = 0; index1 < num1; ++index1)
          {
            BuiltObjectSubRole subRole = (BuiltObjectSubRole) binaryReader.ReadByte();
            bool mobile = binaryReader.ReadBoolean();
            DesignImageScalingMode imageScalingMode = (DesignImageScalingMode) binaryReader.ReadByte();
            float num2 = binaryReader.ReadSingle();
            DesignSpecification designSpecification = new DesignSpecification(subRole, mobile);
            designSpecification.ImageScalingMode = imageScalingMode;
            designSpecification.ImageScalingFactor = num2;
            byte num3 = binaryReader.ReadByte();
            for (int index2 = 0; index2 < (int) num3; ++index2)
            {
              DesignSpecificationComponentRuleType componentRuleType = (DesignSpecificationComponentRuleType) binaryReader.ReadByte();
              ComponentCategoryType componentCategory = (ComponentCategoryType) binaryReader.ReadByte();
              ComponentType componentType = (ComponentType) binaryReader.ReadByte();
              short amount = binaryReader.ReadInt16();
              if (componentType != ComponentType.Undefined)
              {
                DesignSpecificationComponentRule specificationComponentRule = new DesignSpecificationComponentRule(componentRuleType, componentType, (int) amount);
                designSpecification.ComponentRules.Add(specificationComponentRule);
              }
              else if (componentCategory != ComponentCategoryType.Undefined)
              {
                DesignSpecificationComponentRule specificationComponentRule = new DesignSpecificationComponentRule(componentRuleType, componentCategory, (int) amount);
                designSpecification.ComponentRules.Add(specificationComponentRule);
              }
            }
            this.Add(designSpecification);
          }
        }
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this.Count > 0)
          {
            binaryWriter.Write((byte) this.Count);
            for (int index1 = 0; index1 < this.Count; ++index1)
            {
              binaryWriter.Write((byte) this[index1].SubRole);
              binaryWriter.Write(this[index1].Mobile);
              binaryWriter.Write((byte) this[index1].ImageScalingMode);
              binaryWriter.Write(this[index1].ImageScalingFactor);
              binaryWriter.Write((byte) this[index1].ComponentRules.Count);
              for (int index2 = 0; index2 < this[index1].ComponentRules.Count; ++index2)
              {
                binaryWriter.Write((byte) this[index1].ComponentRules[index2].ComponentRuleType);
                binaryWriter.Write((byte) this[index1].ComponentRules[index2].ComponentCategory);
                binaryWriter.Write((byte) this[index1].ComponentRules[index2].ComponentType);
                binaryWriter.Write((short) this[index1].ComponentRules[index2].Amount);
              }
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
          }
          else
            info.AddValue("D", (object) new byte[0]);
        }
      }
    }

    public DesignSpecification GetBySubRole(BuiltObjectSubRole subRole)
    {
      foreach (DesignSpecification bySubRole in (SyncList<DesignSpecification>) this)
      {
        if (bySubRole.SubRole == subRole)
          return bySubRole;
      }
      return (DesignSpecification) null;
    }

    public bool CheckAnyDesignSpecificationsUseComponent(Component component)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        DesignSpecification designSpecification = this[index];
        if (designSpecification != null && designSpecification.ComponentRules != null && designSpecification.ComponentRules.CheckAnyRulesUseComponent(component))
          return true;
      }
      return false;
    }
  }
}
