using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyClass.MyUtils
{
    internal class DrawClass
    {
        public static Texture2D GetTexture(string path, SpriteBatch graph)
        {
            //if (string.IsNullOrEmpty(path)) return null;
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
        public static void DrawRect(RectangleF rect, Color color, SpriteBatch graph, Texture2D texture, SpriteEffects direction = SpriteEffects.None)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rect.X;
            r.Y = (int)rect.Y;
            r.Width = (int)rect.Width;
            r.Height = (int)rect.Height;
            graph.Draw(texture, r, null, color, 0f, new Vector2(0, 0), direction, 0f);
        }
        public static void DrawRect(RectangleF rect, Color color, SpriteBatch graph, Texture2D texture, Rectangle sourcseRect, SpriteEffects direction = SpriteEffects.None)
        {
            Rectangle r = new Rectangle();
            r.X = (int)rect.X;
            r.Y = (int)rect.Y;
            r.Width = (int)rect.Width;
            r.Height = (int)rect.Height;
            graph.Draw(texture, r, sourcseRect, color, 0f, new Vector2(0,0), direction, 0f);
            //graph.Draw(texture, r, sourcseRect, color);
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
