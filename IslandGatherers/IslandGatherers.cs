using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGatherers
{
    public class IslandGatherers : Mod
    {
        internal static IMonitor monitor;
        internal static IModHelper modHelper;

        public override void Entry(IModHelper helper)
        {
            // Set up the monitor and helper
            monitor = Monitor;
            modHelper = helper;
        }
    }
}
