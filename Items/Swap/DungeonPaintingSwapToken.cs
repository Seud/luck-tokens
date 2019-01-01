using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod.Items.Swap
{
    public class DungeonPaintingSwapToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Painting Swap Token");
            Tooltip.SetDefault("Used to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 100;
            item.rare = 1;
        }
    }
}
