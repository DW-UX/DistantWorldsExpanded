// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SyncListEnumerator`1
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  public class SyncListEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
  {
    public SyncList<T> _SyncList;
    private int _Position = -1;

    public SyncListEnumerator(SyncList<T> syncList) => this._SyncList = syncList;

    public bool MoveNext()
    {
      ++this._Position;
      return this._Position < this._SyncList.Count;
    }

    public void Reset() => this._Position = -1;

    public void Dispose()
    {
    }

    public T Current => this._SyncList[this._Position];

    object IEnumerator.Current
    {
      get
      {
        try
        {
          return (object) this._SyncList[this._Position];
        }
        catch (IndexOutOfRangeException ex)
        {
          throw new InvalidOperationException();
        }
      }
    }
  }
}
