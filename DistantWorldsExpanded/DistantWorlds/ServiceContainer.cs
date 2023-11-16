// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ServiceContainer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Collections.Generic;

namespace DistantWorlds
{
    public class ServiceContainer : IServiceProvider
    {
        private Dictionary<Type, object> dictionary_0;

        public void AddService<T>(T service)
        {
            dictionary_0.Add(typeof(T), service);
        }

        public object GetService(Type serviceType)
        {
            dictionary_0.TryGetValue(serviceType, out var value);
            return value;
        }

        public ServiceContainer():base()
        {
            
            dictionary_0 = new Dictionary<Type, object>();
        }
    }

}
