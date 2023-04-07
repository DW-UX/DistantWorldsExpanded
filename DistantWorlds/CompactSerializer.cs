// Decompiled with JetBrains decompiler
// Type: DistantWorlds.CompactSerializer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Xml;

namespace DistantWorlds
{
  public class CompactSerializer : XmlObjectSerializer
  {
    private NetDataContractSerializer netDataContractSerializer_0;

    public FormatterAssemblyStyle AssemblyFormat
    {
      get => this.netDataContractSerializer_0.AssemblyFormat;
      set => this.netDataContractSerializer_0.AssemblyFormat = value;
    }

    public CompactSerializer(Type type, List<Type> knownTypes) : base()
    {
      Class7.VEFSJNszvZKMZ();
      this.netDataContractSerializer_0 = new NetDataContractSerializer();
      // ISSUE: explicit constructor call
      this.netDataContractSerializer_0 = new NetDataContractSerializer();
    }

    public override void WriteStartObject(XmlDictionaryWriter writer, object graph) => this.netDataContractSerializer_0.WriteStartObject(writer, graph);

    public override void WriteEndObject(XmlDictionaryWriter writer) => this.netDataContractSerializer_0.WriteEndObject(writer);

    public override void WriteObjectContent(XmlDictionaryWriter writer, object graph) => this.netDataContractSerializer_0.WriteObjectContent(writer, graph);

    public override void WriteObject(XmlDictionaryWriter writer, object graph)
    {
      this.WriteStartObject(writer, graph);
      if (graph.GetType().Assembly.FullName.StartsWith("DistantWorlds.Types"))
        writer.WriteAttributeString("xmlns", "p", (string) null, "http://schemas.datacontract.org/2004/07/DistantWorlds.Types");
      this.WriteObjectContent(writer, graph);
      this.WriteEndObject(writer);
    }

    public override bool IsStartObject(XmlDictionaryReader reader) => this.netDataContractSerializer_0.IsStartObject(reader);

    public override object ReadObject(XmlDictionaryReader reader) => this.netDataContractSerializer_0.ReadObject(reader);

    public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName) => this.netDataContractSerializer_0.ReadObject(reader, verifyObjectName);
  }
}
