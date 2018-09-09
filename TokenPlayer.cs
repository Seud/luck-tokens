using System;
using Terraria;
using Terraria.ModLoader;

namespace TokenMod
{
    public class TokenPlayer : ModPlayer
    {

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk) return;
            if (TokenUtils.random.NextDouble() < TokenBalance.FISHING_TOKEN_CHANCE)
            {
                caughtType = mod.ItemType<Items.Special.FishingTokenCrate>();
            }
        }
    }
}
