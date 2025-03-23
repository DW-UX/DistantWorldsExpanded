using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.DBLoader
{
    internal class ModDbGlobals
    {
        public static readonly ImmutableList<string> Files = ImmutableList.Create<string>("characterNames.xml", "raceFamilies.xml", "raceFamilyBiases.xml", "plagues.xml", "races.xml", "governments.xml", "GovernmentBiases.xml", "Facilities.xml", "Fighters.xml", "Resources.xml", "Research.xml", "RaceBiases.xml", "Components.xml");
    }
}
