using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class InvasionToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invasion Token");
            Tooltip.SetDefault("Found during a siege of otherworldly creatures\nUsed to craft items");
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
