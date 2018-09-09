using Microsoft.Xna.Framework;

namespace TokenMod.Items.Essence
{
    public class Tier1Essence : Tier0Essence
    {

        protected override Color VColor { get { return Color.LightCyan; } }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tier 1 Essence");
            Tooltip.SetDefault("Created from the potential of a new world\nUsed to craft items");
        }
    }
}