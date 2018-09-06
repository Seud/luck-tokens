using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class WeatherToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Token");
            Tooltip.SetDefault("Found during harsh weather\nUsed to craft items");
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
