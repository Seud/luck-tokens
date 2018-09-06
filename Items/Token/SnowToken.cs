using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class SnowToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Token");
            Tooltip.SetDefault("Found within freezing hills\nUsed to craft items");
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
