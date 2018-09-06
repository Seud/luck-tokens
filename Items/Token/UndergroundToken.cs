using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Token");
            Tooltip.SetDefault("Found deep under the surface\nUsed to craft items");
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
