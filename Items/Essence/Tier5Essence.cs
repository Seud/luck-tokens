using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier5Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.LightGreen; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 5 Essence");
            Tooltip.SetDefault("Created from the wisdom of a natural defender\nUsed to craft items");
        }
    }
}