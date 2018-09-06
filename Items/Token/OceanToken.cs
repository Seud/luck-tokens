using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class OceanToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Token");
            Tooltip.SetDefault("Found at the edge of the world\nUsed to craft items");
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
