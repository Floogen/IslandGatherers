using System;

namespace IslandGatherers.Framework.Interfaces
{
    public interface IJsonAssetsApi
    {
        void LoadAssets(string path);

        event EventHandler IdsAssigned;

        int GetBigCraftableId(string name);
    }
}
