using MyClass.MyUtils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MyCreature;
namespace MyPlayer
{
    internal class PlayerManger
    {        
        Vector2 m_DirectionVelocity;
        Direction m_Direction;
        Vector2 m_ExtraDirection;
        Player.CurantMovment m_AttackType;
        public PlayerManger()
        {
            m_AttackType = Player.CurantMovment.None;
        }
        public Vector2 GetDirection()
        {
            m_DirectionVelocity.X = (int)m_Direction.Horzontal * m_ExtraDirection.X;
            m_DirectionVelocity.Y = (int)m_Direction.Vertical;
            return m_DirectionVelocity;
        }
        public void KeyEvents(KeyboardState keyboardState)
        {
            m_Direction.Horzontal = DirectionValue.None;
            m_Direction.Vertical = DirectionValue.None;
            m_ExtraDirection.X = 1;
            m_ExtraDirection.Y = 1;
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
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                m_ExtraDirection.X = 2;
            }
            //attacks Keys
            m_AttackType = Player.CurantMovment.None;
            if (keyboardState.IsKeyDown(Keys.A))
            {
                m_AttackType = Player.CurantMovment.AttackOne;
            }
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                m_AttackType = Player.CurantMovment.AttackTwo;
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                m_AttackType = Player.CurantMovment.AttackThree;
            }

            //other controls
        }
        public void Update(ref Player player, float elapsedSec)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            KeyEvents(keyboardState);
            player.SetAttack(m_AttackType);
            player.SetDirection(GetDirection());
            player.Update(elapsedSec);
        }
    }
}
