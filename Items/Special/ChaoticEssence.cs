using Terraria;
using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod.Items.Special
{
    public class ChaoticEssence : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic Essence");
            Tooltip.SetDefault("Not yet calibrated to this world\nRight-click to align to the world");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 0;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            Item.NewItem(player.getRect(), TokenUtils.GetTierEssence(mod, TokenUtils.GetCurrentWorldTier()), 25);
        }
    }
}
