using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundJungleToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Jungle Token");
            Tooltip.SetDefault("Found in a place long lost to nature\nUsed to craft items");
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
