using System.Linq;
using Microsoft.Xna.Framework;
using ScreenSavers.Common;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ScreenSavers.Core;

public class AFK : ModSystem
{
	public static bool IsAFK;
	public static float EffectProgress;
	public static uint TimeSinceLastInput;

	public override void OnWorldLoad()
	{
		IsAFK = false;
		TimeSinceLastInput = 0;
		EffectProgress = 0;
	}

	public override void UpdateUI(GameTime gameTime)
	{
		if (Main.gameMenu) return;

		if (!IsAFK)
		{
			EffectProgress = MathHelper.Lerp(EffectProgress, 0, 0.1f);
			if (EffectProgress < 0.05f)
				EffectProgress = 0;
		}

		bool doingAnything = !PlayerInput.MouseInfo.Equals(PlayerInput.MouseInfoOld) || PlayerInput.GetPressedKeys().Any();

		if (doingAnything)
		{
			TimeSinceLastInput = 0;
			return;
		}

		TimeSinceLastInput++;
		if (TimeSinceLastInput > ModContent.GetInstance<Config>().TimeForAFK * 3600 || Main.mouseRight)
			IsAFK = true;
		
		if (IsAFK) 
		{ 
			EffectProgress = MathHelper.Lerp(EffectProgress, 1, 0.1f);
		}
	}
}