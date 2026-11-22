// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SyncList`1
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace DistantWorlds.Types
{
    [Serializable]
    public class SyncList<T> : List<T>
    {

        [NonSerialized]
        internal Lock _LockObject = new();
        [NonSerialized]
        internal List<T> _snapshot;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext _) => _LockObject = new Lock();

        public new void Add(T item)
        {
            lock (this._LockObject)
            {
                base.Add(item);
                _snapshot = null;
            }
        }

        public new void Remove(T item)
        {
            lock (this._LockObject)
            {
                bool removed = base.Remove(item);
                if (removed) _snapshot = null;
            }
        }

        public new void AddRange(IEnumerable<T> items)
        {
            lock (this._LockObject)
            {
                base.AddRange(items);
                _snapshot = null;
            }
        }


        public List<T> CloneThreadSafe()
        {
            var snap = Volatile.Read(ref _snapshot);
            if (snap != null) return snap;          // lock-free fast path

            lock (this._LockObject)
            {
                return _snapshot ??= this.ToList();
            }
        }

        public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)new SyncListEnumerator<T>(this);
    }
}
