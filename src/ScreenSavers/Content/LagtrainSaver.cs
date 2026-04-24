using System;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScreenSavers.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ScreenSavers.Content.Other;

public class LagtrainSaver : ILoadable
{
	public static int Stage = 0;
	public static float Time = 0f;
	
	public void Load(Mod mod)
	{
		Stage = 0;
		Time = 0f;
	}

	public void Unload() { }

	public static void Draw(SpriteBatch sb)
	{
		if ((Time += 0.03f + Time * 0.01f) > 1f)
		{
			Time = 0;
			Stage++;

			if (Stage > 8)
				Stage = 1;
		}
		sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Gray * AFK.EffectProgress);

		Texture2D body = TextureAssets.Extra[ExtrasID.MoonLordRibs].Value;
		Texture2D arm1 = TextureAssets.Extra[ExtrasID.MoonLordBackarm].Value;
		Texture2D arm2 = TextureAssets.Extra[ExtrasID.MoonLordForearm].Value;
		Texture2D eye = TextureAssets.Extra[ExtrasID.MoonLordEyeMouth].Value;
		Texture2D hand = TextureAssets.Npc[NPCID.MoonLordHand].Value;
		Texture2D head = TextureAssets.Npc[NPCID.MoonLordHead].Value;

		// lol
		void DrawML(Vector2 position, float rotation, Color color, float scale)
		{
			Vector2 backarmPosition = position + new Vector2(214, -160).RotatedBy(rotation);
			sb.Draw(arm1, backarmPosition, null, color, rotation + MathHelper.Pi, new Vector2(arm1.Width * 0.5f, arm1.Height), scale, SpriteEffects.FlipVertically, 0);
			
			Vector2 backarmPosition2 = position + new Vector2(-190, -160).RotatedBy(rotation);
			sb.Draw(arm1, backarmPosition2, null, color, rotation + MathHelper.Pi * 1.1f, new Vector2(arm1.Width * 0.5f, arm1.Height), scale, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0);
			
			sb.Draw(body, position, null, color, rotation, new Vector2(body.Width, body.Height * 0.5f), scale, SpriteEffects.None, 0);
			sb.Draw(body, position, null, color, rotation, new Vector2(0, body.Height * 0.5f), scale, SpriteEffects.FlipHorizontally, 0);

			float headRotation = rotation - MathF.Pow(Time, 0.5f) * 0.1f;
			sb.Draw(head, position, null, color, headRotation, new Vector2(head.Width * 0.5f, head.Height), scale, SpriteEffects.FlipHorizontally, 0);
			
			Vector2 eyePosition = position - new Vector2(0, 250).RotatedBy(headRotation);
			Rectangle eyeFrame = eye.Frame(1, 4, 0, 3 - (int)MathHelper.Clamp(Time * 10, 0, 3));
			sb.Draw(eye, eyePosition, eyeFrame, color, headRotation, eye.Size() * 0.5f, scale, SpriteEffects.FlipHorizontally, 0);

			Vector2 forearmPosition = backarmPosition + new Vector2(10, arm1.Height + 20).RotatedBy(rotation);
			sb.Draw(arm2, forearmPosition, null, color, rotation + 0.3f - Time * 0.05f, new Vector2(arm2.Width * 0.5f, arm2.Height), scale, SpriteEffects.FlipHorizontally, 0);
			
			Vector2 forearmPosition2 = backarmPosition2 + new Vector2(-100, arm1.Height - 20).RotatedBy(rotation);
			float mainArmRotation = rotation - 0.4f - 0.3f * MathF.Pow(MathHelper.Clamp(0.2f + Time * 2, 0, 1), 2 + MathF.Sin(Time * MathF.PI)) + Time * 0.02f;
			sb.Draw(arm2, forearmPosition2, null, color, mainArmRotation, new Vector2(arm2.Width * 0.5f, arm2.Height), scale, SpriteEffects.FlipHorizontally, 0);

			Vector2 handPosition = forearmPosition - new Vector2(0, arm2.Height + 10).RotatedBy(rotation + 0.3f - Time * 0.05f);
			Rectangle handFrame = hand.Frame(1, 4, 0, 3 - (int)MathHelper.Clamp(Time * 9, 0, 3));
			sb.Draw(hand, handPosition, handFrame, color, rotation + 0.2f, new Vector2(handFrame.Width * 0.5f, handFrame.Height * 0.8f), scale, SpriteEffects.FlipHorizontally, 0);
			
			handFrame = hand.Frame(1, 4, 0, 3 - (int)MathHelper.Clamp(Time * 10, 0, 3));
			Vector2 handPosition2 = forearmPosition2 - new Vector2(0, arm2.Height).RotatedBy(mainArmRotation);
			sb.Draw(hand, handPosition2, handFrame, color, rotation - MathHelper.PiOver2 * 0.9f, new Vector2(handFrame.Width * 0.5f, handFrame.Height * 0.8f), scale, SpriteEffects.None, 0);
		}

		int max = Math.Clamp(Stage, 0, 4);
		float rotation = 0.05f - 0.05f * MathF.Pow(1 - Time, 3);
		sb.End(out var ss);
		sb.Begin(ss with { SortMode = SpriteSortMode.Immediate });
		for (int i = Math.Clamp(Stage - 4, 0, 4); i < max; i++)
		{
			Vector2 position = new Vector2(150 + i * 400, Main.screenHeight * 0.7f - MathF.Sin(Time * MathF.PI) * 5);
			
			GameShaders.Armor.GetShaderFromItemId(ItemID.ColorOnlyDye).Apply();
			
			DrawML(position, rotation, Color.White * AFK.EffectProgress, 1);
			
			if (i == max - 1)
				DrawML(position, rotation, Color.Black * AFK.EffectProgress, 1f);
		}
		sb.End();
		sb.Begin(ss);
	}
}