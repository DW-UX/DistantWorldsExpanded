using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public interface IEmModMain
    {
        public string GetRepairPriorityTemplateName(Design design);
    }
}
