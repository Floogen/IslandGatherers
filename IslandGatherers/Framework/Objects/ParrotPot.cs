using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGatherers.Framework.Objects
{
    internal class ParrotPot : Chest
    {
        public ParrotPot()
        {

        }

        public ParrotPot(Vector2 position, int itemID, bool enableHarvestMessage = true, bool doJunimosEatExcessCrops = true, bool doJunimosHarvestFromPots = true, bool doJunimosHarvestFromFruitTrees = true, bool doJunimosHarvestFromFlowers = true, bool doJunimosSowSeedsAfterHarvest = false, int minimumFruitOnTreeBeforeHarvest = 3) : base(true, position, itemID)
        {
            this.Name = "Parrot Pot";

            base.type.Value = "Crafting";
            base.bigCraftable.Value = true;
            base.canBeSetDown.Value = true;

            // Setting SpecialChestType to -1 so we can bypass Automate's default chest logic
            // TODO: Make this only happen if Automate is installed
            this.SpecialChestType = (SpecialChestTypes)(-1);
        }

        public override bool placementAction(GameLocation location, int x, int y, Farmer who = null)
        {
            base.tileLocation.Value = new Vector2(x / 64, y / 64);
            return true;
        }

        public override void draw(SpriteBatch spriteBatch, int x, int y, float alpha = 1f)
        {
            float draw_x = x;
            float draw_y = y;
            if (this.localKickStartTile.HasValue)
            {
                draw_x = Utility.Lerp(this.localKickStartTile.Value.X, draw_x, this.kickProgress);
                draw_y = Utility.Lerp(this.localKickStartTile.Value.Y, draw_y, this.kickProgress);
            }
            float base_sort_order = System.Math.Max(0f, ((draw_y + 1f) * 64f - 24f) / 10000f) + draw_x * 1E-05f;
            if (this.localKickStartTile.HasValue)
            {
                spriteBatch.Draw(Game1.shadowTexture, Game1.GlobalToLocal(Game1.viewport, new Vector2((draw_x + 0.5f) * 64f, (draw_y + 0.5f) * 64f)), Game1.shadowTexture.Bounds, Color.Black * 0.5f, 0f, new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y), 4f, SpriteEffects.None, 0.0001f);
                draw_y -= (float)System.Math.Sin((double)this.kickProgress * System.Math.PI) * 0.5f;
            }

            // Show a "filled" sprite or not, based on if the Harvest Statues has items
            spriteBatch.Draw(Game1.bigCraftableSpriteSheet, Game1.GlobalToLocal(Game1.viewport, new Vector2(draw_x * 64f + (float)((base.shakeTimer > 0) ? Game1.random.Next(-1, 2) : 0), (draw_y - 1f) * 64f)), Game1.getSourceRectForStandardTileSheet(Game1.bigCraftableSpriteSheet, this.items.Any() ? this.ParentSheetIndex + 1 : this.ParentSheetIndex, 16, 32), this.tint.Value * alpha, 0f, Vector2.Zero, 4f, SpriteEffects.None, base_sort_order);
        }
    }
}
