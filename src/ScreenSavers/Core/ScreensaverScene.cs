using Terraria;
using Terraria.ModLoader;

namespace ScreenSavers.Core;

public class ScreensaverScene : ModSceneEffect
{
	public override bool IsSceneEffectActive(Player player) => AFK.IsAFK;

	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
}