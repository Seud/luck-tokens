using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class Tier7Token : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tier 7 Token");
            Tooltip.SetDefault("Created from the power of the moon itself\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 600;
            item.rare = 9;
        }
    }
}
