using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier1Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 1 Token");
            Tooltip.SetDefault("Created from the potential of a new world\nUsed to craft items");
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
