using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier3Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.Pink; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 3 Essence");
            Tooltip.SetDefault("Created from the duality of light and darkness\nUsed to craft items");
        }
    }
}