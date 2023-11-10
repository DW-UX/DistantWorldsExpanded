using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.Controls
{
    public static class Helper
    {
    }
    public static class HelperEnum<T> where T : Enum
    {
        public static List<EnumView<T>> GetViewEnum()
        {
            List<EnumView<T>> res = new List<EnumView<T>>();
            var values = Enum.GetValues(typeof(T));
            
            foreach (T item in values)
                res.Add(new EnumView<T>() { Key = item, Name =  item.ToString() });
            return res;
        }
    }

}
