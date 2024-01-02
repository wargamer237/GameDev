using Microsoft.Xna.Framework;
using MyUtils;
using System;

namespace MyBlocks
{
    internal class BackGround : Block
    {
        public BackGround()
           : base()
        {
            this.IsBlock(false);
        }
        RectangleF m_MapSize;
        public BackGround(RectangleF rect, int tileType)
            : base(rect)
        {

            this.IsBlock(true);
            this.SetColor(Color.White);
            this.SetTexture($"Textures/Tiles/Background/{tileType}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
        }
        public BackGround(RectangleF rect, RectangleF mapSize)
           : base(rect)
        {
            m_MapSize = mapSize;

            this.IsBlock(true);
            this.SetColor(Color.HotPink);
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
