using Terraria;
using Terraria.Audio;
using Terraria.ID;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MetroidModPorted.Common.Players;

namespace MetroidModPorted.Content.Projectiles
{
	public class SpeedBall : ModProjectile
	{
		int SpeedSound = 0;
		public ReLogic.Utilities.SlotId soundInstance;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mock Ball");
		}
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 0;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;//Projectile.melee = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 9000;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 7;
			Projectile.alpha = 255;
		}
		public override void AI()
		{
			Player P = Main.player[Projectile.owner];
			Projectile.position.X=P.Center.X-Projectile.width/2;
			Projectile.position.Y=P.Center.Y-Projectile.height/2;
			
			SpeedSound++;
			if(SpeedSound == 4)
			{
				soundInstance = SoundEngine.PlaySound(Sounds.Items.Weapons.SpeedBoosterStartup, P.position);
			}
			if(SoundEngine.TryGetActiveSound(soundInstance, out ActiveSound result) && SpeedSound == 82)
			{
				result.Stop();
				soundInstance = SoundEngine.PlaySound(Sounds.Items.Weapons.SpeedBoosterLoop, P.position);
				SpeedSound = 68;
			}
			MPlayer mp = P.GetModPlayer<MPlayer>();
			if(!mp.ballstate || !mp.speedBoosting || mp.SMoveEffect > 0)
			{
				if(SoundEngine.TryGetActiveSound(soundInstance, out result))
				{
					result.Stop();
				}
				Projectile.Kill();
			}
			foreach(Projectile Pr in Main.projectile) if (Pr!= null)
			{
				if(Pr.active && (Pr.type == ModContent.ProjectileType<ShineBall>() || Pr.type == ModContent.ProjectileType<SpeedBoost>()))
				{
					if(SoundEngine.TryGetActiveSound(soundInstance, out result))
					{
						result.Stop();
					}
					Projectile.Kill();
					return;
				}
			}
			Lighting.AddLight((int)((float)Projectile.Center.X/16f), (int)((float)(Projectile.Center.Y)/16f), 0, 0.75f, 1f);
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage += (int)(target.damage * 1.5f);
		}
	}
}
