using Microsoft.Xna.Framework;
using MyUtils;
using System.Collections.Generic;
using System;

namespace MyCreature
{
    internal class Player : AttackCreature, HasAnimations<Player.CurantMovment>
    {
        //ENUMS 
        //ACTION ENUM
        public enum CurantMovment // set row of texture
        {
            Idel = 0,
            Run = 1,
            AttackOne = 2,
            AttackTwo = 3,
            AttackThree = 4,
            Jump = 5,
            Fall = 6,
            Death = 14,
            None = -1
        }
        bool m_EndAnimation;
        //Movment keys given speed 1,1 
        Vector2 m_Direction;
        //jump
        bool m_Jump;
        int m_JumpingHeight;
        float m_JumpMultiplier;
        //ANIMATIONS VARS
        AnimationHandeler m_AnimationHandeler;

        //CONSTRUCTOR
        public Player(RectangleF playerRect): base(playerRect)
        {
            
        }
        public Player()
        {

        }
        //INTELIZE
        override protected void Intelize()
        {
            m_TexturePath = "Textures/Creatures/Player/Player.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 400;
            m_Resistents = m_Speed / 100 * 50;
            m_MaxSpeed = m_Speed * 1f;
            m_Gravity = 800;
            m_JumpingHeight = 22;
            // down right test
            m_StartPosition = new PointF(200, -300);
            m_DrawRect.X = m_StartPosition.X;
            m_DrawRect.Y = m_StartPosition.Y;

            m_DrawRect.Width = 100;
            m_DrawRect.Height = 100;
            m_LookRight = true;
            m_Jump = false;
            m_JumpMultiplier = 0;
            IntelizeAnimations(13, 15);
        }
        //Macanics Private
        private float ResitenceCalc(float velocity, float elapsedSec)
        {
            float speed = m_Speed;
            if (!(m_Velocity.X > 0 && m_Direction.X < 0) && !(m_Velocity.X < 0 && m_Direction.X > 0))
            {
                if (m_Direction.X != 0) return velocity;
                if (velocity == 0) return velocity;
            }
            else
            {
                speed *= 2;
            }

            float absX = Math.Abs(velocity);

            if (absX < m_Resistents) return 0;

            if (velocity > 0)
            {
                velocity += -speed * elapsedSec;
            }
            else if (velocity < 0)
            {
                velocity += speed * elapsedSec;
            }

            return velocity;
        }
        private void Jump(float elapsedSec)
        {
            //IF ON GROUND GO BACK 
            if ((m_Velocity.Y == 0 && m_Direction.Y >= 0) || (m_Direction.Y >= 0 && m_Jump))
            {
                //IF JUMP WAS NOT USABLE MAKE IT USABLE
                if (!m_Jump)
                {
                    m_Jump = true;
                    m_AttackOneJump = true;
                    m_AttackTwoDash = true;
                }
                return;
            }
            //IF TRY TO JUMP AND JUMP IS TRUE START JUMP
            if (m_Jump == true)
            {
                m_JumpMultiplier = m_Direction.Y;
            }
            //PLAYER IS FALLING DOWN, FALLING DOWN, FALLING DOWN
            //PLAYER IS FALLING DOWN MY PORE READER
            if (!m_Jump) return;
            //GO UP, JUMP
            m_Velocity.Y = m_JumpMultiplier * (m_Gravity * m_JumpingHeight) * 2 * elapsedSec;
            m_Jump = false;
        }
        Vector2 m_AttackVelocity;
        CurantMovment m_CurantAttack;

        public void SetAttack(CurantMovment attack)
        {
            if (attack == CurantMovment.None || m_Attack) return;
            switch (attack)
            {
                case CurantMovment.AttackOne:
                    m_CurantAttack = CurantMovment.AttackOne;
                    break;
                case CurantMovment.AttackTwo:
                    m_CurantAttack = CurantMovment.AttackTwo;
                    break;
                case CurantMovment.AttackThree:
                    m_CurantAttack = CurantMovment.AttackThree;
                    break;
            }
            return;
        }
        bool m_AttackOneJump;
        bool m_AttackTwoDash;
        private RectangleF GetAttackRect(CurantMovment attack)
        {
            float w = m_ColisionRect.Width;
            float h = m_ColisionRect.Height;
            RectangleF attackRect = new RectangleF();
            switch (attack)
            {
                case CurantMovment.AttackOne:
                    base.SetAttackTimers(0.5f, 1);
                    attackRect = new RectangleF(w/100*20,-h/2,w,h);
                    m_Jump = m_AttackOneJump;
                    m_AttackOneJump = false;
                    if (m_Jump) m_Direction.Y = -1;
                    m_Attack = true;

                    break;
                case CurantMovment.AttackTwo:
                    base.SetAttackTimers(0.2f, 0.8f);
                    attackRect = new RectangleF(w / 100 * 20, -h / 2, w, h);
                    m_Attack = true;
                    m_Velocity.X = 0;
                    break;
                case CurantMovment.AttackThree:
                    base.SetAttackTimers(0.5f, 1f);
                    attackRect = new RectangleF(w / 100 * 20, -h / 100 * 10, w, h / 2);
                    m_Velocity.X += m_MaxSpeed / 3f * m_TargetDirection.X;
                    if (m_AttackTwoDash)
                    {
                        m_AttackTwoDash = false;
                    }
                    m_Attack = true;
                    break;
            }

            return attackRect;
        }
        private void AttackRectUpdate(float elapsedSec)
        {
            m_MyCenter = UtilsStatic.GetCenterRect(m_ColisionRect);
            if (base.HandelAttack(GetAttackRect(m_CurantAttack), m_MyCenter))
            {
                bool leathal = base.Attack(elapsedSec);
                if (leathal)
                {
                    m_AttackVelocity.X -= m_AttackVelocity.X / 10;
                }
                if (!m_Attack && m_CurantAttack != CurantMovment.None)
                {
                    m_CurantAttack = CurantMovment.None;
                    m_AttackVelocity = Vector2.Zero;
                    m_AttackTwoDash = true;
                }
            }
            
        }
        //UPDATES

