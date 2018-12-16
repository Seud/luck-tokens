using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod.NPCs
{
    public class TokenGlobalNPC : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            Player plr = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];

            // Token drop check
            if (npc.lifeMax > 1 && !npc.friendly && npc.value > 0f)
            {
                if(npc.boss)
                    TokenUtils.DropTokens(mod, plr, npc.value, npc.getRect(), true, true, mod.ItemType<Items.Token.BossToken>());
                else
                    TokenUtils.DropTokens(mod, plr, npc.value, npc.getRect(), false, true);
            } else if (npc.lifeMax > 1 && (npc.friendly || npc.damage == 0))
            {
                TokenUtils.DropTokens(mod, plr, TokenBalance.NPC_VALUE, npc.getRect(), false, false, mod.ItemType<Items.Token.NPCToken>());
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
