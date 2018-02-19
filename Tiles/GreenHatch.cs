using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System;

namespace MetroidMod.Tiles
{
	public class GreenHatch : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
				Main.tileBlockLight[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3); 
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Green Hatch");
			AddMapEntry(new Color(0, 160, 0), name);
			dustType = 1;
		}
		public override bool Slope(int i, int j)
		{
			return false;
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}
public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			Main.spriteBatch.Draw(mod.GetTexture("Tiles/GreenHatchDoor"), new Vector2((i - 1) * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 48, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return true;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("GreenHatch"));
		}
		public override void HitWire(int i, int j)
		{
			Main.PlaySound(SoundLoader.customSoundType, i * 16, j * 16,  mod.GetSoundSlot(SoundType.Custom, "Sounds/HatchOpenSound"));
			int x = i - (Main.tile[i, j].frameX / 18) % 3;
			int y = j - (Main.tile[i, j].frameY / 18) % 3;
			for (int l = x; l < x + 3; l++)
			{
				for (int m = y; m < y + 3; m++)
				{
					if (Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					if (Main.tile[l, m].active() && Main.tile[l, m].type == Type)
					{
						Main.tile[l,m].type = (ushort)mod.TileType("GreenHatchOpen");
					}
				}
			}
			if (Wiring.running)
			{
				Wiring.SkipWire(x, y);
				Wiring.SkipWire(x, y + 1);
				Wiring.SkipWire(x, y + 2);
				Wiring.SkipWire(x + 1, y);
				Wiring.SkipWire(x + 1, y + 1);
				Wiring.SkipWire(x + 1, y + 2);
			}
			NetMessage.SendTileSquare(-1, x, y + 1, 3);
		}
	}
}