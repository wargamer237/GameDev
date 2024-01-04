using Microsoft.Xna.Framework;
using MyClass.MyUtils;
using MyClass.MyBlocks.BlocksInterfaces;

namespace MyClass.MyBlocks.Blocks
{
    internal class Platform : Block, ICollidable, IMovable
    {
        PointF m_StartPos;
        PointF m_FirstPoint;
        PointF m_SecondPoint;

        Vector2 m_Velocty;
        Vector2 m_Directon;

        float m_WaitTimer;
        float m_CurantTimer;
        bool m_Waiting;

        float m_Speed;
        public Platform()
           : base()
        {
            this.IsBlock(false);
        }
        public Platform(RectangleF rect, int tileType)
            : base(rect)
        {
            this.IsBlock(true);
            this.SetColor(Color.Red);
            this.SetTexture($"Textures/Tiles/Ground/{tileType}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
            m_StartPos.X = rect.X;
            m_StartPos.Y = rect.Y;
            m_Speed = 200;
        }
        public void SetPath(PointF LeftPointBlocks, PointF RightPointBlocks, float waitTimer)
        {
            m_FirstPoint.X = m_Rect.X - LeftPointBlocks.X * m_Rect.Width;
            m_FirstPoint.Y = m_Rect.Y - LeftPointBlocks.Y * m_Rect.Height;

            m_SecondPoint.X = m_Rect.X + RightPointBlocks.X * m_Rect.Width;
            m_SecondPoint.Y = m_Rect.Y + RightPointBlocks.Y * m_Rect.Height;

            m_WaitTimer = waitTimer;
        }
        private void Waiting(float elapsedSec)
        {
            if (m_Waiting)
            {
                m_CurantTimer += 2 * elapsedSec;
                if (m_CurantTimer > m_WaitTimer)
                {
                    m_CurantTimer = 0;
                    m_Waiting = false;
                }
            }
        }
        bool directionRight = true;
        public void Update(float elapsedSec)
        {
            Waiting(elapsedSec);

            PointF curantPoint = new PointF(m_Rect.X, m_Rect.Y);

            if((curantPoint.X >= m_SecondPoint.X && directionRight)
                ||
               (curantPoint.X <= m_FirstPoint.X && !directionRight))
            {
                m_Waiting = true;
                directionRight = !directionRight;
                m_Speed = -m_Speed;
            }

            if (m_Waiting)
            {
                m_Velocty = Vector2.Zero;
                return;
            }

            m_Velocty.X = m_Speed;

            this.m_Rect.X += m_Velocty.X * elapsedSec;
            this.m_Rect.Y += m_Velocty.Y * elapsedSec;
        }
        public Vector2 GetVelocty()
        {
            return m_Velocty;
        }
    }
}
