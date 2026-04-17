using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using ScreenSavers.Core;

namespace ScreenSavers.Content;

public class DVDSaver : ILoadable
{
	public void Load(Mod mod)
	{
		for (int i = 0; i < Position.Length; i++)
			Position[i] = new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
	}
	public void Unload() { }
	
	public static Vector2[] Position = new Vector2[25];
	public static Vector2 Direction = Vector2.One;
	public static float HueOffset;
	public static void Draw(SpriteBatch spriteBatch)
	{
		HueOffset += 0.04f;
		
		for (int i = Main.rand.Next(Position.Length - 1); i > 0; i--)
		{
			Position[i] = Position[i - 1];
		}
		
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * AFK.EffectProgress);

		for (int i = 0; i < Position.Length; i++)
		{
			float factor = (i / (float)Position.Length);
			Color color = Main.hslToRgb((HueOffset + factor) % 1f, 0.5f, 0.5f) with { A = 0 } * (1 - factor) * 0.3f;

			spriteBatch.Draw(Assets.Images.DVD.Asset.Value, Position[i] + Main.rand.NextVector2Circular(25, 25) * factor, null, color * AFK.EffectProgress);
		}
		spriteBatch.Draw(Assets.Images.DVD.Asset.Value, Position[0], null, Color.White with { A = 0} * AFK.EffectProgress);

		Vector2 Size = Assets.Images.DVD.Asset.Value.Size();
		Position[0] += Direction * 4;

		if ((Position[0] + Direction * Size.X).X > Main.screenWidth || (Position[0] + Direction).X < 0)
		{
			Direction.X = -Direction.X;
		}

		if ((Position[0] + Direction * Size.Y).Y > Main.screenHeight || (Position[0] + Direction).Y < 0)
		{
			Direction.Y = -Direction.Y;
		}
	}
}