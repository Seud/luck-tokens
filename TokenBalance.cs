using System;
using System.Collections.Generic;
using Terraria.ID;

namespace TokenMod
{
    public enum Rarity
    {
        None,
        VeryCommon, Common, Uncommon, Rare, VeryRare, Boss,
        VeryCommonObject, CommonObject, UncommonObject, RareObject, VeryRareObject,
        Chest, RareChest, Dye,
        Fishing, FishingCrate, QuestReward,
        TravelingMerchant, Redeem
    };

    public class TokenBalance
    {

        /*
         * Balance values
         */

        // Base token and essence droprate
        public const double DROP_BASE_RATE = 0.02;

        // Global value multiplier
        public const float VALUE_MULTIPLIER = 0.1f;

        // Global value power for high value enemies
        public const double VALUE_POWER = 0.5;

        // Global multiplier for costs
        public const double GLOBAL_COST_MULTIPLIER = 0.1;

        // Base value multiplier of a boss (Compared to average enemy of tier)
        public const float BOSS_VALUE_MULT = 100f;

        // Base value of a fish
        public const float BASE_FISH_VALUE = 100f;

        // Drop multiplier of a quest
        public const int QUEST_MULTIPLIER = 50;

        // Value of an Eternia Token
        public const float ETERNIA_VALUE = 10000f;

        // Value of a friendly NPC
        public const float NPC_VALUE = 4f;

        // Rarity Multipliers
        public const double RARITY_VC = 0.2;
        public const double RARITY_C = 1;
        public const double RARITY_U = 5;
        public const double RARITY_R = 20;
        public const double RARITY_VR = 100;
        public const double RARITY_ER = 500;

        public const double RARITY_FISH = 2;
        public const double RARITY_FISH_RARE = 20;
        public const double RARITY_BOSS = 5;
        public const double RARITY_TM = 6;
        public const double RARITY_REDEEM = 1;

        // Boss value
        public static Dictionary<int, float> BOSS_DICT = GenerateBossDict();

        /*
         * Retrieves cost modifier for a given Rarity
         */
        public static double GetRareModifier(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.VeryCommon: return RARITY_VC;

                case Rarity.VeryCommonObject:
                case Rarity.Common: return RARITY_C;

                case Rarity.CommonObject:
                case Rarity.Uncommon: return RARITY_U;

                case Rarity.Chest:
                case Rarity.Dye:
                case Rarity.UncommonObject:
                case Rarity.Rare: return RARITY_R;

                case Rarity.RareChest:
                case Rarity.RareObject:
                case Rarity.VeryRare: return RARITY_VR;

                case Rarity.VeryRareObject: return RARITY_ER;
                    
                case Rarity.Boss: return RARITY_BOSS;
                case Rarity.Fishing: return RARITY_FISH;
                case Rarity.FishingCrate:
                case Rarity.QuestReward: return RARITY_FISH_RARE;
                case Rarity.TravelingMerchant: return RARITY_TM;

                case Rarity.Redeem: return 1;
                case Rarity.None: return 1;
                default: return 1;
            }
        }

        /*
         * Retrieves base value for a given Tier
         * Roughly equal to the value of an enemy of the matching tier, and increases as spawnrates rise
         */
        public static float GetTierValue(int tier)
        {
            return 25f * (tier + 1) * (tier + 1);
        }

        /*
         * Calculates price or droprate multiplier based on value and tier
         * Linear until tierValue is reached, then becomes fractional to avoid ultimate drops on high value enemies like Mimics
         */
        public static double GetValueMult(float value, int tier, bool noScaling = false)
        {
            if (noScaling) return value;

            float tierValue = GetTierValue(tier);
            if(value <= tierValue)
            {
                return value * VALUE_MULTIPLIER;
            } else
            {
                double overValue = value / tierValue;
                overValue = Math.Pow(overValue, VALUE_POWER);
                return tierValue * overValue * VALUE_MULTIPLIER;
            }
        }

        /*
        * Calculates base cost of an item
        */
        public static int CalculateCost(double dropRate, Rarity rarity, int tier)
        {
            // Calculate Rarity and Tier modifiers
            double costMod = GetRareModifier(rarity) * GetValueMult(GetTierValue(tier), tier) * GLOBAL_COST_MULTIPLIER;

            return (int) Math.Round(Math.Max(0.1, dropRate) * costMod);
        }

        /*
         * Calculates the value of a boss based on its tier and relative value
         */
        public static float GetBossValue(int tier, float mult)
        {
            return mult * GetTierValue(tier) * BOSS_VALUE_MULT;
        }

        /*
         * Generates the dictionary of boss ID and base values
         */
        public static Dictionary<int, float> GenerateBossDict()
        {
            Dictionary<int, float> dict = new Dictionary<int, float>
            {

                // Pre-Hardmode
                { NPCID.KingSlime, GetBossValue(1, 0.4f) },
                { NPCID.EyeofCthulhu, GetBossValue(1, 1f) },
                { NPCID.EaterofWorldsHead, GetBossValue(2, 0.02f) },
                { NPCID.EaterofWorldsBody, GetBossValue(2, 0.02f) },
                { NPCID.EaterofWorldsTail, GetBossValue(2, 0.02f) },
                { NPCID.Creeper, GetBossValue(2, 0.02f) },
                { NPCID.BrainofCthulhu, GetBossValue(2, 0.4f) },
                { NPCID.QueenBee, GetBossValue(2, 2f) },
                { NPCID.SkeletronHead, GetBossValue(2, 2f) },

                // Hardmode
                { NPCID.WallofFlesh, GetBossValue(3, 1f) },
                { NPCID.Retinazer, GetBossValue(4, 0.5f) },
                { NPCID.Spazmatism, GetBossValue(4, 0.5f) },
                { NPCID.TheDestroyer, GetBossValue(4, 1f) },
                { NPCID.SkeletronPrime, GetBossValue(4, 1f) },
                { NPCID.Plantera, GetBossValue(5, 1f) },
                { NPCID.Golem, GetBossValue(6, 1f) },
                { NPCID.DukeFishron, GetBossValue(6, 1.5f) },

                // Lunar event
                { NPCID.CultistBoss, GetBossValue(7, 0.1f) },
                { NPCID.LunarTowerSolar, GetBossValue(7, 0.1f) },
                { NPCID.LunarTowerVortex, GetBossValue(7, 0.1f) },
                { NPCID.LunarTowerNebula, GetBossValue(7, 0.1f) },
                { NPCID.LunarTowerStardust, GetBossValue(7, 0.1f) },
                { NPCID.MoonLordCore, GetBossValue(7, 1f) },

                // Invasions
                { NPCID.PirateShip, GetBossValue(3, 0.25f) },
                { NPCID.MourningWood, GetBossValue(5, 0.05f) },
                { NPCID.Pumpking, GetBossValue(5, 0.25f) },
                { NPCID.Everscream, GetBossValue(5, 0.05f) },
                { NPCID.SantaNK1, GetBossValue(5, 0.1f) },
                { NPCID.IceQueen, GetBossValue(5, 0.25f) },
                { NPCID.MartianSaucerCore, GetBossValue(6, 0.25f) }
            };

            return dict;
        }
    }
}
