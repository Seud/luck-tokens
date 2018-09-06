using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class FishingToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishing Token");
            Tooltip.SetDefault("A small piece that never dries\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 2;
        }
    }
}
