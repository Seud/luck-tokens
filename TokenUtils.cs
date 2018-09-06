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
        public static int GetTokenAmount(double tokenDropChance, int mult = 1)
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

            return (droppedTokens * mult);
        }

        /*
         * Calculates an amount of dropped luck or tokens depending on value
         */
        public static int GetValueAmount(float value, bool vid, bool luck = false)
        {
            double tokenDropChance = TokenBalance.DROP_BASE_RATE * (vid ? Math.Pow(value, TokenBalance.DROP_VALUE_POWER) : 1);
            int mult = ((luck) ? TokenBalance.GetLuckAmount() : 1) * (vid ? 1 : (int) Math.Floor(Math.Pow(value, TokenBalance.DROP_VALUE_POWER)));

            return GetTokenAmount(tokenDropChance, mult);
        }

        /*
         * Automatically drop tokens
         */
        public static void DropTokens(Mod mod, Player plr, float value, Rectangle rect, bool vid, int tokenOverride = 0, bool noLuck = false)
        {
            int tier = GetCurrentWorldTier();

            // Calculate quantities
            int droppedLuck = 0;

            if (!noLuck)
            {
                droppedLuck = GetValueAmount(value, vid, true);
            }

            int droppedLocationTokens = GetValueAmount(value, vid);
            int droppedTierTokens = GetValueAmount(value, vid);

            // Set token types
            int locationToken = GetLocationToken(mod, plr);
            int tierToken = mod.ItemType<Items.Token.Tier1Token>();

            switch (tier)
            {
                case 7:
                    tierToken = mod.ItemType<Items.Token.Tier7Token>();
                    break;
                case 6:
                    tierToken = mod.ItemType<Items.Token.Tier6Token>();
                    break;
                case 5:
                    tierToken = mod.ItemType<Items.Token.Tier5Token>();
                    break;
                case 4:
                    tierToken = mod.ItemType<Items.Token.Tier4Token>();
                    break;
                case 3:
                    tierToken = mod.ItemType<Items.Token.Tier3Token>();
                    break;
                case 2:
                    tierToken = mod.ItemType<Items.Token.Tier2Token>();
                    break;
                default:
                    tierToken = mod.ItemType<Items.Token.Tier1Token>();
                    break;
            }

            // Drop the items
            if (droppedLuck > 0) Item.NewItem(rect, mod.ItemType<Items.Essence.LuckEssence>(), droppedLuck);
            if (droppedLocationTokens > 0) Item.NewItem(rect, locationToken, droppedLocationTokens);
            if (droppedTierTokens > 0) Item.NewItem(rect, tierToken, droppedTierTokens);
        }
    }


}
