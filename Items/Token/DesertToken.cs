using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class DesertToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Token");
            Tooltip.SetDefault("Found in a barren hot place\nUsed to craft items");
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
