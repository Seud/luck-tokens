using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier6Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 6 Token");
            Tooltip.SetDefault("Created from the spirit of a mystical construct\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 500;
            item.rare = 8;
        }
    }
}
