using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod.NPCs
{
    public class TokenGlobalNPC : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            // If the NPC was a boss, use special check
            if(TokenUtils.CheckBoss(mod, npc)) return;

            if (npc.lifeMax > 1 && !npc.friendly && npc.value > 0f && !npc.boss)
            {
                TokenUtils.DropTokens(mod, null, npc.value, npc, false, 1, true, TokenUtils.GetLocationTokens(mod, Main.player[Player.FindClosest(npc.position, npc.width, npc.height)]));
            } else if (npc.lifeMax > 1 && npc.friendly)
            {
                TokenUtils.DropTokens(mod, null, TokenBalance.NPC_VALUE, npc, false, 1, false, new List<int> { mod.ItemType<Items.Token.NPCToken>() } );
            }

        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Token.TravelingMerchantToken>());
                nextSlot++;
            }
        }
    }
}
