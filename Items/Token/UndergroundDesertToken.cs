using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundDesertToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Desert Token");
            Tooltip.SetDefault("Found in a horrifying giant anthill\nUsed to craft items");
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
