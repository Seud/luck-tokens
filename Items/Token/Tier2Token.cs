using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier2Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 2 Token");
            Tooltip.SetDefault("Created from the corruption of an ancient guardian\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 150;
            item.rare = 2;
        }
    }
}
