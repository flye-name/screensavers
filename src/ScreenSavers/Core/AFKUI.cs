using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ScreenSavers.Common;
using ScreenSavers.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ScreenSavers.Core;

public class AFKUI : ModSystem
{
	public static ScreensaverMode Mode = ScreensaverMode.Pussy;
	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	{
		int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 4"));

		SpriteBatch sb = Main.spriteBatch;
		layers.Insert(index, new LegacyGameInterfaceLayer("ScreenSavers: UI", () =>
		{
			switch (Mode)
			{
				case ScreensaverMode.DVD: 
					DVDSaver.Draw(sb); 
					break;
                case ScreensaverMode.Pussy:
                    PussySaver.Draw(sb);
                    break;
            }
			return true;
		}, InterfaceScaleType.UI));
	}
}