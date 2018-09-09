using System;

namespace TokenMod
{
    public enum Rarity
    {
        None,
        VeryCommon, Common, Uncommon, Rare, VeryRare,
        VeryCommonObject, CommonObject, UncommonObject, RareObject, VeryRareObject,
        Chest, RareChest, Furniture, Dye,
        Boss, Fishing, QuestReward, TravelingMerchant, Redeem
    };

    public class TokenBalance
    {

        /*
         * Balance values
         */

        // Base token and essence droprate
        public const double DROP_BASE_RATE = 0.05;

        // Global value multiplier
        public const float VALUE_MULTIPLIER = 0.1f;

        // Global multiplier for costs
        public const double GLOBAL_COST_MULTIPLIER = 0.5;

        // Chance that a fishing catch is replaced by a token
        public const double FISHING_TOKEN_CHANCE = 0.1;

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
        public const double RARITY_QUEST = 10;
        public const double RARITY_BOSS = 5;
        public const double RARITY_TM = 6;
        public const double RARITY_REDEEM = 1;

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
                case Rarity.Furniture:
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
                case Rarity.QuestReward: return RARITY_QUEST;
                case Rarity.TravelingMerchant: return RARITY_TM;

                case Rarity.Redeem: return 1;
                case Rarity.None: return 1;
                default: return 1;
            }
        }

        /*
         * Retrieves value for a given Tier
         * Roughly equal to the value of an enemy of the matching tier
         */
        public static float GetTierValue(int tier)
        {
            switch (tier)
            {
                case 0: return 25f;
                case 1: return 100f;
                case 2: return 250f;
                case 3: return 400f;
                case 4: return 700f;
                case 5: return 1000f;
                case 6: return 1200f;
                case 7: return 1500f;
                default: return 100f;
            }
        }

        /*
         * Calculates price or droprate multiplier based on value and tier
         */
        public static double GetValueMult(float value, int tier)
        {
            double power = 1;
            switch(tier)
            {
                case 1:
                    power = 0.7;
                    break;
                case 2:
                    power = 0.7;
                    break;
                case 3:
                    power = 0.8;
                    break;
                case 4:
                    power = 0.8;
                    break;
                case 5:
                    power = 0.9;
                    break;
                case 6:
                    power = 0.9;
                    break;
                case 7:
                    power = 1;
                    break;
            }
            return Math.Pow(value * VALUE_MULTIPLIER, power);
        }

        /*
        * Calculates base cost of an item
        */
        public static int CalculateCost(double dropRate, Rarity rarity, int tier)
        {
            // Calculate Rarity and Tier modifiers
            double costMod = GetRareModifier(rarity) * GetValueMult(GetTierValue(tier), tier) * GLOBAL_COST_MULTIPLIER;

            return (int) Math.Ceiling(Math.Max(0.1, dropRate) * costMod);
        }
    }
}
