using Harmony;
using IslandGatherers.Framework;
using IslandGatherers.Framework.Patches;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System;
using System.Collections.Generic;
using System.IO;
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

            // Set up our asset manager
            AssetManager.SetUpAssets(helper);

            // Load our Harmony patches
            try
            {
                var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);

                // Apply our patches
                new ObjectPatch(monitor).Apply(harmony);
            }
            catch (Exception e)
            {
                Monitor.Log($"Issue with Harmony patching: {e}", LogLevel.Error);
                return;
            }

            // Hook into GameLoop events
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        }
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // Hook into the APIs we utilize
            if (Helper.ModRegistry.IsLoaded("spacechase0.JsonAssets") && ApiManager.HookIntoJsonAssets(Helper))
            {
                var jsonAssetsApi = ApiManager.GetJsonAssetsApi();

                // Check if furyx639's Expanded Storage is in the current mod list
                if (Helper.ModRegistry.IsLoaded("furyx639.ExpandedStorage") && ApiManager.HookIntoExpandedStorage(Helper))
                {
                    var expandedStorageApi = ApiManager.GetExpandedStorageApi();

                    // Add the Harvest Statue via Expanded Storage, so we can make use of their expanded chest options
                    expandedStorageApi.LoadContentPack(Path.Combine(Helper.DirectoryPath, "assets", "[JA] Island Gatherers Pack"));
                }
                else
                {
                    // Load in our assets
                    jsonAssetsApi.LoadAssets(Path.Combine(Helper.DirectoryPath, "assets", "[JA] Island Gatherers Pack"));
                }
            }
        }
    }
}
