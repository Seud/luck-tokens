using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class CrimsonToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Token");
            Tooltip.SetDefault("Found in a ravaged land polluted by blood\nUsed to craft items");
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
