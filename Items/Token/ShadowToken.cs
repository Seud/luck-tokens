using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class ShadowToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Token");
            Tooltip.SetDefault("A special token pulsing with dark energy\nRequired for crafting Shadow Chest items");
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
