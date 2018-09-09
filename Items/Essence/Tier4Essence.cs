using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier4Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.Red; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 4 Essence");
            Tooltip.SetDefault("Created from the power of technology\nUsed to craft items");
        }
    }
}