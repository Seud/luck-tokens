using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod
{
    public class TokenRecipes
    {
        public const int TOKEN_CRAFTING_STATION = TileID.Anvils;

        private Mod mod;

        private int bonusItemID = -1;
        private int bonusItemAmount = 1;
        private int swapItemID = -1;
        private int resultAmount = 1;

        public TokenRecipes(Mod mod)
        {
            this.mod = mod;
        }

        /*
         * Makes a raw recipe with essence and tokens
         * Can optionally use another item or generate multiple copies (Using class attributes)
         */
        public void MakeRawTokenRecipe(int result, int tier, int cost, string token1 = "", string token2 = "")
        {
            // Add a recipe using up to 4 different token types and a bonus item
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddTile(TOKEN_CRAFTING_STATION);

            if (swapItemID > 0) recipe.AddIngredient(swapItemID, 1);
            if (cost > 0)
            {
                if (tier >= 0) recipe.AddIngredient(TokenUtils.GetTierEssence(mod, tier), cost);
                if (!string.IsNullOrEmpty(token1)) recipe.AddIngredient(mod, token1 + "Token", cost);
                if (!string.IsNullOrEmpty(token2)) recipe.AddIngredient(mod, token2 + "Token", cost);
            }
            if (bonusItemID > 0) recipe.AddIngredient(bonusItemID, bonusItemAmount);

            recipe.SetResult(result, resultAmount);
            recipe.AddRecipe();
        }

        /*
         * Makes a recipe with tokens
         * Automatically calculates costs
         */
        public void MakeTokenRecipe(int result, double dropRate, Rarity rarity, int tier, string location = "", string location2 = "")
        {
            int totalCost = TokenBalance.CalculateCost(dropRate, rarity, tier);

            MakeRawTokenRecipe(result, tier, totalCost, location, location2);
        }

        /*
         * Make multiple recipes for equivalent results
         */
        public void MakeTokenRecipes(int[] results, int dropRate, Rarity rarity, int tier, string location = "", string location2 = "")
        {
            foreach (int result in results)
            {
                MakeTokenRecipe(result, dropRate, rarity, tier, location, location2);
            }
        }

        /*
        * Makes a recipe for swapping items at a cost
        */
        public void MakeSwapRecipe(int swapIn, int swapOut, int length, int tier, string swapCost = "")
        {
            int totalCost = TokenBalance.CalculateCost(length, Rarity.Redeem, tier);

            swapItemID = swapIn;
            MakeRawTokenRecipe(swapOut, tier, totalCost, swapCost);
            swapItemID = -1;
        }

        /*
         * Makes recipes for swapping items within a set at a cost
         * Can also deduce a recipe
         */
        public void MakeSwapRecipes(int[] swaps, Rarity rarity, int tier, string swapCost = "", bool addRecipe = false, double dropRate = 1)
        {
            for (int i = 0; i < swaps.Length; i++)
            {
                int oldID = bonusItemID;
                bonusItemID = -1;

                for (int j = 0; j < swaps.Length; j++)
                {
                    if (i != j) MakeSwapRecipe(swaps[i], swaps[j], swaps.Length, tier, swapCost);
                }

                bonusItemID = oldID;
                if (addRecipe) MakeTokenRecipe(swaps[i], swaps.Length * dropRate, rarity, tier, swapCost);
            }
        }

        /*
         * Makes recipes for a traveling merchant item
         * costIn10s must be equal to the cost of buying packSize items divided by 10 silver
         */
        public void MakeTMRecipes(int result, double chance, int tier, int costIn10s, int packSize = 1)
        {
            // Create standard recipe
            bonusItemID = mod.ItemType<Items.Token.TravelingMerchantToken>();
            bonusItemAmount = costIn10s;
            resultAmount = packSize;
            MakeTokenRecipe(result, chance, Rarity.TravelingMerchant, tier);

            // Create "duplication" recipe
            bonusItemID = result;
            bonusItemAmount = packSize;
            resultAmount *= 2;
            MakeRawTokenRecipe(result, -1, costIn10s, "TravelingMerchant");

            resultAmount = 1;
            bonusItemAmount = 1;
            bonusItemID = -1;
        }

        /*
         * Register all recipes
         */
        public void AddRecipes()
        {

            /*
             * Token manipulation
             */

            // Tier downgrade
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier6Essence>(), 7, 1);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier5Essence>(), 6, 1);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier4Essence>(), 5, 1);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier3Essence>(), 4, 1);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier2Essence>(), 3, 1);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier1Essence>(), 2, 1);
            resultAmount = 100;
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier6Essence>(), 7, 100);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier5Essence>(), 6, 100);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier4Essence>(), 5, 100);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier3Essence>(), 4, 100);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier2Essence>(), 3, 100);
            MakeRawTokenRecipe(mod.ItemType<Items.Essence.Tier1Essence>(), 2, 100);
            resultAmount = 1;

            /*
             * Mob drops
             */

            // Forest mobs
            MakeTokenRecipe(ItemID.SlimeStaff, 10000, Rarity.VeryCommon, 0);
            MakeTokenRecipe(ItemID.Shackle, 50, Rarity.Common, 0, "Forest");
            MakeTokenRecipe(ItemID.ZombieArm, 250, Rarity.Common, 0, "Forest");
            MakeTokenRecipe(ItemID.BlackLens, 100, Rarity.Common, 0, "Forest");
            MakeTokenRecipe(ItemID.RainHat, 20, Rarity.Common, 0, "Forest", "Weather");
            MakeTokenRecipe(ItemID.RainCoat, 20, Rarity.Common, 0, "Forest", "Weather");
            MakeTokenRecipe(ItemID.UmbrellaHat, 45, Rarity.Common, 0, "Forest", "Weather");
            
            MakeTokenRecipe(ItemID.TatteredCloth, 1, Rarity.Rare, 1, "Forest");
            resultAmount = 35;
            MakeTokenRecipe(ItemID.PinkGel, 1, Rarity.VeryRare, 1, "Forest");
            resultAmount = 1;

            MakeTokenRecipe(ItemID.MoonCharm, 60, Rarity.Common, 3, "Forest");
            MakeTokenRecipe(ItemID.AdhesiveBandage, 100, Rarity.Common, 3, "Forest");
            MakeTokenRecipe(ItemID.NimbusRod, 15, Rarity.Uncommon, 3, "Forest", "Weather");

            // Underground mobs
            MakeTokenRecipe(ItemID.Hook, 25, Rarity.VeryCommon, 1, "Underground");
            MakeTokenRecipe(ItemID.AncientIronHelmet, 100, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.AncientGoldHelmet, 200, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.BoneSword, 200, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.Skull, 500, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.Rally, 26, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.Compass, 50, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.RobotHat, 250, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.ChainKnife, 250, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.JellyfishNecklace, 100, Rarity.Common, 1, "Underground");
            MakeTokenRecipe(ItemID.WhoopieCushion, 50, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.MiningShirt, 40 / 2, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.MiningPants, 40 / 2, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.BonePickaxe, 50, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.NightVisionHelmet, 30, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.GladiatorHelmet, 60 / 3, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.GladiatorBreastplate, 60 / 3, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.GladiatorLeggings, 60 / 3, Rarity.Uncommon, 1, "Underground");
            MakeTokenRecipe(ItemID.WizardHat, 1, Rarity.VeryRare, 1, "Underground");
            MakeTokenRecipe(ItemID.MetalDetector, 2, Rarity.VeryRare, 1, "Underground");

            MakeTokenRecipe(ItemID.AdhesiveBandage, 100, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.ArmorPolish, 100, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.Bezoar, 100, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.TrifoldMap, 100, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.MagicQuiver, 80, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.Marrow, 200, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.BeamSword, 150, Rarity.Common, 3, "Underground");
            MakeTokenRecipe(ItemID.MedusaHead, 100, Rarity.Uncommon, 3, "Underground");
            MakeTokenRecipe(ItemID.PocketMirror, 100, Rarity.Uncommon, 3, "Underground");
            MakeTokenRecipe(ItemID.PoisonStaff, 40, Rarity.Uncommon, 3, "Underground");
            int[] mimicSwaps = { ItemID.DualHook, ItemID.MagicDagger, ItemID.TitanGlove, ItemID.PhilosophersStone, ItemID.CrossNecklace, ItemID.StarCloak };
            MakeSwapRecipes(mimicSwaps, Rarity.Rare, 3, "Underground", true);
            MakeTokenRecipe(ItemID.RuneHat, 1, Rarity.VeryRare, 3, "Underground");
            MakeTokenRecipe(ItemID.RuneRobe, 1, Rarity.VeryRare, 3, "Underground");

            // Jungle mobs
            MakeTokenRecipe(ItemID.RobotHat, 250, Rarity.Common, 1, "Jungle");
            MakeTokenRecipe(ItemID.ArchaeologistsHat, 1, Rarity.VeryRare, 1, "Jungle");

            MakeTokenRecipe(ItemID.AdhesiveBandage, 100, Rarity.Common, 3, "Jungle");
            MakeTokenRecipe(ItemID.TurtleShell, 17, Rarity.Uncommon, 3, "Jungle");

            MakeTokenRecipe(ItemID.Yelets, 200, Rarity.VeryCommon, 4, "Jungle");

            MakeTokenRecipe(ItemID.AncientCobaltHelmet, 300 / 3, Rarity.Common, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.AncientCobaltBreastplate, 300 / 3, Rarity.Common, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.AncientCobaltLeggings, 300 / 3, Rarity.Common, 2, "UndergroundJungle");

            MakeTokenRecipe(ItemID.Bezoar, 100, Rarity.Common, 3, "UndergroundJungle");
            MakeTokenRecipe(ItemID.Uzi, 100, Rarity.Common, 3, "UndergroundJungle");
            MakeTokenRecipe(ItemID.TatteredBeeWing, 150, Rarity.Common, 3, "UndergroundJungle");
            MakeTokenRecipe(ItemID.TurtleShell, 17, Rarity.Uncommon, 3, "UndergroundJungle");
            MakeTokenRecipe(ItemID.ButterflyDust, 2, Rarity.VeryRare, 3, "UndergroundJungle");

            MakeTokenRecipe(ItemID.Yelets, 200, Rarity.VeryCommon, 4, "UndergroundJungle");

            MakeTokenRecipe(ItemID.LihzahrdPowerCell, 50, Rarity.Common, 5, "UndergroundJungle");
            MakeTokenRecipe(ItemID.LizardEgg, 1000, Rarity.Common, 5, "UndergroundJungle");

            // Desert mobs
            MakeTokenRecipe(ItemID.AntlionClaw, 50, Rarity.Common, 1, "Desert", "Weather");

            MakeTokenRecipe(ItemID.MummyMask, 75 / 3, Rarity.Common, 3, "Desert");
            MakeTokenRecipe(ItemID.MummyShirt, 75 / 3, Rarity.Common, 3, "Desert");
            MakeTokenRecipe(ItemID.MummyPants, 75 / 3, Rarity.Common, 3, "Desert");
            MakeTokenRecipe(ItemID.FastClock, 100, Rarity.Common, 3, "Desert");
            MakeTokenRecipe(ItemID.AncientBattleArmorMaterial, 1, Rarity.Rare, 3, "Desert", "Weather");

            MakeTokenRecipe(ItemID.DarkShard, 10, Rarity.Common, 3, "Corruption", "Desert");
            MakeTokenRecipe(ItemID.Megaphone, 100, Rarity.Common, 3, "Corruption", "Desert");

            MakeTokenRecipe(ItemID.DarkShard, 10, Rarity.Common, 3, "Crimson", "Desert");
            MakeTokenRecipe(ItemID.Megaphone, 100, Rarity.Common, 3, "Crimson", "Desert");

            MakeTokenRecipe(ItemID.LightShard, 10, Rarity.Common, 3, "Hallow", "Desert");
            MakeTokenRecipe(ItemID.TrifoldMap, 100, Rarity.Common, 3, "Hallow", "Desert");

            MakeTokenRecipe(ItemID.AntlionClaw, 50, Rarity.Common, 1, "UndergroundDesert");

            MakeTokenRecipe(ItemID.AncientCloth, 15 / 2, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.AncientHorn, 50, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.DjinnsCurse, 30, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.DjinnLamp, 30, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.LamiaHat, 100 / 3, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.LamiaShirt, 100 / 3, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.LamiaPants, 100 / 3, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.SunMask, 25, Rarity.Common, 3, "UndergroundDesert");
            MakeTokenRecipe(ItemID.MoonMask, 25, Rarity.Common, 3, "UndergroundDesert", "Corruption");
            MakeTokenRecipe(ItemID.MoonMask, 25, Rarity.Common, 3, "UndergroundDesert", "Crimson");

            // Snow mobs
            MakeTokenRecipe(ItemID.EskimoHood, 150 / 3, Rarity.Common, 1, "Snow");
            MakeTokenRecipe(ItemID.EskimoCoat, 150 / 3, Rarity.Common, 1, "Snow");
            MakeTokenRecipe(ItemID.EskimoPants, 150 / 3, Rarity.Common, 1, "Snow");

            MakeTokenRecipe(ItemID.Amarok, 300, Rarity.VeryCommon, 3, "Snow");

            MakeTokenRecipe(ItemID.FrostCore, 1, Rarity.VeryRare, 3, "Snow", "Weather");
            MakeTokenRecipe(ItemID.IceFeather, 3, Rarity.VeryRare, 3, "Snow", "Weather");

            MakeTokenRecipe(ItemID.VikingHelmet, 50, Rarity.Common, 1, "UndergroundSnow");
            MakeTokenRecipe(ItemID.SnowballLauncher, 150, Rarity.Common, 1, "UndergroundSnow");

            MakeTokenRecipe(ItemID.Amarok, 300, Rarity.VeryCommon, 3, "UndergroundSnow");
            MakeTokenRecipe(ItemID.FrostStaff, 50, Rarity.Common, 3, "UndergroundSnow");
            MakeTokenRecipe(ItemID.IceSickle, 180, Rarity.Common, 3, "UndergroundSnow");
            MakeTokenRecipe(ItemID.FrozenTurtleShell, 200, Rarity.Uncommon, 3, "UndergroundSnow");

            int[] iceMimicSwaps = { ItemID.Frostbrand, ItemID.IceBow, ItemID.FlowerofFrost };
            MakeSwapRecipes(iceMimicSwaps, Rarity.Rare, 3, "UndergroundSnow", true);
            MakeTokenRecipe(ItemID.ToySled, 20, Rarity.Rare, 3, "UndergroundSnow");
            

            // Corruption mobs
            MakeTokenRecipe(ItemID.AncientShadowHelmet, 525 / 3, Rarity.Common, 1, "Corruption");
            MakeTokenRecipe(ItemID.AncientShadowScalemail, 525 / 3, Rarity.Common, 1, "Corruption");
            MakeTokenRecipe(ItemID.AncientShadowGreaves, 525 / 3, Rarity.Common, 1, "Corruption");

            MakeTokenRecipe(ItemID.Blindfold, 100, Rarity.Common, 3, "Corruption");
            MakeTokenRecipe(ItemID.Vitamins, 100, Rarity.Common, 3, "Corruption");

            MakeTokenRecipe(ItemID.Blindfold, 100, Rarity.Common, 3, "UndergroundCorruption");
            MakeTokenRecipe(ItemID.Vitamins, 100, Rarity.Common, 3, "UndergroundCorruption");

            int[] corruptionMimicSwaps = { ItemID.DartRifle, ItemID.WormHook, ItemID.ChainGuillotines, ItemID.ClingerStaff, ItemID.PutridScent };
            MakeSwapRecipes(corruptionMimicSwaps, Rarity.VeryRare, 3, "UndergroundCorruption", true);
            MakeTokenRecipe(ItemID.Nazar, 100, Rarity.Uncommon, 3, "UndergroundCorruption");

            // Crimson mobs
            MakeTokenRecipe(ItemID.MeatGrinder, 200, Rarity.VeryCommon, 1, "Crimson");

            int[] crimsonMimicSwaps = { ItemID.SoulDrain, ItemID.DartPistol, ItemID.FetidBaghnakhs, ItemID.FleshKnuckles, ItemID.TendonHook };
            MakeSwapRecipes(crimsonMimicSwaps, Rarity.VeryRare, 3, "UndergroundCrimson", true);
            MakeTokenRecipe(ItemID.MeatGrinder, 200, Rarity.VeryCommon, 3, "UndergroundCrimson");
            MakeTokenRecipe(ItemID.Vitamins, 100, Rarity.Common, 3, "UndergroundCrimson");
            MakeTokenRecipe(ItemID.Nazar, 100, Rarity.Uncommon, 3, "UndergroundCrimson");

            // Hallow mobs
            MakeTokenRecipe(ItemID.UnicornonaStick, 100, Rarity.Common, 3, "Hallow");
            MakeTokenRecipe(ItemID.FastClock, 100, Rarity.Common, 3, "Hallow");
            MakeTokenRecipe(ItemID.Megaphone, 100, Rarity.Common, 3, "Hallow");
            MakeTokenRecipe(ItemID.BlessedApple, 200, Rarity.VeryCommon, 3, "Hallow");
            resultAmount = 30;
            MakeTokenRecipe(ItemID.RainbowBrick, 1, Rarity.VeryRare, 3, "Hallow", "Weather");
            resultAmount = 1;

            MakeTokenRecipe(ItemID.BlessedApple, 200, Rarity.VeryCommon, 3, "UndergroundHallow");
            MakeTokenRecipe(ItemID.RodofDiscord, 500, Rarity.Common, 3, "UndergroundHallow");
            int[] hallowMimicSwaps = { ItemID.DaedalusStormbow, ItemID.FlyingKnife, ItemID.CrystalVileShard, ItemID.IlluminantHook };
            MakeSwapRecipes(hallowMimicSwaps, Rarity.VeryRare, 3, "UndergroundHallow", true);
            MakeTokenRecipe(ItemID.Nazar, 100, Rarity.Uncommon, 3, "UndergroundHallow");

            // Dungeon mobs
            MakeTokenRecipe(ItemID.GoldenKey, 65, Rarity.VeryCommon, 2, "Dungeon");
            MakeTokenRecipe(ItemID.Nazar, 100, Rarity.Uncommon, 2, "Dungeon");
            MakeTokenRecipe(ItemID.TallyCounter, 100, Rarity.Common, 2, "Dungeon");
            MakeTokenRecipe(ItemID.BoneWand, 250, Rarity.Common, 2, "Dungeon");
            MakeTokenRecipe(ItemID.ClothierVoodooDoll, 300, Rarity.Common, 2, "Dungeon");
            MakeTokenRecipe(ItemID.AncientNecroHelmet, 450, Rarity.Common, 2, "Dungeon");

            MakeTokenRecipe(ItemID.Kraken, 400, Rarity.VeryCommon, 5, "Dungeon");
            MakeTokenRecipe(ItemID.Ectoplasm, 15, Rarity.Common, 5, "Dungeon");
            MakeTokenRecipe(ItemID.Keybrand, 200, Rarity.Common, 5, "Dungeon");
            MakeTokenRecipe(ItemID.MagnetSphere, 300, Rarity.Common, 5, "Dungeon");
            MakeTokenRecipe(ItemID.WispinaBottle, 400, Rarity.Common, 5, "Dungeon");
            MakeTokenRecipe(ItemID.BoneFeather, 450, Rarity.Common, 5, "Dungeon");
            MakeTokenRecipe(ItemID.ShadowbeamStaff, 20, Rarity.Rare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.SpectreStaff, 20, Rarity.Rare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.InfernoFork, 20, Rarity.Rare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.PaladinsHammer, 15, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.PaladinsShield, 10.5, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.BlackBelt, 12, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.Tabi, 12, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.TacticalShotgun, 13, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.RocketLauncher, 18, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.SniperRifle, 13, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.SniperScope, 12, Rarity.VeryRare, 5, "Dungeon");
            MakeTokenRecipe(ItemID.SWATHelmet, 12, Rarity.VeryRare, 5, "Dungeon");

            // Space mobs
            MakeTokenRecipe(ItemID.GiantHarpyFeather, 200, Rarity.Common, 1, "Space");

            // Ocean mobs
            MakeTokenRecipe(ItemID.JellyfishNecklace, 100, Rarity.Common, 1, "Ocean");
            MakeTokenRecipe(ItemID.DivingHelmet, 50, Rarity.Uncommon, 1, "Ocean");

            MakeTokenRecipe(ItemID.PirateMap, 100, Rarity.VeryCommon, 3, "Ocean");

            // Underworld mobs
            MakeTokenRecipe(ItemID.ObsidianRose, 500, Rarity.Common, 2, "Underworld");
            MakeTokenRecipe(ItemID.PlumbersHat, 500, Rarity.Common, 2, "Underworld");
            MakeTokenRecipe(ItemID.DemonScythe, 35, Rarity.Common, 2, "Underworld");
            MakeTokenRecipe(ItemID.MagmaStone, 150 / 2, Rarity.Common, 2, "Underworld");
            MakeTokenRecipe(ItemID.Cascade, 400, Rarity.VeryCommon, 2, "Underworld");
            MakeTokenRecipe(ItemID.GuideVoodooDoll, 1, Rarity.Rare, 2, "Underworld");

            resultAmount = 30;
            MakeTokenRecipe(ItemID.LivingFireBlock, 50, Rarity.VeryCommon, 3, "Underworld");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.HelFire, 400, Rarity.VeryCommon, 3, "Underworld");

            MakeTokenRecipe(ItemID.UnholyTrident, 30, Rarity.Common, 4, "Underworld");
            MakeTokenRecipe(ItemID.FireFeather, 75, Rarity.Common, 4, "Underworld");

            /*
             * Invasions and events
             */

            // Blood Moon
            MakeTokenRecipe(ItemID.SharkToothNecklace, 150, Rarity.Common, 0, "Invasion");
            MakeTokenRecipe(ItemID.MoneyTrough, 200, Rarity.Common, 0, "Invasion");
            MakeTokenRecipe(ItemID.BunnyHood, 75, Rarity.Uncommon, 0, "Invasion");
            MakeTokenRecipe(ItemID.TopHat, 1.1, Rarity.Rare, 0, "Invasion");
            MakeTokenRecipe(ItemID.TheBrideDress, 1.0 / 2, Rarity.Rare, 0, "Invasion");
            MakeTokenRecipe(ItemID.TheBrideHat, 1.0 / 2, Rarity.Rare, 0, "Invasion");

            MakeTokenRecipe(ItemID.PedguinHat, 50 / 3, Rarity.Uncommon, 1, "Invasion", "Snow");
            MakeTokenRecipe(ItemID.PedguinShirt, 50 / 3, Rarity.Uncommon, 1, "Invasion", "Snow");
            MakeTokenRecipe(ItemID.PedguinPants, 50 / 3, Rarity.Uncommon, 1, "Invasion", "Snow");

            MakeTokenRecipe(ItemID.KOCannon, 1000, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.TrifoldMap, 100, Rarity.Uncommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.Bananarang, 30 / 2.5, Rarity.Uncommon, 3, "Invasion");

            // Goblin Invasion
            MakeTokenRecipe(ItemID.Harpoon, 200, Rarity.VeryCommon, 1, "Invasion");
            int[] summonerSwaps = { ItemID.ShadowFlameHexDoll, ItemID.ShadowFlameBow, ItemID.ShadowFlameKnife };
            MakeSwapRecipes(summonerSwaps, Rarity.Rare, 3, "Invasion", true, 2);

            // Solar Eclipse
            MakeTokenRecipe(ItemID.NeptunesShell, 50, Rarity.Common, 4, "Invasion");
            MakeTokenRecipe(ItemID.MoonStone, 35, Rarity.Common, 4, "Invasion");
            MakeTokenRecipe(ItemID.BrokenBatWing, 40, Rarity.Common, 4, "Invasion");
            MakeTokenRecipe(ItemID.DeathSickle, 40, Rarity.Common, 4, "Invasion");
            MakeTokenRecipe(ItemID.EyeSpring, 15, Rarity.Uncommon, 4, "Invasion");
            MakeTokenRecipe(ItemID.BrokenHeroSword, 4, Rarity.Rare, 4, "Invasion");
            MakeTokenRecipe(ItemID.TheEyeOfCthulhu, 4, Rarity.Rare, 5, "Invasion");
            MakeTokenRecipe(ItemID.MothronWings, 20, Rarity.Rare, 5, "Invasion");
            MakeTokenRecipe(ItemID.ButchersChainsaw, 40, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.DeadlySphereStaff, 40, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.ToxicFlask, 40, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.NailGun, 25, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.PsychoKnife, 40, Rarity.Common, 5, "Invasion");

            // Pirate Invasion
            MakeTokenRecipe(ItemID.Cutlass, 200, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.GoldRing, 200, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.PirateStaff, 2000, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.DiscountCard, 2000, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.LuckyCoin, 4000, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.CoinGun, 8000, Rarity.VeryCommon, 3, "Invasion");
            int[] pirateFurniture = { ItemID.GoldenBathtub, ItemID.GoldenBed, ItemID.GoldenBookcase, ItemID.GoldenCandelabra, ItemID.GoldenCandle, ItemID.GoldenChair, ItemID.GoldenChest,
                ItemID.GoldenChandelier, ItemID.GoldenDoor, ItemID.GoldenDresser, ItemID.GoldenClock, ItemID.GoldenLamp, ItemID.GoldenLantern, ItemID.GoldenPiano, ItemID.GoldenPlatform, ItemID.GoldenSink,
                ItemID.GoldenSofa, ItemID.GoldenTable, ItemID.GoldenToilet, ItemID.GoldenWorkbench };
            MakeTokenRecipes(pirateFurniture, 300 / 20, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.EyePatch, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.SailorHat, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.SailorShirt, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.SailorPants, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.BuccaneerBandana, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.BuccaneerShirt, 500 / 3, Rarity.VeryCommon, 3, "Invasion");
            MakeTokenRecipe(ItemID.BuccaneerPants, 500 / 3, Rarity.VeryCommon, 3, "Invasion");

            // Pumpkin Moon
            resultAmount = 30;
            MakeTokenRecipe(ItemID.SpookyWood, 1, Rarity.Common, 5, "Invasion");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.ScarecrowHat, 60 / 3, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.ScarecrowShirt, 60 / 3, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.ScarecrowPants, 60 / 3, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.JackOLanternMask, 40, Rarity.Uncommon, 5, "Invasion");

            // Frost Moon
            MakeTokenRecipe(ItemID.ElfHat, 600 / 3, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.ElfShirt, 600 / 3, Rarity.Common, 5, "Invasion");
            MakeTokenRecipe(ItemID.ElfPants, 600 / 3, Rarity.Common, 5, "Invasion");

            // Martian Madness
            resultAmount = 15;
            MakeTokenRecipe(ItemID.MartianConduitPlating, 8, Rarity.VeryCommon, 6, "Invasion");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.MartianCostumeMask, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.MartianCostumeShirt, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.MartianCostumePants, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.MartianUniformHelmet, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.MartianUniformTorso, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.MartianUniformPants, 200 / 3, Rarity.VeryCommon, 6, "Invasion");
            MakeTokenRecipe(ItemID.BrainScrambler, 100, Rarity.Uncommon, 6, "Invasion");

            /*
             * Normal Boss items
             */

            // King Slime
            int[] kingSlimeSwaps1 = { ItemID.NinjaHood, ItemID.NinjaShirt, ItemID.NinjaPants };
            int[] kingSlimeSwaps2 = { ItemID.SlimeGun, ItemID.SlimeHook };
            MakeSwapRecipes(kingSlimeSwaps1, Rarity.Boss, 1, "Boss");
            MakeSwapRecipes(kingSlimeSwaps2, Rarity.Boss, 1, "Boss");
            MakeTokenRecipe(ItemID.SlimySaddle, 4, Rarity.Boss, 1, "Boss");
            MakeTokenRecipe(ItemID.KingSlimeMask, 7, Rarity.Boss, 1, "Boss");
            MakeTokenRecipe(ItemID.KingSlimeTrophy, 10, Rarity.Boss, 1, "Boss");

            // Eye of Cthulhu
            MakeTokenRecipe(ItemID.Binoculars, 40, Rarity.Boss, 1, "Boss");
            MakeTokenRecipe(ItemID.EyeMask, 7, Rarity.Boss, 1, "Boss");
            MakeTokenRecipe(ItemID.EyeofCthulhuTrophy, 10, Rarity.Boss, 1, "Boss");

            // Eater of Worlds
            MakeTokenRecipe(ItemID.EatersBone, 20, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.EaterMask, 7, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.EaterofWorldsTrophy, 10, Rarity.Boss, 2, "Boss");

            // Brain of Cthulhu
            MakeTokenRecipe(ItemID.BoneRattle, 20, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BrainMask, 7, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BrainofCthulhuTrophy, 10, Rarity.Boss, 2, "Boss");

            // Queen Bee
            int[] queenBeeSwaps1 = { ItemID.BeeGun, ItemID.BeeKeeper, ItemID.BeesKnees };
            int[] queenBeeSwaps2 = { ItemID.HiveWand, ItemID.BeeHat, ItemID.BeeShirt, ItemID.BeePants };
            MakeSwapRecipes(queenBeeSwaps1, Rarity.Boss, 2, "Boss");
            MakeSwapRecipes(queenBeeSwaps2, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.HoneyComb, 3, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.Nectar, 15, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.HoneyedGoggles, 20, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BeeMask, 7, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.QueenBeeTrophy, 10, Rarity.Boss, 2, "Boss");

            // Skeletron
            MakeTokenRecipe(ItemID.SkeletronHand, 8, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BookofSkulls, 10, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.SkeletronMask, 7, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.SkeletronTrophy, 10, Rarity.Boss, 2, "Boss");

            // Wall of Flesh
            int[] wallOfFleshSwaps = { ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.WarriorEmblem, ItemID.SorcererEmblem, ItemID.RangerEmblem, ItemID.SummonerEmblem };
            MakeSwapRecipes(wallOfFleshSwaps, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.FleshMask, 7, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.WallofFleshTrophy, 10, Rarity.Boss, 3, "Boss");

            // Mechanical bosses
            MakeTokenRecipe(ItemID.TwinMask, 7, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.RetinazerTrophy, 10, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.SpazmatismTrophy, 10, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.DestroyerMask, 7, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.DestroyerTrophy, 10, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.SkeletronPrimeMask, 7, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.SkeletronPrimeTrophy, 10, Rarity.Boss, 4, "Boss");

            // Plantera
            int[] planteraSwaps = { ItemID.GrenadeLauncher, ItemID.VenusMagnum, ItemID.NettleBurst, ItemID.LeafBlower, ItemID.FlowerPow, ItemID.WaspGun, ItemID.Seedler };
            MakeSwapRecipes(planteraSwaps, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.PygmyStaff, 4, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.Seedling, 20, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.TheAxe, 50, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.PlanteraMask, 7, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.PlanteraTrophy, 10, Rarity.Boss, 5, "Boss");

            // Golem
            int[] golemSwaps = { ItemID.Stynger, ItemID.PossessedHatchet, ItemID.SunStone, ItemID.EyeoftheGolem, ItemID.Picksaw, ItemID.HeatRay, ItemID.StaffofEarth, ItemID.GolemFist };
            MakeSwapRecipes(golemSwaps, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.GolemMask, 7, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.GolemTrophy, 10, Rarity.Boss, 6, "Boss");

            // Duke Fishron
            int[] dukeFishronSwaps = { ItemID.BubbleGun, ItemID.Flairon, ItemID.RazorbladeTyphoon, ItemID.TempestStaff, ItemID.Tsunami };
            MakeSwapRecipes(dukeFishronSwaps, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.FishronWings, 15, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.DukeFishronMask, 7, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.DukeFishronTrophy, 10, Rarity.Boss, 6, "Boss");

            // Lunatic Cultist
            MakeTokenRecipe(ItemID.BossMaskCultist, 7, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.AncientCultistTrophy, 10, Rarity.Boss, 6, "Boss");

            // Moon Lord
            int[] moonLordSwaps = { ItemID.Meowmere, ItemID.Terrarian, ItemID.StarWrath, ItemID.SDMG, ItemID.FireworksLauncher, ItemID.LastPrism, ItemID.LunarFlareBook, ItemID.RainbowCrystalStaff,
            ItemID.MoonlordTurretStaff};
            MakeSwapRecipes(moonLordSwaps, Rarity.Boss, 7, "Boss");
            MakeTokenRecipe(ItemID.MoonMask, 7, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.MoonLordTrophy, 10, Rarity.Boss, 2, "Boss");

            /*
             * Invasion Boss items
             */

            // Flying Dutchman
            MakeTokenRecipe(ItemID.Cutlass, 10, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.GoldRing, 10, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.PirateStaff, 100, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.DiscountCard, 100, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.LuckyCoin, 200, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.CoinGun, 400, Rarity.Boss, 3, "Boss");
            MakeTokenRecipe(ItemID.FlyingDutchmanTrophy, 10, Rarity.Boss, 3, "Boss");

            // Mourning Wood
            int[] mourningWoodSwaps = { ItemID.CursedSapling, ItemID.SpookyTwig, ItemID.SpookyHook, ItemID.NecromanticScroll, ItemID.StakeLauncher };
            MakeSwapRecipes(mourningWoodSwaps, Rarity.Boss, 5, "Boss", true, 2);
            MakeTokenRecipe(ItemID.MourningWoodTrophy, 10, Rarity.Boss, 5, "Boss");

            // Pumpking
            int[] pumpkingSwaps = { ItemID.TheHorsemansBlade, ItemID.BatScepter, ItemID.BlackFairyDust, ItemID.SpiderEgg, ItemID.RavenStaff, ItemID.CandyCornRifle, ItemID.JackOLanternLauncher };
            MakeSwapRecipes(pumpkingSwaps, Rarity.Boss, 5, "Boss", true, 2);
            MakeTokenRecipe(ItemID.PumpkingTrophy, 10, Rarity.Boss, 5, "Boss");

            // Everscream
            int[] everscreamSwaps = { ItemID.ChristmasTreeSword, ItemID.ChristmasHook, ItemID.Razorpine };
            MakeSwapRecipes(everscreamSwaps, Rarity.Boss, 5, "Boss", true, 8);
            MakeTokenRecipe(ItemID.FestiveWings, 120, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.EverscreamTrophy, 10, Rarity.Boss, 5, "Boss");

            // Santa NK-1
            int[] santaNK1Swaps = { ItemID.EldMelter, ItemID.ChainGun };
            MakeSwapRecipes(santaNK1Swaps, Rarity.Boss, 5, "Boss", true, 8);
            MakeTokenRecipe(ItemID.SantaNK1Trophy, 10, Rarity.Boss, 5, "Boss");

            // Ice Queen
            int[] iceQueenSwaps = { ItemID.SnowmanCannon, ItemID.NorthPole, ItemID.BlizzardStaff };
            MakeSwapRecipes(iceQueenSwaps, Rarity.Boss, 5, "Boss", true, 8);
            MakeTokenRecipe(ItemID.BabyGrinchMischiefWhistle, 120 / 2, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.ReindeerBells, 240 / 2, Rarity.Boss, 5, "Boss");
            MakeTokenRecipe(ItemID.IceQueenTrophy, 10, Rarity.Boss, 5, "Boss");

            // Martian Saucer
            int[] saucerSwaps = { ItemID.Xenopopper, ItemID.XenoStaff, ItemID.LaserMachinegun, ItemID.LaserDrill, ItemID.ElectrosphereLauncher, ItemID.ChargedBlasterCannon, ItemID.InfluxWaver,
                ItemID.CosmicCarKey, ItemID.AntiGravityHook };
            MakeSwapRecipes(saucerSwaps, Rarity.Boss, 6, "Boss", true);
            MakeTokenRecipe(ItemID.MartianSaucerTrophy, 10, Rarity.Boss, 6, "Boss");

            // Dark Mage
            MakeTokenRecipe(ItemID.WarTable, 10 / 2, Rarity.Boss, 2, "Boss");
            resultAmount = 4;
            MakeTokenRecipe(ItemID.WarTableBanner, 10 / 2, Rarity.Boss, 2, "Boss");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.DD2PetDragon, 6 / 2, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.DD2PetGato, 6 / 2, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BossMaskDarkMage, 7, Rarity.Boss, 2, "Boss");
            MakeTokenRecipe(ItemID.BossTrophyDarkmage, 10, Rarity.Boss, 2, "Boss");

            // Ogre
            int[] ogreSwaps1 = { ItemID.ApprenticeScarf, ItemID.SquireShield, ItemID.HuntressBuckler, ItemID.MonkBelt };
            int[] ogreSwaps2 = { ItemID.DD2PhoenixBow, ItemID.DD2SquireDemonSword, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.BookStaff };
            MakeSwapRecipes(ogreSwaps1, Rarity.Boss, 4, "Boss", true, 3);
            MakeSwapRecipes(ogreSwaps2, Rarity.Boss, 4, "Boss", true, 3);
            MakeTokenRecipe(ItemID.DD2PetGhost, 5, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.BossMaskOgre, 7, Rarity.Boss, 4, "Boss");
            MakeTokenRecipe(ItemID.BossTrophyOgre, 10, Rarity.Boss, 4, "Boss");

            // Betsy
            int[] betsySwaps = { ItemID.DD2SquireBetsySword, ItemID.ApprenticeStaffT3, ItemID.MonkStaffT3, ItemID.DD2BetsyBow };
            MakeSwapRecipes(betsySwaps, Rarity.Boss, 6, "Boss", true);
            MakeTokenRecipe(ItemID.BetsyWings, 4, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.BossMaskBetsy, 7, Rarity.Boss, 6, "Boss");
            MakeTokenRecipe(ItemID.BossTrophyBetsy, 10, Rarity.Boss, 6, "Boss");

            /*
             * NPC drops
             * You monster
             */

            MakeTokenRecipe(ItemID.GreenCap, 1, Rarity.Rare, 0, "NPC");
            MakeTokenRecipe(ItemID.DyeTradersScimitar, 8, Rarity.Common, 1, "NPC");
            MakeTokenRecipe(ItemID.AleThrowingGlove, 8, Rarity.Common, 2, "NPC");
            MakeTokenRecipe(ItemID.StylistKilLaKillScissorsIWish, 8, Rarity.Common, 1, "NPC");
            MakeTokenRecipe(ItemID.PainterPaintballGun, 10, Rarity.Common, 1, "NPC");
            resultAmount = 45;
            MakeTokenRecipe(ItemID.PartyGirlGrenade, 4, Rarity.Uncommon, 1, "NPC");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.TaxCollectorsStickOfDoom, 8, Rarity.Common, 3, "NPC");
            MakeTokenRecipe(ItemID.PeddlersHat, 1, Rarity.TravelingMerchant, 1, "NPC");

            /*
             * Chest contents
             */

            // Surface Chests
            int[] surfaceChest = { ItemID.Spear, ItemID.WoodenBoomerang, ItemID.Blowpipe, ItemID.Aglet, ItemID.ClimbingClaws, ItemID.Umbrella, ItemID.Radar, ItemID.CordageGuide, ItemID.WandofSparking };
            MakeSwapRecipes(surfaceChest, Rarity.Chest, 0, "Forest", true);

            // Underground Chests
            int[] goldenChest = { ItemID.BandofRegeneration, ItemID.MagicMirror, ItemID.CloudinaBottle, ItemID.HermesBoots, ItemID.EnchantedBoomerang, ItemID.ShoeSpikes, ItemID.FlareGun };
            MakeSwapRecipes(goldenChest, Rarity.Chest, 1, "Underground", true);
            MakeTokenRecipe(ItemID.Extractinator, 20, Rarity.Chest, 1, "Underground");
            MakeTokenRecipe(ItemID.LavaCharm, 40, Rarity.Chest, 1, "Underground");
            MakeTokenRecipe(ItemID.AngelStatue, 5, Rarity.Chest, 1, "Underground");

            // Dungeon Chests
            int[] dungeonChest = { ItemID.MagicMissile, ItemID.Muramasa, ItemID.CobaltShield, ItemID.AquaScepter, ItemID.BlueMoon, ItemID.Handgun, ItemID.ShadowKey, ItemID.Valor };
            bonusItemID = ItemID.GoldenKey;
            MakeSwapRecipes(dungeonChest, Rarity.Chest, 2, "Dungeon", true);
            bonusItemID = -1;
            MakeTokenRecipe(ItemID.BoneWelder, 1, Rarity.Chest, 2, "Dungeon");

            // Jungle Chests
            int[] jungleChest = { ItemID.AnkletoftheWind, ItemID.FeralClaws, ItemID.StaffofRegrowth, ItemID.Boomstick };
            MakeSwapRecipes(jungleChest, Rarity.Chest, 2, "UndergroundJungle", true);
            MakeTokenRecipe(ItemID.LivingMahoganyLeafWand, 6, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.LivingMahoganyWand, 6, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.HoneyDispenser, 4, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.FlowerBoots, 20, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.FiberglassFishingPole, 30, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.Seaweed, 50, Rarity.Chest, 2, "UndergroundJungle");

            // Shadow Chests
            // Since we should not consume a Shadow Key but still require one and cannot use a Shadow Chest as a crafting station, we create a locked token instead
            MakeTokenRecipe(mod.ItemType<Items.Special.ShadowLockedToken>(), 2, Rarity.None, 0, "");
            int[] shadowChest = { ItemID.DarkLance, ItemID.Flamelash, ItemID.FlowerofFire, ItemID.Sunfury, ItemID.HellwingBow };
            bonusItemID = mod.ItemType<Items.Token.ShadowToken>();
            MakeSwapRecipes(shadowChest, Rarity.Chest, 0, "Underworld", true);
            bonusItemID = -1;

            // Ocean Chests
            int[] oceanChest = { ItemID.Trident, ItemID.BreathingReed, ItemID.Flipper };
            MakeSwapRecipes(oceanChest, Rarity.Chest, 1, "Ocean", true);
            MakeTokenRecipe(ItemID.WaterWalkingBoots, 15, Rarity.Uncommon, 1, "Ocean");

            // Sky Chests
            int[] skyChest = { ItemID.ShinyRedBalloon, ItemID.Starfury, ItemID.LuckyHorseshoe, ItemID.SkyMill };
            MakeSwapRecipes(skyChest, Rarity.RareChest, 1, "Space", true);

            // Ice chests
            int[] iceChest = { ItemID.IceBoomerang, ItemID.IceBlade, ItemID.IceSkates, ItemID.SnowballCannon, ItemID.BlizzardinaBottle, ItemID.FlurryBoots };
            MakeSwapRecipes(iceChest, Rarity.Chest, 1, "UndergroundSnow", true);
            MakeTokenRecipe(ItemID.IceMirror, 5, Rarity.Chest, 1, "UndergroundSnow");
            MakeTokenRecipe(ItemID.IceMachine, 5, Rarity.Chest, 1, "UndergroundSnow");
            MakeTokenRecipe(ItemID.Fish, 25, Rarity.Chest, 1, "UndergroundSnow");

            // Living Wood Chest
            int[] livingWoodChest = { ItemID.LivingWoodWand, ItemID.LeafWand, ItemID.LivingLoom };
            MakeSwapRecipes(livingWoodChest, Rarity.RareChest, 0, "Forest", true);

            // Pyramid Chest
            int[] pyramidChest = { ItemID.SandstorminaBottle, ItemID.PharaohsMask, ItemID.PharaohsRobe, ItemID.FlyingCarpet };
            MakeSwapRecipes(pyramidChest, Rarity.RareChest, 1, "Desert", true);

            // Other chests
            MakeTokenRecipe(ItemID.WebSlinger, 1, Rarity.RareChest, 1, "Underground");

            // Hardmode chest keys
            MakeTokenRecipe(ItemID.JungleKey, 2500, Rarity.VeryCommon, 3, "Jungle");
            MakeTokenRecipe(ItemID.CorruptionKey, 2500, Rarity.VeryCommon, 3, "Corruption");
            MakeTokenRecipe(ItemID.CrimsonKey, 2500, Rarity.VeryCommon, 3, "Crimson");
            MakeTokenRecipe(ItemID.HallowedKey, 2500, Rarity.VeryCommon, 3, "Hallow");
            MakeTokenRecipe(ItemID.FrozenKey, 2500, Rarity.VeryCommon, 3, "Snow");

            MakeTokenRecipe(ItemID.JungleKey, 2500, Rarity.VeryCommon, 3, "UndergroundJungle");
            MakeTokenRecipe(ItemID.CorruptionKey, 2500, Rarity.VeryCommon, 3, "UndergroundCorruption");
            MakeTokenRecipe(ItemID.CrimsonKey, 2500, Rarity.VeryCommon, 3, "UndergroundCrimson");
            MakeTokenRecipe(ItemID.HallowedKey, 2500, Rarity.VeryCommon, 3, "UndergroundHallow");
            MakeTokenRecipe(ItemID.FrozenKey, 2500, Rarity.VeryCommon, 3, "UndergroundSnow");

            /*
             * Exploration
             */

            // Standalone items
            MakeTokenRecipe(ItemID.LifeCrystal, 1, Rarity.UncommonObject, 1, "Underground");
            MakeTokenRecipe(ItemID.EnchantedSword, 1, Rarity.VeryRareObject, 1, "Underground");
            MakeTokenRecipe(ItemID.Arkhalis, 10, Rarity.VeryRareObject, 1, "Underground");

            MakeTokenRecipe(ItemID.Hellforge, 1, Rarity.CommonObject, 2, "Underworld");
            MakeTokenRecipe(ItemID.AlchemyTable, 1, Rarity.UncommonObject, 2, "Dungeon");

            MakeTokenRecipe(ItemID.LifeFruit, 1, Rarity.UncommonObject, 4, "UndergroundJungle");

            // Rare plants
            MakeTokenRecipe(ItemID.JungleRose, 100, Rarity.VeryCommonObject, 1, "Jungle");
            MakeTokenRecipe(ItemID.NaturesGift, 1, Rarity.RareObject, 2, "UndergroundJungle");

            // Orbs & hearts
            int[] shadowOrb = { ItemID.Musket, ItemID.ShadowOrb, ItemID.Vilethorn, ItemID.BallOHurt, ItemID.BandofStarpower };
            MakeSwapRecipes(shadowOrb, Rarity.UncommonObject, 1, "Corruption", true);

            int[] crimsonHeart = { ItemID.TheUndertaker, ItemID.CrimsonHeart, ItemID.PanicNecklace, ItemID.CrimsonRod, ItemID.TheRottedFork };
            MakeSwapRecipes(crimsonHeart, Rarity.UncommonObject, 1, "Crimson", true);

            // Extractinator
            MakeTokenRecipe(ItemID.AmberMosquito, 5000 / 50, Rarity.VeryCommonObject, 1, "UndergroundDesert");

            /*
             * Traps & Wiring
             */

            int[] undergroundTraps = { ItemID.DartTrap, ItemID.GeyserTrap, ItemID.Detonator };
            MakeTokenRecipes(undergroundTraps, 1, Rarity.UncommonObject, 1, "Underground");
            int[] templeTraps = { ItemID.SuperDartTrap, ItemID.SpikyBallTrap, ItemID.SpearTrap, ItemID.SuperDartTrap };
            MakeTokenRecipes(templeTraps, 1, Rarity.CommonObject, 5, "UndergroundJungle");

            /*
             * Dyes
             */

            // Standard dyes
            MakeTokenRecipe(ItemID.TealMushroom, 1, Rarity.Dye, 1, "Underground");
            MakeTokenRecipe(ItemID.GreenMushroom, 1, Rarity.Dye, 1, "Underground");
            MakeTokenRecipe(ItemID.SkyBlueFlower, 1, Rarity.Dye, 1, "Jungle");
            MakeTokenRecipe(ItemID.YellowMarigold, 1, Rarity.Dye, 1, "Forest");
            MakeTokenRecipe(ItemID.BlueBerries, 1, Rarity.Dye, 1, "Forest");
            MakeTokenRecipe(ItemID.LimeKelp, 1, Rarity.Dye, 1, "Underground");
            MakeTokenRecipe(ItemID.PinkPricklyPear, 1, Rarity.Dye, 1, "Desert");
            MakeTokenRecipe(ItemID.OrangeBloodroot, 1, Rarity.Dye, 1, "Underground");
            MakeTokenRecipe(ItemID.RedHusk, 1, Rarity.Dye, 1, "Underground");
            MakeTokenRecipe(ItemID.CyanHusk, 1, Rarity.Dye, 1, "UndergroundSnow");
            MakeTokenRecipe(ItemID.VioletHusk, 1, Rarity.Dye, 1, "UndergroundJungle");
            MakeTokenRecipe(ItemID.PurpleMucos, 1, Rarity.Dye, 1, "Ocean");
            MakeTokenRecipe(ItemID.BlackInk, 1, Rarity.Dye, 1, "Ocean");

            // Strange dyes
            int[] strangePlants = { ItemID.StrangePlant1, ItemID.StrangePlant2, ItemID.StrangePlant3, ItemID.StrangePlant4};
            MakeSwapRecipes(strangePlants, Rarity.RareObject, 1, "", true);

            bonusItemID = ItemID.StrangePlant1;
            resultAmount = 3;
            int[] prehmDyes = { ItemID.AcidDye, ItemID.BlueAcidDye, ItemID.RedAcidDye, ItemID.MushroomDye, ItemID.MirageDye, ItemID.NegativeDye, ItemID.PurpleOozeDye, ItemID.ReflectiveDye,
                ItemID.ReflectiveCopperDye, ItemID.ReflectiveGoldDye, ItemID.ReflectiveObsidianDye, ItemID.ReflectiveMetalDye, ItemID.ReflectiveSilverDye, ItemID.ShadowDye};
            MakeTokenRecipes(prehmDyes, prehmDyes.Length, Rarity.Redeem, 1);
            int[] hmDyes = { ItemID.GelDye, ItemID.GrimDye, ItemID.HadesDye, ItemID.BurningHadesDye, ItemID.ShadowflameHadesDye, ItemID.PhaseDye, ItemID.ShiftingSandsDye, ItemID.TwilightDye };
            MakeTokenRecipes(hmDyes, prehmDyes.Length + hmDyes.Length, Rarity.Redeem, 3);
            int[] mbDyes = { ItemID.ChlorophyteDye, ItemID.LivingOceanDye, ItemID.LivingFlameDye, ItemID.LivingRainbowDye };
            MakeTokenRecipes(mbDyes, prehmDyes.Length + hmDyes.Length + mbDyes.Length, Rarity.Redeem, 4);
            int[] planteraDyes = { ItemID.PixieDye, ItemID.WispDye, ItemID.InfernalWispDye, ItemID.UnicornWispDye };
            MakeTokenRecipes(planteraDyes, prehmDyes.Length + hmDyes.Length + mbDyes.Length + planteraDyes.Length, Rarity.Redeem, 5);
            MakeTokenRecipe(ItemID.MartianArmorDye, prehmDyes.Length + hmDyes.Length + mbDyes.Length + planteraDyes.Length + 2, Rarity.Redeem, 6);
            MakeTokenRecipe(ItemID.MidnightRainbowDye, prehmDyes.Length + hmDyes.Length + mbDyes.Length + planteraDyes.Length + 2, Rarity.Redeem, 6);
            MakeTokenRecipe(ItemID.DevDye, prehmDyes.Length + hmDyes.Length + mbDyes.Length + planteraDyes.Length + 3, Rarity.Redeem, 7);
            resultAmount = 1;
            bonusItemID = -1;

            /*
             * Critters
             */

            // Golden critters
            bonusItemID = ItemID.GoldCoin;
            bonusItemAmount = 10;
            int[] goldenCritters = { ItemID.GoldBird, ItemID.GoldBunny, ItemID.GoldFrog, ItemID.GoldGrasshopper, ItemID.GoldMouse, ItemID.SquirrelGold, ItemID.GoldWorm, ItemID.GoldButterfly };
            MakeTokenRecipes(goldenCritters, 1, Rarity.VeryRare, 1);
            bonusItemID = -1;
            bonusItemAmount = 1;

            // Other rare critters
            MakeTokenRecipe(ItemID.TreeNymphButterfly, 1, Rarity.VeryRare, 1);
            MakeTokenRecipe(ItemID.TruffleWorm, 1, Rarity.Rare, 6, "Underground");

            /*
             * Fishing
             */

            // Rare Fish
            MakeTokenRecipe(ItemID.BlueJellyfish, 120, Rarity.Fishing, 1, "Fishing", "Underground");
            bonusItemID = ItemID.GoldCoin;
            bonusItemAmount = 10;
            MakeTokenRecipe(ItemID.GoldenCarp, 100, Rarity.Fishing, 1, "Fishing");
            bonusItemID = -1;
            bonusItemAmount = 1;
            MakeTokenRecipe(ItemID.GreenJellyfish, 240, Rarity.Fishing, 3, "Fishing", "Underground");
            MakeTokenRecipe(ItemID.PinkJellyfish, 120, Rarity.Fishing, 1, "Fishing", "Ocean");

            // Other catches
            MakeTokenRecipe(ItemID.FrogLeg, 500, Rarity.Fishing, 1, "Fishing");
            MakeTokenRecipe(ItemID.BalloonPufferfish, 625, Rarity.Fishing, 1, "Fishing");
            MakeTokenRecipe(ItemID.PurpleClubberfish, 50, Rarity.Fishing, 1, "Fishing", "Corruption");
            MakeTokenRecipe(ItemID.ReaverShark, 100, Rarity.Fishing, 2, "Fishing", "Ocean");
            MakeTokenRecipe(ItemID.Rockfish, 50, Rarity.Fishing, 1, "Fishing", "Underground");
            MakeTokenRecipe(ItemID.SawtoothShark, 100, Rarity.Fishing, 1, "Fishing", "Ocean");
            MakeTokenRecipe(ItemID.Swordfish, 60, Rarity.Fishing, 1, "Fishing", "Ocean");
            MakeTokenRecipe(ItemID.ZephyrFish, 1560, Rarity.Fishing, 1, "Fishing");
            MakeTokenRecipe(ItemID.Toxikarp, 200, Rarity.Fishing, 3, "Fishing", "Corruption");
            MakeTokenRecipe(ItemID.Bladetongue, 200, Rarity.Fishing, 3, "Fishing", "Crimson");
            MakeTokenRecipe(ItemID.CrystalSerpent, 200, Rarity.Fishing, 3, "Fishing", "Hallow");
            MakeTokenRecipe(ItemID.ScalyTruffle, 100 * 2, Rarity.Fishing, 1, "Fishing", "UndergroundSnow");
            MakeTokenRecipe(ItemID.ObsidianSwordfish, 7 * 10, Rarity.Fishing, 3, "Fishing", "Underground");

            // Crates
            MakeTokenRecipe(ItemID.SailfishBoots, 20, Rarity.FishingCrate, 1, "Fishing");
            MakeTokenRecipe(ItemID.TsunamiInABottle, 20, Rarity.FishingCrate, 1, "Fishing");
            MakeTokenRecipe(ItemID.GingerBeard, 25, Rarity.FishingCrate, 1, "Fishing");
            MakeTokenRecipe(ItemID.TartarSauce, 21, Rarity.FishingCrate, 1, "Fishing");
            MakeTokenRecipe(ItemID.FalconBlade, 16, Rarity.FishingCrate, 1, "Fishing");
            MakeTokenRecipe(ItemID.Sundial, 60, Rarity.FishingCrate, 3, "Fishing");
            MakeTokenRecipe(ItemID.HardySaddle, 10 * 4, Rarity.FishingCrate, 1, "Fishing");

            /*
             * Furniture
             */

            // Decorative banners
            int[] dungeonBanners = { ItemID.MarchingBonesBanner, ItemID.RaggedBrotherhoodSigil, ItemID.NecromanticSign, ItemID.MoltenLegionFlag, ItemID.RustedCompanyStandard, ItemID.DiabolicSigil };
            int[] spaceBanners = { ItemID.WorldBanner, ItemID.SunBanner, ItemID.GravityBanner };
            int[] underworldBanners = { ItemID.HellboundBanner, ItemID.LostHopesofManBanner, ItemID.HellHammerBanner, ItemID.ObsidianWatcherBanner, ItemID.HelltowerBanner, ItemID.LavaEruptsBanner };
            int[] pyramidBanners = { ItemID.AnkhBanner, ItemID.SnakeBanner, ItemID.OmegaBanner };
            MakeTokenRecipes(dungeonBanners, 1, Rarity.Furniture, 2, "Dungeon");
            MakeTokenRecipes(spaceBanners, 1, Rarity.Furniture, 1, "Space");
            MakeTokenRecipes(underworldBanners, 1, Rarity.Furniture, 2, "Underworld");
            MakeTokenRecipes(pyramidBanners, 1, Rarity.Furniture, 1, "Desert");

            // Uncraftable chests
            MakeTokenRecipe(ItemID.GoldChest, 1, Rarity.Chest, 1, "Underground");
            MakeTokenRecipe(ItemID.IceChest, 1, Rarity.Chest, 1, "UndergroundSnow");
            MakeTokenRecipe(ItemID.IvyChest, 1, Rarity.Chest, 2, "UndergroundJungle");
            MakeTokenRecipe(ItemID.LihzahrdChest, 1, Rarity.Chest, 5, "UndergroundJungle");
            MakeTokenRecipe(ItemID.LivingWoodChest, 1, Rarity.Chest, 0, "Underground");
            MakeTokenRecipe(ItemID.SkywareChest, 1, Rarity.RareChest, 1, "Space");
            MakeTokenRecipe(ItemID.WaterChest, 1, Rarity.Chest, 1, "Ocean");
            MakeTokenRecipe(ItemID.WebCoveredChest, 1, Rarity.RareChest, 1, "Underground");
            MakeTokenRecipe(ItemID.ShadowChest, 1, Rarity.Chest, 2, "Underworld");
            MakeTokenRecipe(ItemID.CorruptionChest, 1, Rarity.Chest, 2, "Dungeon");
            MakeTokenRecipe(ItemID.CrimsonChest, 1, Rarity.Chest, 2, "Dungeon");
            MakeTokenRecipe(ItemID.HallowedChest, 1, Rarity.Chest, 2, "Dungeon");
            MakeTokenRecipe(ItemID.FrozenChest, 1, Rarity.Chest, 2, "Dungeon");
            MakeTokenRecipe(ItemID.JungleChest, 1, Rarity.Chest, 2, "Dungeon");

            // Standalone furniture
            int[] dungeonDecorations = { ItemID.WallSkeleton, ItemID.HangingSkeleton, ItemID.BrassLantern, ItemID.CagedLantern, ItemID.CarriageLantern, ItemID.AlchemyLantern, ItemID.DiablostLamp,
                ItemID.OilRagSconse, ItemID.Catacomb, ItemID.GothicBookcase, ItemID.GothicChair, ItemID.GothicTable, ItemID.GothicWorkBench, ItemID.DungeonDoor };
            MakeTokenRecipes(dungeonDecorations, 1, Rarity.Furniture, 2, "Dungeon");
            int[] blueDungeonDecorations = { ItemID.BlueDungeonBathtub, ItemID.BlueDungeonBed, ItemID.BlueDungeonBookcase, ItemID.BlueDungeonCandelabra, ItemID.BlueDungeonCandle,
                ItemID.BlueDungeonChair, ItemID.BlueDungeonChandelier, ItemID.BlueDungeonDoor, ItemID.BlueDungeonDresser, ItemID.BlueDungeonLamp, ItemID.BlueDungeonPiano, ItemID.BlueDungeonSofa,
                ItemID.BlueDungeonTable, ItemID.BlueDungeonVase, ItemID.BlueDungeonWorkBench };
            int[] greenDungeonDecorations = { ItemID.GreenDungeonBathtub, ItemID.GreenDungeonBed, ItemID.GreenDungeonBookcase, ItemID.GreenDungeonCandelabra, ItemID.GreenDungeonCandle,
                ItemID.GreenDungeonChair, ItemID.GreenDungeonChandelier, ItemID.GreenDungeonDoor, ItemID.GreenDungeonDresser, ItemID.GreenDungeonLamp, ItemID.GreenDungeonPiano, ItemID.GreenDungeonSofa,
                ItemID.GreenDungeonTable, ItemID.GreenDungeonVase, ItemID.GreenDungeonWorkBench };
            int[] pinkDungeonDecorations = { ItemID.PinkDungeonBathtub, ItemID.PinkDungeonBed, ItemID.PinkDungeonBookcase, ItemID.PinkDungeonCandelabra, ItemID.PinkDungeonCandle,
                ItemID.PinkDungeonChair, ItemID.PinkDungeonChandelier, ItemID.PinkDungeonDoor, ItemID.PinkDungeonDresser, ItemID.PinkDungeonLamp, ItemID.PinkDungeonPiano, ItemID.PinkDungeonSofa,
                ItemID.PinkDungeonTable, ItemID.PinkDungeonVase, ItemID.PinkDungeonWorkBench };
            MakeTokenRecipes(blueDungeonDecorations, 1, Rarity.Furniture, 2, "Dungeon");
            MakeTokenRecipes(greenDungeonDecorations, 1, Rarity.Furniture, 2, "Dungeon");
            MakeTokenRecipes(pinkDungeonDecorations, 1, Rarity.Furniture, 2, "Dungeon");
            resultAmount = 50;
            int[] dungeonPlatforms = { ItemID.DungeonShelf, ItemID.WoodShelf, ItemID.MetalShelf, ItemID.BrassShelf, ItemID.BlueBrickPlatform, ItemID.GreenBrickPlatform, ItemID.PinkBrickPlatform, ItemID.Book };
            MakeTokenRecipes(dungeonPlatforms, 1, Rarity.Furniture, 2, "Dungeon");
            resultAmount = 1;
            int[] underworldDecorations = { ItemID.ObsidianBathtub, ItemID.ObsidianBed, ItemID.ObsidianBookcase, ItemID.ObsidianCandelabra, ItemID.ObsidianCandle, ItemID.ObsidianChair,
                ItemID.ObsidianChandelier, ItemID.ObsidianDoor, ItemID.ObsidianDresser, ItemID.ObsidianLamp, ItemID.ObsidianPiano, ItemID.ObsidianSofa, ItemID.ObsidianTable, ItemID.ObsidianVase,
                ItemID.ObsidianWorkBench };
            MakeTokenRecipes(underworldDecorations, 1, Rarity.Furniture, 2, "Underworld");

            /*
             * Statues
             */

            int[] mobStatues = { ItemID.BatStatue, ItemID.ChestStatue, ItemID.CrabStatue, ItemID.JellyfishStatue, ItemID.PiranhaStatue, ItemID.SharkStatue,
                ItemID.SkeletonStatue, ItemID.SlimeStatue, ItemID.WallCreeperStatue, ItemID.UnicornStatue, ItemID.DripplerStatue, ItemID.WraithStatue,
                ItemID.BoneSkeletonStatue, ItemID.UndeadVikingStatue, ItemID.MedusaStatue, ItemID.HarpyStatue, ItemID.PigronStatue, ItemID.HopliteStatue,
                ItemID.GraniteGolemStatue, ItemID.ZombieArmStatue, ItemID.BloodZombieStatue };
            int[] functionalStatues = { ItemID.KingStatue, ItemID.QueenStatue, ItemID.BombStatue, ItemID.HeartStatue, ItemID.StarStatue };
            int[] decorationStatues = { ItemID.MushroomStatue, ItemID.AnvilStatue, ItemID.ArmorStatue, ItemID.AxeStatue, ItemID.BoomerangStatue, ItemID.BootStatue,
                ItemID.BowStatue, ItemID.CorruptStatue, ItemID.CrossStatue, ItemID.EyeballStatue, ItemID.GargoyleStatue, ItemID.GloomStatue, ItemID.GoblinStatue,
                ItemID.HammerStatue, ItemID.HornetStatue, ItemID.ImpStatue, ItemID.PickaxeStatue, ItemID.PillarStatue, ItemID.PotStatue, ItemID.PotionStatue,
                ItemID.ReaperStatue, ItemID.ShieldStatue, ItemID.SpearStatue, ItemID.SunflowerStatue, ItemID.SwordStatue, ItemID.TreeStatue, ItemID.WomanStatue,
                ItemID.LihzahrdStatue, ItemID.LihzahrdGuardianStatue, ItemID.LihzahrdWatcherStatue };
            MakeTokenRecipes(mobStatues, 1, Rarity.Furniture, 1, "Underground");
            MakeTokenRecipes(functionalStatues, 1, Rarity.Furniture, 1, "Underground");
            MakeTokenRecipes(decorationStatues, 1, Rarity.Furniture, 1, "Underground");

            /*
             * Paintings
             */

            int[] undergroundPaintings = { ItemID.AmericanExplosive, ItemID.CrownoDevoursHisLunch, ItemID.Discover, ItemID.FatherofSomeone, ItemID.FindingGold, ItemID.GloriousNight, ItemID.GuidePicasso,
                ItemID.Land, ItemID.TheMerchant, ItemID.NurseLisa, ItemID.OldMiner, ItemID.RareEnchantment, ItemID.Sunflowers, ItemID.TerrarianGothic, ItemID.Waldo };
            MakeTokenRecipes(undergroundPaintings, 1, Rarity.Furniture, 1, "Underground");
            int[] dungeonPaintings = { ItemID.BloodMoonRising, ItemID.BoneWarp, ItemID.TheCreationoftheGuide, ItemID.TheCursedMan, ItemID.TheDestroyer, ItemID.Dryadisque, ItemID.TheEyeSeestheEnd,
                ItemID.FacingtheCerebralMastermind, ItemID.GloryoftheFire, ItemID.GoblinsPlayingPoker, ItemID.GreatWave, ItemID.TheGuardiansGaze, ItemID.TheHangedMan,
                ItemID.Impact, ItemID.ThePersistencyofEyes, ItemID.PoweredbyBirds, ItemID.TheScreamer, ItemID.SkellingtonJSkellingsworth, ItemID.SparkyPainting, ItemID.SomethingEvilisWatchingYou,
                ItemID.StarryNight, ItemID.TrioSuperHeroes, ItemID.TheTwinsHaveAwoken, ItemID.UnicornCrossingtheHallows };
            MakeTokenRecipes(undergroundPaintings, 1, Rarity.Furniture, 2, "Dungeon");
            int[] underworldPaintings = { ItemID.DarkSoulReaper, ItemID.Darkness, ItemID.DemonsEye, ItemID.FlowingMagma, ItemID.HandEarth, ItemID.ImpFace, ItemID.LakeofFire, ItemID.LivingGore,
                ItemID.OminousPresence, ItemID.ShiningMoon, ItemID.Skelehead, ItemID.TrappedGhost };
            MakeTokenRecipes(undergroundPaintings, 1, Rarity.Furniture, 2, "Underworld");


            /*
             * Angler rewards
             */

            MakeTokenRecipe(ItemID.FuzzyCarrot, 5, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.AnglerHat, 15, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.AnglerVest, 15, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.AnglerPants, 15, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.GoldenFishingRod, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.GoldenBugNet, 75, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FishHook, 60, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.HighTestFishingLine, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.AnglerEarring, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.TackleBox, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FishermansGuide, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.WeatherRadio, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.Sextant, 40, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FinWings, 70, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.BottomlessBucket, 70, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.SuperAbsorbantSponge, 70, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.HotlineFishingHook, 75, Rarity.QuestReward, 1, "Fishing");
            resultAmount = 30;
            MakeTokenRecipe(ItemID.CoralstoneBlock, 100 / 30, Rarity.QuestReward, 1, "Fishing");
            resultAmount = 1;
            MakeTokenRecipe(ItemID.MermaidAdornment, 50 / 2, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.MermaidTail, 50 / 2, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FishCostumeMask, 50 / 3, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FishCostumeShirt, 50 / 3, Rarity.QuestReward, 1, "Fishing");
            MakeTokenRecipe(ItemID.FishCostumeFinskirt, 50 / 3, Rarity.QuestReward, 1, "Fishing");
            int[] anglerDecorativeRewards = { ItemID.BunnyfishTrophy, ItemID.GoldfishTrophy, ItemID.SharkteethTrophy, ItemID.SwordfishTrophy, ItemID.TreasureMap, ItemID.SeaweedPlanter,
                ItemID.PillaginMePixels, ItemID.CompassRose, ItemID.ShipsWheel, ItemID.LifePreserver, ItemID.WallAnchor, ItemID.ShipInABottle };
            MakeTokenRecipes(anglerDecorativeRewards, 100, Rarity.QuestReward, 1, "Fishing");

            /*
             * Traveling Merchants
             */

            // Traveling Merchant
            MakeTMRecipes(ItemID.Sake, 6, 1, 1, 2);
            MakeTMRecipes(ItemID.Pho, 6, 1, 3, 1);
            MakeTMRecipes(ItemID.PadThai, 6, 1, 2, 1);
            MakeTMRecipes(ItemID.UltrabrightTorch, 12, 1, 3, 10);
            MakeTMRecipes(ItemID.AmmoBox, 18, 1, 150);
            MakeTMRecipes(ItemID.MagicHat, 18, 1, 30);
            MakeTMRecipes(ItemID.GypsyRobe, 18, 1, 20);
            MakeTMRecipes(ItemID.Gi, 18, 1, 20);
            MakeTMRecipes(ItemID.CelestialMagnet, 24, 1, 150);
            MakeTMRecipes(ItemID.DPSMeter, 6, 1, 50);
            MakeTMRecipes(ItemID.LifeformAnalyzer, 6, 1, 50);
            MakeTMRecipes(ItemID.Stopwatch, 6, 1, 50);
            MakeTMRecipes(ItemID.PaintSprayer, 12, 1, 100);
            MakeTMRecipes(ItemID.BrickLayer, 12, 1, 100);
            MakeTMRecipes(ItemID.PortableCementMixer, 12, 1, 100);
            MakeTMRecipes(ItemID.ExtendoGrip, 12, 1, 100);
            MakeTMRecipes(ItemID.ActuationAccessory, 12, 1, 100);
            MakeTMRecipes(ItemID.BlackCounterweight, 30, 1, 50);
            MakeTMRecipes(ItemID.YellowCounterweight, 24, 1, 50);
            MakeTMRecipes(ItemID.SittingDucksFishingRod, 24, 1, 350);
            MakeTMRecipes(ItemID.Katana, 12, 1, 40);
            MakeTMRecipes(ItemID.Code1, 12, 1, 50);
            MakeTMRecipes(ItemID.Revolver, 18, 1, 100);
            MakeTMRecipes(ItemID.Gatligator, 30, 3, 350);
            MakeTMRecipes(ItemID.Code2, 12, 4, 250);
            MakeTMRecipes(ItemID.PulseBow, 24, 5, 450);
            MakeTMRecipes(ItemID.TeamBlockRed, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockRedPlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockYellow, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockYellowPlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockGreen, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockGreenPlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockBlue, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockBluePlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockPink, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockPinkPlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockWhite, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.TeamBlockWhitePlatform, 6 / 2, 1, 1, 10);
            MakeTMRecipes(ItemID.DiamondRing, 24, 1, 2000);
            MakeTMRecipes(ItemID.AngelHalo, 36, 1, 400);
            MakeTMRecipes(ItemID.Fez, 18, 1, 35);
            MakeTMRecipes(ItemID.WinterCape, 24, 1, 50);
            MakeTMRecipes(ItemID.RedCape, 24, 1, 50);
            MakeTMRecipes(ItemID.CrimsonCloak, 24, 1, 50);
            MakeTMRecipes(ItemID.MysteriousCape, 24, 1, 50);
            MakeTMRecipes(ItemID.Kimono, 30, 1, 10);
            MakeTMRecipes(ItemID.WaterGun, 24, 1, 15);
            MakeTMRecipes(ItemID.CompanionCube, 24, 1, 5000);
            MakeTMRecipes(ItemID.SteampunkCup, 6, 1, 5);
            MakeTMRecipes(ItemID.ArcaneRuneWall, 30, 1, 1, 4);
            MakeTMRecipes(ItemID.FancyDishes, 6, 1, 2);
            MakeTMRecipes(ItemID.DynastyWood, 6 / 3, 1, 1, 20);
            MakeTMRecipes(ItemID.RedDynastyShingles, 6 / 3, 1, 1, 20);
            MakeTMRecipes(ItemID.BlueDynastyShingles, 6 / 3, 1, 1, 20);
            MakeTMRecipes(ItemID.ZebraSkin, 18, 1, 10);
            MakeTMRecipes(ItemID.LeopardSkin, 18, 1, 10);
            MakeTMRecipes(ItemID.TigerSkin, 18, 1, 10);
            MakeTMRecipes(ItemID.PaintingCastleMarsberg, 12, 6, 30);
            MakeTMRecipes(ItemID.PaintingMartiaLisa, 12, 6, 30);
            MakeTMRecipes(ItemID.PaintingTheTruthIsUpThere, 12, 6, 30);
            MakeTMRecipes(ItemID.MoonLordPainting, 12, 7, 30);
            MakeTMRecipes(ItemID.PaintingAcorns, 12 * 3, 1, 5);
            MakeTMRecipes(ItemID.PaintingColdSnap, 12 * 3, 1, 5);
            MakeTMRecipes(ItemID.PaintingCursedSaint, 12 * 3, 1, 5);
            MakeTMRecipes(ItemID.PaintingSnowfellas, 12 * 3, 1, 5);
            MakeTMRecipes(ItemID.PaintingTheSeason, 12 * 3, 1, 5);

            // Skeleton Merchant
            MakeTMRecipes(ItemID.StrangeBrew, 2, 1, 1, 2);
            MakeTMRecipes(ItemID.SpelunkerGlowstick, 1, 1, 3, 20);
            MakeTMRecipes(ItemID.BoneTorch, 1, 1, 1, 10);
            MakeTMRecipes(ItemID.BoneArrow, 2, 1, 3, 200);
            MakeTMRecipes(ItemID.BlueCounterweight, 4, 1, 50);
            MakeTMRecipes(ItemID.RedCounterweight, 4, 1, 50);
            MakeTMRecipes(ItemID.PurpleCounterweight, 4, 1, 50);
            MakeTMRecipes(ItemID.GreenCounterweight, 4, 1, 50);
            MakeTMRecipes(ItemID.Gradient, 2, 3, 200);
            MakeTMRecipes(ItemID.FormatC, 2, 3, 200);
            MakeTMRecipes(ItemID.YoYoGlove, 1, 3, 500);
            MakeTMRecipes(ItemID.SlapHand, 18, 3, 250);
            MakeTMRecipes(ItemID.MagicLantern, 16, 1, 100);

            /*
             * Seasonal
             */

            // Halloween
            bonusItemID = ItemID.Pumpkin;
            bonusItemAmount = 10;
            MakeTokenRecipe(ItemID.BloodyMachete, 2000 / 2, Rarity.VeryCommon, 0);
            MakeTokenRecipe(ItemID.BladedGlove, 2000 / 2, Rarity.VeryCommon, 0);
            MakeTokenRecipe(ItemID.MagicalPumpkinSeed, 200, Rarity.Common, 0);
            MakeTokenRecipe(ItemID.GoodieBag, 80, Rarity.VeryCommon, 0);
            bonusItemID = ItemID.GoodieBag;
            bonusItemAmount = 1;
            int[] fallCostumes = { ItemID.CatEars, ItemID.CreeperMask, ItemID.CreeperShirt, ItemID.CreeperPants, ItemID.PumpkinMask, ItemID.PumpkinShirt, ItemID.PumpkinPants,
                ItemID.SpaceCreatureMask, ItemID.SpaceCreatureShirt, ItemID.SpaceCreaturePants, ItemID.CatMask, ItemID.CatShirt, ItemID.CatPants, ItemID.KarateTortoiseMask,
                ItemID.KarateTortoiseShirt, ItemID.KarateTortoisePants, ItemID.FoxMask, ItemID.FoxShirt, ItemID.FoxPants, ItemID.WitchHat, ItemID.WitchDress, ItemID.WitchBoots,
                ItemID.VampireMask, ItemID.VampireShirt, ItemID.VampirePants, ItemID.LeprechaunHat, ItemID.LeprechaunShirt, ItemID.LeprechaunPants, ItemID.RobotMask,
                ItemID.RobotShirt, ItemID.RobotPants, ItemID.PrincessHat, ItemID.PrincessDressNew, ItemID.TreasureHunterShirt, ItemID.TreasureHunterPants, ItemID.WolfMask,
                ItemID.WolfShirt, ItemID.WolfPants, ItemID.UnicornMask, ItemID.UnicornShirt, ItemID.UnicornPants, ItemID.ReaperHood, ItemID.ReaperRobe, ItemID.PixieShirt,
                ItemID.PixiePants, ItemID.BrideofFrankensteinMask, ItemID.BrideofFrankensteinDress, ItemID.GhostMask, ItemID.GhostShirt };
            int[] fallpaintings = { ItemID.BitterHarvest, ItemID.BloodMoonCountess, ItemID.HallowsEve, ItemID.JackingSkeletron, ItemID.MorbidCuriosity };
            MakeTokenRecipes(fallCostumes, 28 / 2, Rarity.Redeem, 0);
            MakeTokenRecipes(fallpaintings, 67, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.UnluckyYarn, 150, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.BatHook, 151, Rarity.Redeem, 0);
            bonusItemID = -1;

            // Christmas
            bonusItemID = ItemID.SnowBlock;
            bonusItemAmount = 100;
            MakeTokenRecipe(ItemID.Present, 13, Rarity.VeryCommon, 0);
            MakeTokenRecipe(ItemID.GiantBow, 20, Rarity.Common, 0, "Forest");
            bonusItemID = ItemID.Present;
            bonusItemAmount = 1;
            int[] winterCostumes = { ItemID.TreeMask, ItemID.TreeShirt, ItemID.TreeTrunks, ItemID.ParkaHood, ItemID.ParkaCoat, ItemID.ParkaPants, ItemID.MrsClauseHat,
                ItemID.MrsClauseShirt, ItemID.MrsClauseHeels, ItemID.UglySweater, ItemID.SnowHat };
            MakeTokenRecipe(ItemID.SnowGlobe, 15, Rarity.Redeem, 3);
            MakeTokenRecipes(winterCostumes, 82 / 2, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.Coal, 30, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.Holly, 11, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.ReindeerAntlers, 43, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.Toolbox, 323, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.HandWarmer, 161, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.FruitcakeChakram, 160, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.CandyCaneHook, 159, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.CnadyCanePickaxe, 159, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.CandyCaneSword, 156, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.RedRyder, 156, Rarity.Redeem, 0);
            MakeTokenRecipe(ItemID.DogWhistle, 417, Rarity.Redeem, 0);
            bonusItemID = -1;
            

            /*
             * Other
             */

            // Meteor Charm
            // Used to manually drop a meteorite
            MakeTokenRecipe(mod.ItemType<Items.Special.MeteorCharm>(), 1, Rarity.RareObject, 2);

            // Eternia Tokens
            // Compensates the fact that DD2 enemies do not drop gold or tokens
            bonusItemID = ItemID.DefenderMedal;
            MakeTokenRecipe(mod.ItemType<Items.Special.EterniaStone>(), 0, Rarity.None, 0);
            bonusItemID = -1;
        }


    }
}
