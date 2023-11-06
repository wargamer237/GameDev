﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyUtils
{
    internal class DrawClass
    {
        public static Texture2D GetTexture(string path, SpriteBatch graph)
        {
            /*if(!File.Exists(path)) return null;*/
            var stream = TitleContainer.OpenStream($"{path}");
            Texture2D texture = Texture2D.FromStream(graph.GraphicsDevice, stream);
            return texture;
        }
        public static void DrawTexture(RectangleF rect, Color color, SpriteBatch graph, Texture2D texture)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rect.X;
            r.Y = (int)rect.Y;
            r.Width = (int)rect.Width;
            r.Height = (int)rect.Height;
            graph.Draw(texture, r, color);
        }
        public static void DrawRect(RectangleF rect, Color color, SpriteBatch graph, Texture2D texture)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rect.X;
            r.Y = (int)rect.Y;
            r.Width = (int)rect.Width;
            r.Height = (int)rect.Height;

            graph.Draw(texture, r, color);
        }
        public static void DrawRect(RectangleF rect, Color color, SpriteBatch graph, Texture2D texture, Rectangle sourcseRect)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rect.X;
            r.Y = (int)rect.Y;
            r.Width = (int)rect.Width;
            r.Height = (int)rect.Height;

            graph.Draw(texture, r, sourcseRect, color);
        }
    }
}