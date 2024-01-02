using MyUtils;
using System.Collections.Generic;
namespace MyCreature
{
    internal class Goal : Creature
    {
        public Goal(RectangleF goalRect) : base(goalRect)
        {
            Intelize();
            SetColisonRect(goalRect);
        }
        public Goal()
        {

        }
        override protected void Intelize()
        {
            m_TexturePath = "Textures/Creatures/Player/Player.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 0;
            m_Resistents = m_Speed / 100 * 50;
            m_MaxSpeed = m_Speed * 1f;
            m_Gravity = 0;
            // down right test
            m_StartPosition = new PointF(200, -300);
            m_DrawRect.X = m_StartPosition.X;
            m_DrawRect.Y = m_StartPosition.Y;

            m_DrawRect.Width = 100;
            m_DrawRect.Height = 100;
            m_LookRight = true;
            m_SourceRect.X = (int)m_DrawRect.X;
            m_SourceRect.Y = (int)m_DrawRect.Y;
            m_SourceRect.Width = (int)m_DrawRect.Width;
            m_SourceRect.Height = (int)m_DrawRect.Height;
        }
        protected override void SetColisonRect(RectangleF drawRect)
        {
            float marginH = drawRect.Height / 100 * 5;
            float width = drawRect.Width / 1.9f;
            float height = drawRect.Height / 1.8f;

            m_ColisionRect.X = drawRect.X + (drawRect.Width - width) / 2;
            m_ColisionRect.Y = drawRect.Y + (drawRect.Height - height) - marginH;
            m_ColisionRect.Width = width;
            m_ColisionRect.Height = height;
        }

        public override void Update(float elapsedSec)
        {
            m_Velocity.X = 0;
            m_Velocity.Y = 0;
            base.UpdateRects(elapsedSec);
        }
    }
}
