// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SyncList`1
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SyncList<T> : List<T>
  {
    internal object _LockObject = new object();

    public new void Add(T item)
    {
      lock (this._LockObject)
        base.Add(item);
    }

    public void Remove(T item)
    {
      lock (this._LockObject)
        base.Remove(item);
    }

    public new void AddRange(IEnumerable<T> items)
    {
      lock (this._LockObject)
        base.AddRange(items);
    }

    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) new SyncListEnumerator<T>(this);
  }
}
