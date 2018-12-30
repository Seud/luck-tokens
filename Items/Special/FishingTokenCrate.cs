using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod.Items.Special
{
    public class FishingTokenCrate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishing Token Crate");
            Tooltip.SetDefault("This wet token seems to have a lid...\nRight-click to generate random fishing tokens");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 2;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            TokenUtils.DropTokens(mod, player, TokenBalance.BASE_FISH_VALUE + TokenBalance.GetTierValue(TokenUtils.GetCurrentWorldTier()), null, true, 10, true, new List<int> { mod.ItemType<FishingToken>() });
        }
    }
}
