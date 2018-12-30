using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TokenMod.Items.Token;

namespace TokenMod
{
    public class TokenPlayer : ModPlayer
    {
        public override void AnglerQuestReward(float quality, List<Item> rewardItems)
        {
            float fish_value = TokenBalance.BASE_FISH_VALUE + TokenBalance.GetTierValue(TokenUtils.GetCurrentWorldTier());
            TokenUtils.DropTokens(mod, player, fish_value, null, true, TokenBalance.QUEST_MULTIPLIER / quality, true, new List<int> { mod.ItemType<FishingToken>() });
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk) return;
            float fish_value = (TokenBalance.BASE_FISH_VALUE + TokenBalance.GetTierValue(TokenUtils.GetCurrentWorldTier()));
            TokenUtils.DropTokens(mod, player, fish_value, null, false, 1 + power / 100f, true, new List<int> { mod.ItemType<FishingToken>() });
        }
    }
}