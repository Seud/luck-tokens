using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class TravelingMerchantToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Traveling Merchant Token");
            Tooltip.SetDefault("Who said money was worthless ?\nUsed to craft Traveling Merchant items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 1000;
            item.rare = 2;
        }
    }
}
