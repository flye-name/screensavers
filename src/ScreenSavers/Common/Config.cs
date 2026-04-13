using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ScreenSavers.Common;

public class Config : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ClientSide;
	
	[Range(1f, 60), DefaultValue(15)]
	public int TimeForAFK = 15;
}