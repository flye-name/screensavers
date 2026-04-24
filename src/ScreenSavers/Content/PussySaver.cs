using Microsoft.Build.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Peripherals.RGB;
using ScreenSavers.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;

namespace ScreenSavers.Content;

public class PussySaver : ILoadable
{
    public class Cat(Vector2 pos, Vector2 vel, string tex)
    {
        public Vector2 Vel = vel;
        public Vector2 Pos = pos;
        public string Tex = tex;
    }

    public static List<Cat> Cats
    {
        get;
        internal set;
    } = [];

    public void Load(Mod mod)
    {
        Cats.Add(new Cat(Main.rand.NextVector2FromRectangle(new(0, 0, Main.screenWidth, Main.screenHeight)), Main.rand.NextVector2CircularEdge(1, 1) * 6, "ScreenSavers/Assets/Images/Cat_6"));

        for (int k = 0; k < 299; k++)
            Cats.Add(new Cat(Main.rand.NextVector2FromRectangle(new(0, 0, Main.screenWidth, Main.screenHeight)), Main.rand.NextVector2CircularEdge(1, 1) * 6, $"ScreenSavers/Assets/Images/Cat_{Main.rand.Next(6)}"));
    }

	public void Unload()
	{

	}

	public static void Draw(SpriteBatch spriteBatch)
    {
        if (AFK.EffectProgress < 0)
            return;

        spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * AFK.EffectProgress);

        for (int k = 0; k < Cats.Count; k++)
        {
            var c = Cats[k];
            Texture2D texture = ModContent.Request<Texture2D>(c.Tex).Value;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);

            spriteBatch.Draw(texture, c.Pos, texture.Bounds, Color.White * AFK.EffectProgress, 0, texture.Size() * 0.5f, 2, c.Vel.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin();

            c.Pos += c.Vel;

            float pad = 50f;
            Vector2 vel = Main.rand.NextVector2CircularEdge(1, 1) * 6;

            if (c.Pos.X > Main.screenWidth + pad)
            {
                c.Pos.X = -pad;
                c.Vel = vel;
            }
            else if (c.Pos.X < -pad)
            {
                c.Pos.X = Main.screenWidth + pad;
                c.Vel = vel;
            }

            if (c.Pos.Y > Main.screenHeight + pad)
            {
                c.Pos.Y = -pad;
                c.Vel = vel;
            }
            else if (c.Pos.Y < -pad)
            {
                c.Pos.Y = Main.screenHeight + pad;
                c.Vel = vel;
            }
        }
    }
}