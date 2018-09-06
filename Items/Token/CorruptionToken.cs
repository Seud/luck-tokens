using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class CorruptionToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Token");
            Tooltip.SetDefault("Found in a land of corruption and desolation\nUsed to craft items");
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
