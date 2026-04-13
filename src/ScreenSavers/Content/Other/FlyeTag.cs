using Daybreak.Common.Features.Authorship;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ScreenSavers.Content.Other;

public class FlyeTag : AuthorTag
{
	public override string Texture => Assets.Images.UI.FlyeTag.KEY;

	public override void DrawIcon(SpriteBatch sb, Vector2 position)
	{
		Texture2D icon = Assets.Images.UI.FlyeTag.Asset.Value;
		
		for (int i = 0; i < 4; i++) 
			sb.Draw(icon, position + icon.Size() / 2f - new Vector2(0, 2), null, Color.White with { A = 0 } * (i / 4f) * 0.65f, (float)Main.timeForVisualEffects * -0.2f + i * 0.5f, icon.Size() / 2f, 1f, SpriteEffects.None, 0);
	}
}