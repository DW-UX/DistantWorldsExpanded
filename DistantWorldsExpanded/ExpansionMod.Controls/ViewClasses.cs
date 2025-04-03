using ExpansionMod.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.Controls
{
    public class ViewKeyMappingTarget
    {
        public virtual string ViewName
        {
            get { return this.KeyTarget.ToString(); }
        }
        public KeyMappingFriendlyNames KeyTarget { get; set; }
        public int TargetMethodId { get; set; }
        public List<ViewMappedHotKey> MappedHotKeys { get; set; }
    }
    public class ViewMappedHotKey
    {
        public virtual string ViewName
        {
            get
            {
                string temp = $"{this.Key[0]}";
                for (int i = 1; i < this.Key.Count; i++)
                {
                    temp += $" + {this.Key[i]}";
                }
                return temp;
            }
        }
        public List<Keys> Key { get; set; }
    }
    public class EnumView<T> where T : Enum
    {
        public string Name { get; set; }
        public T Key { get; set; }
    }
    //public class EnumViewComparer<T> : IEqualityComparer<T> where T : EnumView<Keys>
    //{
    //    public bool Equals(T x, T y)
    //    {
    //        return x.Key.Equals(y.Key);
    //    }

    //    public int GetHashCode(T obj)
    //    {
    //        return obj.Key.GetHashCode();
    //    }
    //}
}
