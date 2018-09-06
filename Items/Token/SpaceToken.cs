using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class SpaceToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Token");
            Tooltip.SetDefault("Found within the atmosphere\nUsed to craft items");
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
