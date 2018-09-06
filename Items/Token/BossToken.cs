using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class BossToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boss Token");
            Tooltip.SetDefault("A special token radiating power\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = 3;
        }
    }
}
