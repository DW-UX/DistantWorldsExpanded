using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TxtFileParser
{
    static class Helper
    {
        public static bool ParseBool(string value)
        {
            bool res = false;
            if (value.Trim().ToUpperInvariant() == "Y")
                res = true;
            else if (value.Trim().ToUpperInvariant() == "N")
                res = false;
            else
                throw new Exception($"Invalid bool string value: {value}. Valid values are Y or N");
            return res;
        }
    }
}
