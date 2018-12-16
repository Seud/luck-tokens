using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        public static int GetLocationToken(Mod mod, Player plr)
        {
            List<int> eligibleTokens = new List<int>();

            bool isUnderground = plr.ZoneDirtLayerHeight || plr.ZoneRockLayerHeight;
            bool isDesert = (plr.ZoneDesert || plr.ZoneUndergroundDesert);

            // Determine biome
            if (isDesert) eligibleTokens.Add(mod.ItemType<Items.Token.DesertToken>());
            if (plr.ZoneCorrupt) eligibleTokens.Add(mod.ItemType<Items.Token.CorruptionToken>());
            if (plr.ZoneCrimson) eligibleTokens.Add(mod.ItemType<Items.Token.CrimsonToken>());
            if (plr.ZoneHoly) eligibleTokens.Add(mod.ItemType<Items.Token.HallowToken>());
            if (plr.ZoneJungle) eligibleTokens.Add(mod.ItemType<Items.Token.JungleToken>());
            if (plr.ZoneSnow) eligibleTokens.Add(mod.ItemType<Items.Token.SnowToken>());
            if (plr.ZoneBeach) eligibleTokens.Add(mod.ItemType<Items.Token.OceanToken>());
            if (plr.ZoneSkyHeight) eligibleTokens.Add(mod.ItemType<Items.Token.SpaceToken>());
            if (plr.ZoneUnderworldHeight) eligibleTokens.Add(mod.ItemType<Items.Token.UnderworldToken>());

            if (isUnderground && isDesert) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundDesertToken>());
            if (isUnderground && plr.ZoneCorrupt) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundCorruptionToken>());
            if (isUnderground && plr.ZoneCrimson) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundCrimsonToken>());
            if (isUnderground && plr.ZoneDungeon) eligibleTokens.Add(mod.ItemType<Items.Token.DungeonToken>());
            if (isUnderground && plr.ZoneHoly) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundHallowToken>());
            if (isUnderground && plr.ZoneJungle) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundJungleToken>());
            if (isUnderground && plr.ZoneSnow) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundSnowToken>());
            if (isUnderground) eligibleTokens.Add(mod.ItemType<Items.Token.UndergroundToken>());

            if (eligibleTokens.Count == 0) eligibleTokens.Add(mod.ItemType<Items.Token.ForestToken>());

            // Determine additional conditions
            if (Main.eclipse || Main.bloodMoon || Main.pumpkinMoon || Main.snowMoon || Main.invasionType > 0) eligibleTokens.Add(mod.ItemType<Items.Token.InvasionToken>());
            if (Main.raining || Main.slimeRain || plr.ZoneSandstorm) eligibleTokens.Add(mod.ItemType<Items.Token.WeatherToken>());

            return eligibleTokens[random.Next(eligibleTokens.Count)];
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
         * tokenOverride guarantees a certain token if set to a positive number, or does not generate any token if negative
         */
        public static void DropTokens(Mod mod, Player plr, float value, Rectangle rect, bool vic, bool dropEssence, int tokenOverride = 0, bool playerSpawn = false)
        {
            int tier = GetCurrentWorldTier();

            // Calculate quantities
            int dropQuantity = GetDropAmount(value, tier, vic);
            if (dropQuantity == 0)
            {
                if (vic && value > 0) dropQuantity = 1; else return;
            }

            // Set token types
            int locationToken = (tokenOverride <= 0) ? GetLocationToken(mod, plr) : tokenOverride;
            int tierEssence = GetTierEssence(mod, tier);

            // Drop the items
            if (playerSpawn)
            {
                if (dropEssence) plr.QuickSpawnItem(tierEssence, dropQuantity);
                if (tokenOverride >= 0) plr.QuickSpawnItem(locationToken, dropQuantity);
            } else
            {
                if (dropEssence) Item.NewItem(rect, tierEssence, dropQuantity);
                if (tokenOverride >= 0) Item.NewItem(rect, locationToken, dropQuantity);
            }
        }

        /*
         * Calculates an amount of dropped essence or tokens depending on value
         * vic determines whether the value affects drop chance or drop quantity (Average is the same)
         */
        public static int GetDropAmount(float value, int tier, bool vic)
        {
            double valueMult = TokenBalance.GetValueMult(value, tier);

            double tokenDropChance = TokenBalance.DROP_BASE_RATE * (vic ? valueMult : 1);
            int mult = vic ? 1 : (int) Math.Ceiling(valueMult);

            return GetTokenAmount(tokenDropChance) * mult;
        }
    }


}
