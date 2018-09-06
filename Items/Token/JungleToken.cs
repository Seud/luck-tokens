using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class JungleToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Token");
            Tooltip.SetDefault("Found where vegetation is dense and the air even thicker\nUsed to craft items");
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
