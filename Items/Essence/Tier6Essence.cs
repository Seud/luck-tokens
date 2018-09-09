using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier6Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.Yellow; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 6 Essence");
            Tooltip.SetDefault("Created from the spirit of a mystical construct\nUsed to craft items");
        }
    }
}