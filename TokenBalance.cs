using System;

namespace TokenMod
{
    public enum Rarity
    {
        None,
        VeryCommon, Common, Uncommon, Rare, VeryRare,
        VeryCommonObject, CommonObject, UncommonObject, RareObject, VeryRareObject,
        Chest, RareChest, Furniture, RareFurniture, Dye,
        Boss, Fishing, QuestReward, TravelingMerchant, Redeem
    };

    public class TokenBalance
    {

        /*
         * Balance values
         */

        // Base token and luck droprate for an enemy worth 1c
        public const double DROP_BASE_RATE = 0.01;

        // Determines whether enemy value influences luck/tokens drop rates (True) or drop quantity (False). Does not affect averages, bosses or fixed sources.
        public const bool VALUE_INFLUENCES_DROPRATE = false;

        // Value is raised to this power before multiplying droprate
        public const double DROP_VALUE_POWER = 0.5;

        // How much luck should drop on average at once (Not counting value)
        public const double DROP_AVERAGE_LUCK = 10;

        // Inverse of chance to grant more luck. Does not affect average.
        public const int JACKPOT_RARITY = 20;

        // Raises the final luck cost to this power. Reduces cost difference between rarer items.
        public const double COST_POWER = 0.9;

        // Cost multiplier for tokens
        public const double COST_TOKEN_MULTIPLIER = 0.1;

        // Chance that a fishing catch is replaced by a token
        public const double FISHING_TOKEN_CHANCE = 0.1;

        // Efficiency of recycling Tier tokens to luck
        public const double RECYCLING_EFFICIENCY = 0.2;

        // Value of an Eternia Token
        public const float ETERNIA_VALUE = 10000f;

        // Value of a friendly NPC
        public const float NPC_VALUE = 4f;

        // Rarity Multipliers
        public const double RARITY_VC = 0.2;
        public const double RARITY_C = 1;
        public const double RARITY_U = 3;
        public const double RARITY_R = 8;
        public const double RARITY_VR = 20;

        public const double RARITY_NPC = 1;
        public const double RARITY_OBJ = 5;

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
                case Rarity.VeryCommon: return RARITY_VC * RARITY_NPC;
                case Rarity.Common: return RARITY_C * RARITY_NPC;
                case Rarity.Uncommon: return RARITY_U * RARITY_NPC;
                case Rarity.Rare: return RARITY_R * RARITY_NPC;
                case Rarity.VeryRare: return RARITY_VR * RARITY_NPC;

                case Rarity.VeryCommonObject: return RARITY_VC * RARITY_OBJ;
                case Rarity.CommonObject: return RARITY_C * RARITY_OBJ;
                case Rarity.UncommonObject: return RARITY_U * RARITY_OBJ;
                case Rarity.RareObject: return RARITY_R * RARITY_OBJ;
                case Rarity.VeryRareObject: return RARITY_VR * RARITY_OBJ;

                case Rarity.Chest: return RARITY_U * RARITY_OBJ;
                case Rarity.RareChest: return RARITY_R * RARITY_OBJ;
                case Rarity.Furniture: return RARITY_C * RARITY_OBJ;
                case Rarity.RareFurniture: return RARITY_U * RARITY_OBJ;
                case Rarity.Dye: return RARITY_U * RARITY_OBJ;

                case Rarity.Boss: return RARITY_BOSS;
                case Rarity.Fishing: return RARITY_FISH;
                case Rarity.QuestReward: return RARITY_QUEST;
                case Rarity.TravelingMerchant: return RARITY_TM;
                case Rarity.Redeem: return RARITY_REDEEM;

                case Rarity.None: return 1;
                default: return 1;
            }
        }

        /*
         * Retrieves cost modifier for a given Tier
         * Roughly equal to the value in silver of an enemy of the matching tier
         */
        public static double GetTierModifier(int tier)
        {
            switch (tier)
            {
                case 0: return 0.5;
                case 1: return 1;
                case 2: return 2.5;
                case 3: return 4;
                case 4: return 7;
                case 5: return 10;
                case 6: return 12;
                case 7: return 15;
                default: return 1;
            }
        }

        /*
         * Calculates base cost of an item
         */
        public static double CalculateBaseCost(double luckCost, Rarity rarity, int tier)
        {
            // Calculate Rarity and Tier modifiers
            double costMod = GetRareModifier(rarity) * Math.Pow(GetTierModifier(tier), DROP_VALUE_POWER);

            double totalLuckCost = Math.Max(1, luckCost);
            totalLuckCost = Math.Pow(totalLuckCost * costMod, COST_POWER);

            return totalLuckCost;
        }

        /*
         * Generates a random luck value
         */
        public static int GetLuckAmount()
        {
            int luck = (int) Math.Floor(DROP_AVERAGE_LUCK * 0.5);
            int r = TokenUtils.random.Next(JACKPOT_RARITY);
            if (r == 0) luck *= JACKPOT_RARITY + 1;
            return luck;
        }
    }
}
