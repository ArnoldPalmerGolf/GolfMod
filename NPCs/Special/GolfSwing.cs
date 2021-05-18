using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GolfMod.NPCs.Special
{
    class GolfSwing : ModNPC
    {

        private int frameTimer = 0;
        private int frameNum = 0;

        public override bool? CanBeHitByProjectile(Projectile projectile) => false;

        public override bool? CanBeHitByItem(Player player, Item item) => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("---");
            Main.npcFrameCount[npc.type] = 11;
            NPCID.Sets.MustAlwaysDraw[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.immortal = true;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.knockBackResist = 0; //very very important!!
            npc.aiStyle = -1;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;

            if (frameTimer == 2)
            {
                frameNum++;
                frameTimer = 0;
            }

            if (frameNum == 11)
            {
                npc.life = 0;
            }

            npc.frame.Y = frameNum * frameHeight;
        }




        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.ai[2]++;
            if (npc.ai[2] >= 3)
            {
                npc.ai[2] = 0f;
                npc.netUpdate = true;
                frameTimer++;
            }

            Player player = Main.player[npc.target];
            Vector2 vectoridk = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float playerX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - vectoridk.X;
            float playerY = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - vectoridk.Y;
            if (playerX > 0f)
            {
                npc.spriteDirection = player.direction;
                npc.rotation = (float)Math.Atan2(playerX, -playerY) + 3.14f;
            }
            if (playerX < 0f)
            {
                npc.spriteDirection = -player.direction;
                npc.rotation = (float)Math.Atan2(playerX, -playerY) + -3.14f;
            }
        }

    }
}
