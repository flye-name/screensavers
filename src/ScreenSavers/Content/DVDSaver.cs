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
		Position = new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
	}
	public void Unload() { }
	
	public static Vector2 Position = Vector2.Zero;
	public static Vector2 Direction = Vector2.One;
	public static Color Color = Color.White;
	public static void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * AFK.EffectProgress);
		
		spriteBatch.Draw(TextureAssets.Moon[0].Value, Position, TextureAssets.Moon[0].Value.Frame(1, 8, 0, 0),Color with { A = 0 } * AFK.EffectProgress);

		Vector2 Size = TextureAssets.Moon[0].Value.Frame(1, 8, 0, 0).Size();
		Position += Direction * 4;

		if ((Position + Direction * Size.X).X > Main.screenWidth || (Position + Direction).X < 0)
		{
			Direction.X = -Direction.X;
			Color = Main.DiscoColor;
		}

		if ((Position + Direction * Size.Y).Y > Main.screenHeight || (Position + Direction).Y < 0)
		{
			Direction.Y = -Direction.Y;
			Color = Main.DiscoColor;
		}
	}
}