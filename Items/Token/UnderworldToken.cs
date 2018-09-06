using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UnderworldToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underworld Token");
            Tooltip.SetDefault("Found in a literal hell\nUsed to craft items");
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
