using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class NPCToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NPC Token");
            Tooltip.SetDefault("You are a terrible person\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 0;
            item.rare = -1;
        }
    }
}
