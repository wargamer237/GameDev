using MyUtils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MyPlayer
{
    internal class PlayerManger
    {
        Vector2 m_Velocity;
        Direction m_Direction;

        bool m_Jumping;
        float m_JumpTimer;
        float m_JumpDuration;
        public PlayerManger()
        {
            m_JumpDuration = 2;//s
        }
        public Vector2 GetDirection()
        {
            m_Velocity.X = (int)m_Direction.Horzontal;
            m_Velocity.Y = (int)m_Direction.Vertical;
            return m_Velocity;
        }
        public void KeyEvents(KeyboardState keyboardState)
        {
            m_Direction.Horzontal = DirectionValue.None;
            m_Direction.Vertical = DirectionValue.None;
            
            //left right up down keys
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                m_Direction.Horzontal = DirectionValue.Negetive;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (m_Direction.Horzontal == DirectionValue.Negetive)
                    m_Direction.Horzontal = DirectionValue.None;
                else
                m_Direction.Horzontal = DirectionValue.Positve;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                m_Direction.Vertical = DirectionValue.Negetive;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (m_Direction.Vertical == DirectionValue.Positve)
                    m_Direction.Vertical = DirectionValue.None;
                m_Direction.Vertical = DirectionValue.Positve;
            }
            //other controls
        }
    }
}
