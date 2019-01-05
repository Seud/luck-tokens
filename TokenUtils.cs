using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TokenMod
{
    public class TokenUtils
    {
        public static Random random = new Random();

        /*
         * Get current world tier
         * Used to determine which Tier tokens drop
         */
        public static int GetCurrentWorldTier()
        {
            if (NPC.downedMoonlord) return 7;
            if (NPC.downedGolemBoss) return 6;
            if (NPC.downedPlantBoss) return 5;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) return 4;
            if (Main.hardMode) return 3;
            if (NPC.downedBoss2) return 2;
            return 1;
        }

        /*
         * Get essence corresponding to a certain tier
         */
        public static int GetTierEssence(Mod mod, int tier)
        {
            switch (tier)
            {
                case 7: return mod.ItemType<Items.Essence.Tier7Essence>();
                case 6: return mod.ItemType<Items.Essence.Tier6Essence>();
                case 5: return mod.ItemType<Items.Essence.Tier5Essence>();
                case 4: return mod.ItemType<Items.Essence.Tier4Essence>();
                case 3: return mod.ItemType<Items.Essence.Tier3Essence>();
                case 2: return mod.ItemType<Items.Essence.Tier2Essence>();
                default: return mod.ItemType<Items.Essence.Tier1Essence>();
            }
        }

        /*
         * Make a list of eligible tokens depending on biome and events
         * At the end, randomly pick one
         */
        public static List<int> GetLocationTokens(Mod mod, Player plr)
        {
            List<int> eligibleTokens = new List<int>();

            bool isUnderground = plr.ZoneDirtLayerHeight || plr.ZoneRockLayerHeight;
            bool isDesert = (plr.ZoneDesert || plr.ZoneUndergroundDesert);

            // Determine biome
            if(plr.ZoneOverworldHeight)
            {
                if (isDesert) eligibleTokens.Add(mod.ItemType<Items.Token.DesertToken>());
                if (plr.ZoneCorrupt) eligibleTokens.Add(mod.ItemType<Items.Token.CorruptionToken>());
                if (plr.ZoneCrimson) eligibleTokens.Add(mod.ItemType<Items.Token.CrimsonToken>());
                if (plr.ZoneHoly) eligibleTokens.Add(mod.ItemType<Items.Token.HallowToken>());
                if (plr.ZoneJungle) eligibleTokens.Add(mod.ItemType<Items.Token.JungleToken>());
                if (plr.ZoneSnow) eligibleTokens.Add(mod.ItemType<Items.Token.SnowToken>());
                if (plr.ZoneBeach) eligibleTokens.Add(mod.ItemType<Items.Token.OceanToken>());

                if (eligibleTokens.Count == 0) eligibleTokens.Add(mod.ItemType<Items.Token.ForestToken>());

                if (Main.eclipse || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon || Main.invasionType > 0) eligibleTokens.Add(mod.ItemType<Items.Token.InvasionToken>());
                if (Main.raining || Main.slimeRain || plr.ZoneSandstorm) eligibleTokens.Add(mod.ItemType<Items.Token.WeatherToken>());
            }

            if(isUnderground)
            {
                if (isDesert) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundDesertToken>());
                if (plr.ZoneCorrupt) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundCorruptionToken>());
                if (plr.ZoneCrimson) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundCrimsonToken>());
                if (plr.ZoneDungeon) eligibleTokens.Add(mod.ItemType<Items.Token.DungeonToken>());
                if (plr.ZoneHoly) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundHallowToken>());
                if (plr.ZoneJungle) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundJungleToken>());
                if (plr.ZoneSnow) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundSnowToken>());

                eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundToken>());
            }

            if (plr.ZoneSkyHeight) eligibleTokens.Add(mod.ItemType<Items.Token.SpaceToken>());
            if (plr.ZoneUnderworldHeight) eligibleTokens.Add(mod.ItemType<Items.Token.UnderworldToken>());

            return eligibleTokens;
        }

        /*
         * Calculate an amount of dropped tokens based on a chance
         */
        public static int GetTokenAmount(double tokenDropChance)
        {
            int droppedTokens = 0;

            // If chance > 100%, guarantee some tokens
            if (tokenDropChance >= 1)
            {
                droppedTokens = (int)tokenDropChance;
                tokenDropChance -= droppedTokens;
            }
            // Check for rest of chance
            if (random.NextDouble() < tokenDropChance)
            {
                droppedTokens++;
            }

            return droppedTokens;
        }

        /*
         * Automatically drop tokens and essence
         */
        public static void DropTokens(Mod mod, Player plr, float value, NPC npc, bool vic, double dropMult, bool dropEssence, List<int> tokens, bool instancedDrop = false)
        {
            int tier = GetCurrentWorldTier();

            // Calculate quantities
            int dropQuantity = GetDropAmount(value, tier, vic, dropMult);
            if (dropQuantity == 0) return;

            int tierEssence = GetTierEssence(mod, tier);

            // Drop the items
            if (npc == null)
            {
                if (plr == null) return;
                if (dropEssence) plr.QuickSpawnItem(tierEssence, dropQuantity);
                foreach (int token in tokens) plr.QuickSpawnItem(token, dropQuantity);
                return;
            }

            if (instancedDrop)
            {
                if (dropEssence) npc.DropItemInstanced(npc.position, npc.Size, tierEssence, dropQuantity, true);
                foreach (int token in tokens) npc.DropItemInstanced(npc.position, npc.Size, token, dropQuantity, true);
                return;
            } else {
                if (dropEssence) Item.NewItem(npc.getRect(), tierEssence, dropQuantity);
                foreach (int token in tokens) Item.NewItem(npc.getRect(), token, dropQuantity);
                return;
            }
        }

        /*
         * Calculates an amount of dropped essence or tokens depending on value
         */
        public static int GetDropAmount(float value, int tier, bool ignore_droprate, double dropMult)
        {
            // Check if we drop tokens
            if (!ignore_droprate)
                if (random.NextDouble() >= TokenBalance.DROP_BASE_RATE) return 0;

            return (int) Math.Ceiling(TokenBalance.GetValueMult(value, tier) * dropMult);
        }

        /*
         * Checks whether the NPC is a boss and manage its drops if needed
         */
        public static bool CheckBoss(Mod mod, NPC npc)
        {
            if (!TokenBalance.BOSS_DICT.ContainsKey(npc.type)) return false;

            float bossValue = TokenBalance.BOSS_DICT[npc.type];
            if (Main.expertMode) bossValue *= TokenBalance.BOSS_EXPERT_MULT;
            DropTokens(mod, null, bossValue, npc, true, 1, true, new List<int> { mod.ItemType<Items.Token.BossToken>() }, Main.expertMode);
            return true;
        }

        /*
         * Check if a condition is respected for crafting items
         */
        public static bool GetRecipeCondition(string condition)
        {
            switch(condition)
            {
                // Pre-Hardmode
                case "downedSlimeKing": return NPC.downedSlimeKing;
                case "downedBoss1": return NPC.downedBoss1;
                case "downedBoss2": return NPC.downedBoss2;
                case "downedQueenBee": return NPC.downedQueenBee;
                case "downedBoss3": return NPC.downedBoss3;

                // Hardmode
                case "hardMode": return Main.hardMode;
                case "downedMechBoss1": return NPC.downedMechBoss1;
                case "downedMechBoss2": return NPC.downedMechBoss2;
                case "downedMechBoss3": return NPC.downedMechBoss3;
                case "downedPlantBoss": return NPC.downedPlantBoss;
                case "downedGolemBoss": return NPC.downedGolemBoss;
                case "downedFishron": return NPC.downedFishron;
                case "downedAncientCultist": return NPC.downedAncientCultist;
                case "downedMoonlord": return NPC.downedMoonlord;

                // Invasions and events
                case "downedPirates": return NPC.downedPirates;
                case "downedMartians": return NPC.downedMartians;
                case "downedHalloweenTree": return NPC.downedHalloweenTree;
                case "downedHalloweenKing": return NPC.downedHalloweenKing;
                case "downedChristmasTree": return NPC.downedChristmasTree;
                case "downedChristmasSantank": return NPC.downedChristmasSantank;
                case "downedChristmasIceQueen": return NPC.downedChristmasIceQueen;
                case "DownedInvasionAnyDifficulty": return Terraria.GameContent.Events.DD2Event.DownedInvasionAnyDifficulty;

                default: return true;
            }
        }
    }


}
