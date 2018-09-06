using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundSnowToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Snow Token");
            Tooltip.SetDefault("Found in a perfectly crystallized place\nUsed to craft items");
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
