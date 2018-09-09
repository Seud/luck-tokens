using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod.Items.Special
{
    public class ShadowLockedToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Locked Token");
            Tooltip.SetDefault("A special token pulsing with dark energy\nRequired for crafting Shadow Chest items\nUse to unlock with a Shadow Key");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 3;
            item.color = Color.Purple;
        }

        public override bool CanUseItem(Player player)
        {
            return player.HasItem(ItemID.ShadowKey);
        }

        public override bool UseItem(Player player)
        {
            if(player.HasItem(ItemID.ShadowKey))
            {
                player.QuickSpawnItem(mod.ItemType<ShadowToken>());
                return true;
            }
            return false;
        }
    }
}
