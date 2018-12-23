using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod.Items.Special
{
    public class EterniaStone : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternia Stone");
            Tooltip.SetDefault("A foreign token stone\nRight-click to generate semi-random tokens");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 3;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            TokenUtils.DropTokens(mod, player, TokenBalance.ETERNIA_VALUE, player.getRect(), true, true, new List<int> { mod.ItemType<InvasionToken>(), mod.ItemType<BossToken>() }, true);
        }
    }
}
