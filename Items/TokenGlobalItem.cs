using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TokenMod;

namespace ExampleMod.Items
{
    public class TokenGlobalItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            float baseBossValue = 0f;
            switch (arg)
            {
                case ItemID.KingSlimeBossBag: baseBossValue = 2; break;
                case ItemID.EyeOfCthulhuBossBag: baseBossValue = 5; break;
                case ItemID.EaterOfWorldsBossBag: baseBossValue = 5; break;
                case ItemID.BrainOfCthulhuBossBag: baseBossValue = 5; break;
                case ItemID.QueenBeeBossBag: baseBossValue = 8; break;
                case ItemID.WallOfFleshBossBag: baseBossValue = 10; break;
                case ItemID.SkeletronBossBag: baseBossValue = 8; break;
                case ItemID.DestroyerBossBag: baseBossValue = 12; break;
                case ItemID.TwinsBossBag: baseBossValue = 12; break;
                case ItemID.SkeletronPrimeBossBag: baseBossValue = 12; break;
                case ItemID.PlanteraBossBag: baseBossValue = 20; break;
                case ItemID.GolemBossBag: baseBossValue = 25; break;
                case ItemID.FishronBossBag: baseBossValue = 30; break;
                case ItemID.MoonLordBossBag: baseBossValue = 50; break;
            }
            TokenUtils.DropTokens(mod, player, baseBossValue * 10000f, player.getRect(), true, true, new List<int> { mod.ItemType<TokenMod.Items.Token.BossToken>() }, true);
        }
    }
}