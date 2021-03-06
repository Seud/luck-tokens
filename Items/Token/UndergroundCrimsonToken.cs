﻿using Terraria.ModLoader;

namespace TokenMod.Items.Token
{
    public class UndergroundCrimsonToken : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Underground Crimson Token");
            Tooltip.SetDefault("Found within a half-organic infestation\nUsed to craft items");
        }

        public override void SetDefaults()
        {
            item.width = BaseToken.WIDTH;
            item.height = BaseToken.HEIGHT;
            item.maxStack = BaseToken.MAX_STACK;
            item.value = 100;
            item.rare = 1;
        }
    }
}
