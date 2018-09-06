using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier3Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 3 Token");
            Tooltip.SetDefault("Created from the duality of light and darkness\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 200;
            item.rare = 4;
        }
    }
}
