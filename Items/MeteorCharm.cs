using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod.Items
{
    public class MeteorCharm : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Charm");
            Tooltip.SetDefault("A small stone glowing with otherworldly light\nWill attract a Meteorite");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
            item.width = 26;
            item.height = 26;
            item.maxStack = 99;
            item.value = 0;
            item.rare = 2;
            item.color = Color.Orange;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            WorldGen.dropMeteor();
            return true;
        }
    }
}
