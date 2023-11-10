using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public enum FilterType
    {
        NotSet =0,
        TotalResource,
        SelectedResource,
    }
    public class HabitatPrioritizationListFilter
    {
        public bool Enabled { get; set; }
        public FilterType FilterType { get; set; }
        public int Percentage { get; set; }
        public Resource SelectedResource { get; set; }
        //public int TotalResourcePercentage { get; set; }
        //public int CurrentResourceResourcePercentage { get; set; }
    }
}
