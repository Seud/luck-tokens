using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier5Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 5 Token");
            Tooltip.SetDefault("Created from the wisdom of a natural defender\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 400;
            item.rare = 7;
        }
    }
}