        public override void Update(float elapsedSec)
        {
            if (m_LookRight)
                m_TargetDirection.X = 1;
            else m_TargetDirection.X = -1;

            AttackRectUpdate(elapsedSec);

            //SLOW DOWN player when stops moving Horzontal X
            if (m_AttackTwoDash)
            m_Velocity.X = ResitenceCalc(m_Velocity.X, elapsedSec);
            //JUMP
            Jump(elapsedSec);
            m_Velocity.X += m_Direction.X * m_Speed * elapsedSec;
            m_Velocity.Y += m_Gravity * elapsedSec;
            //SPEED LIMIT
            m_Velocity.X = SpeedLimit(m_Velocity.X, m_MaxSpeed, m_Direction.X);
            m_Velocity.Y = SpeedLimit(m_Velocity.Y, m_Gravity);
            m_Velocity.X += m_AttackVelocity.X;
            //CHECK COLISIONS (Creature standard)
            UpdateColision(ref m_DrawRect);
            UpdateRects(elapsedSec);
            //TEXTURE UPDATE ANIMATIONS
            UpdateTexture(elapsedSec, 0.18f);
        }
        /// <summary>
        /// Its working!!. it check colison of the vertics and 
        /// if it hit somthing and the directions of the velocty is the same 
        /// then it applys the efect of hiting a wal (snap the wall in 2 steps)
        /// if you change direction it will check that sinde IF THERE IS COLISION
        /// (ps: all vertics are always checking for colison: see maphandeler)
        /// </summary>
        //IAnimations
        private void IntelizeAnimations(int maxWidth, int maxHeight)
        {
            m_AnimationHandeler = new AnimationHandeler(m_TexturePath, new Point(maxWidth, maxHeight));
            //IDEL
            m_AnimationHandeler.AddRow(13);
            //RUN
            m_AnimationHandeler.AddRow(8);
            //ATACK(1,2,3)
            m_AnimationHandeler.AddRow(10);
            m_AnimationHandeler.AddRow(10);
            m_AnimationHandeler.AddRow(10);
            //JUMP
            m_AnimationHandeler.AddRow(6);
            //FALL
            m_AnimationHandeler.AddRow(4);
            //OTHER / not used
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            //DEATH
            m_AnimationHandeler.AddRow(9);
        }
        private void UpdateTexture(float elapsedSec, float animationDuration)
        {
            m_EndAnimation = false;
            SetAnimation(GetAnimationType());
            m_EndAnimation = m_AnimationHandeler.UpdateTexture(elapsedSec, animationDuration);
            m_SourceRect = m_AnimationHandeler.GetSourceRect();
            m_SourceRect.Width -= m_SourceRect.Width / 100 * 10;
            m_SourceRect.Height -= m_SourceRect.Height / 100 * 20;
            m_SourceRect.Y += m_SourceRect.Height / 100 * 20;
        }
        private void SetAnimation(CurantMovment movment)
        {
            switch (movment)
            {
                case CurantMovment.Idel:
                    // Code to handle idle state
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Idel);
                    break;
                case CurantMovment.Run:
                    // Code to handle running
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Run);
                    break;
                case CurantMovment.AttackOne:
                    // Code for attack one
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.AttackOne);
                    break;
                case CurantMovment.AttackTwo:
                    // Code for attack two
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.AttackTwo);
                    break;
                case CurantMovment.AttackThree:
                    // Code for attack three
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.AttackThree);
                    break;
                case CurantMovment.Jump:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Jump, 0, 2, 4);
                    break;
                case CurantMovment.Fall:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Fall, 0, 1, 4);
                    break;
                case CurantMovment.Death:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Death);
                    break;
                default:
                    // Code to handle an undefined movement
                    break;
            }
            //m_AnimationHandeler.SetAnimation((int)movment);
        }
       
        //SETTERS

        public void SetDirection(Vector2 velocty)
        {
            m_Direction = velocty;
        }
        //GETTERS
        private CurantMovment GetAnimationType()
        {
            Vector2 velocty = m_Velocity;
            if(m_CurantAttack == CurantMovment.AttackOne 
                || m_CurantAttack == CurantMovment.AttackTwo
                || m_CurantAttack == CurantMovment.AttackThree)
            {
                return m_CurantAttack;
            }
                if (m_Dead) return CurantMovment.Death;
            if (velocty.X == 0 && velocty.Y == 0)
            {
                //stand stil
                return CurantMovment.Idel;
            }
            if (velocty.Y > 0)
            {
                return CurantMovment.Fall;
            }
            if (velocty.Y < 0)
            {
                return CurantMovment.Jump;
            }
            if (velocty.X > 0 || velocty.X < 0)
            {
                return CurantMovment.Run;
            }
            return CurantMovment.Idel;
        }
        public override bool GetDeathState()
        {
            if (m_EndAnimation && m_Dead)
            {
                return m_Dead;
            }
            return false;
        }
    }
}
