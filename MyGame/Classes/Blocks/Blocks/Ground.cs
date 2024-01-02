using MyBlocks;
using Microsoft.Xna.Framework;
using MyUtils;

namespace MyBlocks
{
    internal class Ground : Block, ICollidable
    {
        public Ground() 
            : base()
        {
            this.IsBlock(false);
        }
        RectangleF m_MapSize;
        public Ground(RectangleF rect, int tileType) 
            : base(rect)
        {
            this.IsBlock(true);
            this.SetColor(Color.Yellow);
            this.SetTexture($"Textures/Tiles/Ground/{tileType}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
        }
        public Ground(RectangleF rect, RectangleF mapSize)
           : base(rect)
        {
            m_MapSize = mapSize;
            this.IsBlock(true);
            this.SetColor(Color.White);
            this.SetTexture($"Textures/Tiles/Background/{5}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
        }
        public void DrawBackground()
        {
            m_Rect.X = m_MapSize.X;
            m_Rect.Y = m_MapSize.Y;
            bool drawing = true;
            while (drawing)
            {
                base.Draw();
                m_Rect.X += m_Rect.Width;
                if (m_Rect.X > m_MapSize.Width)
                {
                    if (m_Rect.Y > m_MapSize.Height) return;
                    m_Rect.X = m_MapSize.X;
                    m_Rect.Y += m_Rect.Height;
                }
            }
        }
    }
}