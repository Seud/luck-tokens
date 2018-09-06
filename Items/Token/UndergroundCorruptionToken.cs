using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundCorruptionToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Corruption Token");
            Tooltip.SetDefault("Found within a wretched, cursed place\nUsed to craft items");
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
