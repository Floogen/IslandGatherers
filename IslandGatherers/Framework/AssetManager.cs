using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IslandGatherers.Framework
{
    internal static class AssetManager
    {
        internal static string assetFolderPath;

        internal static void SetUpAssets(IModHelper helper)
        {
            assetFolderPath = helper.Content.GetActualAssetKey("assets", ContentSource.ModFolder);
        }

        internal static string SplitCamelCaseText(string input)
        {
            return string.Join(" ", Regex.Split(input, @"(?<!^)(?=[A-Z](?![A-Z]|$))"));
        }
    }
}
