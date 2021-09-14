using Microsoft.Xna.Framework.Graphics;
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
        internal static Texture2D emptyStatue;
        internal static Texture2D filledStatue;

        internal static void SetUpAssets(IModHelper helper)
        {
            assetFolderPath = Path.Combine("assets", "Parrot Pot");
            emptyStatue = helper.Content.Load<Texture2D>(Path.Combine(assetFolderPath, "Sprites", "empty.png"));
            filledStatue = helper.Content.Load<Texture2D>(Path.Combine(assetFolderPath, "Sprites", "filled.png"));
        }

        internal static string SplitCamelCaseText(string input)
        {
            return string.Join(" ", Regex.Split(input, @"(?<!^)(?=[A-Z](?![A-Z]|$))"));
        }
    }
}
