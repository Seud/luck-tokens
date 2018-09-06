using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundHallowToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Hallow Token");
            Tooltip.SetDefault("Found in a bright, reality-defying place\nUsed to craft items");
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
