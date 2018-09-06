using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class BaseToken : ModItem
    {
        public const int WIDTH = 30;
        public const int HEIGHT = 30;
        public const int MAX_STACK = 1000;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Base Token");
            Tooltip.SetDefault("A basic token to rule them all\nYou should not have this");
        }

        public override void SetDefaults()
        {
            item.width = WIDTH;
            item.height = HEIGHT;
            item.maxStack = MAX_STACK;
            item.value = 10;
            item.rare = 0;
        }
    }
}
