using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod.Items.Essence
{
    public class Tier0Essence : ModItem
    {
        protected virtual Color VColor { get { return Color.Gray; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Essence");
            Tooltip.SetDefault("Faded essence that has lost its power\nYou should not have this");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = vectorItemToPlayer.SafeNormalize(default(Vector2)) * 1f;
            item.velocity = item.velocity + movement;
            item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
            return true;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 4;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, VColor.ToVector3() * 0.50f * Main.essScale);
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.maxStack = 9999;
            item.value = 5;
            item.rare = 1;
        }
    }
}
