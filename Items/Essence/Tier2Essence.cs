using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier2Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.Blue; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 2 Essence");
            Tooltip.SetDefault("Created from the corruption of an ancient guardian\nUsed to craft items");
        }
    }
}