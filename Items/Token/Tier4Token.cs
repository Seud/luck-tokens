using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier4Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 4 Token");
            Tooltip.SetDefault("Created from the power of technology\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 300;
            item.rare = 5;
        }
    }
}
