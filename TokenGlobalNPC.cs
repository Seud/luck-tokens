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
                int tokenOverride = (npc.boss) ? mod.ItemType<Items.Token.BossToken>() : 0;
                TokenUtils.DropTokens(mod, plr, npc.value, npc.getRect(), (npc.boss) ? true : TokenBalance.VALUE_INFLUENCES_DROPRATE, tokenOverride);
            } else if (npc.lifeMax > 1 && npc.friendly)
            {
                TokenUtils.DropTokens(mod, plr, TokenBalance.NPC_VALUE, npc.getRect(), TokenBalance.VALUE_INFLUENCES_DROPRATE, mod.ItemType<Items.Token.NPCToken>(), true);
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
