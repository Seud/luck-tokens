using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier7Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.White; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 7 Essence");
            Tooltip.SetDefault("Created from the power of the moon itself\nUsed to craft items");
        }
    }
}