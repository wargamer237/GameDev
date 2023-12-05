using Microsoft.Xna.Framework;
using MyCreature;
using MyUtils;
using System;
using System.Collections.Generic;



namespace MyCreature
{
    internal class Robot : AttackCreature, HasAnimations<Robot.CurantMovment>
    {
        //ENUMS 
        //ACTION ENUM
        enum CurantMovment
        {
            Idel = 0,
            Run = 1,
            Attack = 2,
            IdelTwo = 3,
            Death = 4,
        }
        //STANDARS
        Vector2 m_Direction;
        //ANIMATIONS VARS
        AnimationHandeler m_AnimationHandeler;
        //ROBOT VARS
        //movment
        float m_MaxWait = 2;
        MyTimer m_WaitTimer;
        bool m_OnGround = false;
        bool m_HitWall = false;
        bool m_IsWaiting = true;
        bool m_Waking = false;
       
            //CONSTRUCTOR
        public Robot(RectangleF playerRect) : base(new RectangleF(playerRect.X, playerRect.Y, playerRect.Width*1.5f, playerRect.Height))
        {
            Intelize();
        }
        public Robot()
        {
            Intelize();
            SetColisonRect(m_DrawRect);
            IntelizeVertexs();
        }
        //INTELIZE
        override protected void Intelize()
        {
            //Standards
            m_TexturePath = "Textures/Creatures/Robot/Robot1.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 100;
            m_Resistents = m_Speed / 100 * 50;
            m_MaxSpeed = m_Speed * 1f;
            m_Gravity = 800;
            //directions look start
            m_LookRight = true;
            IntelizeAnimations(8, 5);
             //Attacks INtelize
            m_WaitTimer = new MyTimer(m_MaxWait);
            m_WaitTimer.Reset();
            m_AttackStartDuration = 0.8f;
            m_AttackDuration = 1;
            m_AttackStartTimer = new MyTimer(m_AttackStartDuration);
            m_AttackTimer = new MyTimer(m_AttackDuration);
            m_GotHitTimer = new MyTimer(0.3f);

            m_AgroRange = 600;
            m_TargetingRange = 300;
            m_AttackRange = 100;
        }
        //Macanics + brain
        //Stay on platform
        void StayOnPlatform(List<Vertexs> vertexs, float elapsedSec)
        {
            if (m_WaitTimer.IsRun(elapsedSec)) return;
            m_Waking = true;
            //5right,7left
            if (vertexs[5].Colided && vertexs[7].Colided) m_OnGround = true;
            if (!m_OnGround) return;
            if (!vertexs[6].Colided && !vertexs[4].Colided) m_HitWall = false;
            if (m_HitWall) return;
            if (!vertexs[5].Colided || !vertexs[7].Colided || vertexs[6].Colided || vertexs[4].Colided)
            {
                m_HitWall = true;
                m_OnGround = false;
                m_WaitTimer.Reset();
                m_Waking = false;
                m_Speed = -m_Speed;
                m_Velocity.X = 0;
            }
        }
        //Get AttackRect 
        private RectangleF GetAttackRect()
        {
            float wMargin = m_ColisionRect.Width / 100 * 30;
            float hMargin = (m_ColisionRect.Height - m_ColisionRect.Height * 1.5f);

            float x = m_ColisionRect.Width / 2 - wMargin;
            float y = -m_ColisionRect.Height / 2 + hMargin;
            float w = m_ColisionRect.Width / 2 + wMargin;
            float h = m_ColisionRect.Height - hMargin;

            RectangleF attackRect = new RectangleF(x, y, w, h);

            return attackRect;
        }
        //UPDATES
        public override void Update(float elapsedSec)
        {
            if (!m_GotHitTimer.IsRun(elapsedSec) && m_GotHit && m_Attack) m_GotHitTimer.Reset();
            if (!m_GotHitTimer.IsRun(elapsedSec)) m_GotHit = false;

            float agroBoost = 1;
            if (base.HandelTargeting())
            {
                agroBoost = 2;
                if (base.HandelAttack(GetAttackRect(), m_MyCenter))
                {
                    agroBoost = 0;
                    if (m_GotHit)
                    {
                        m_Attack = false;
                    }
                    base.Attack(elapsedSec);
                }
            }

            StayOnPlatform(m_Vertexs, elapsedSec);
            if (m_Waking) m_Velocity.X += m_Speed * elapsedSec * agroBoost;
            else m_Velocity.X = 0;
            m_Velocity.X = base.SpeedLimit(m_Velocity.X, m_MaxSpeed * agroBoost);
            UpdateTexture(elapsedSec, 0.2f);

            base.Update(elapsedSec);
        }
        //IAnimations
        private void IntelizeAnimations(int maxWidth, int maxHeight)
        {
            m_AnimationHandeler = new AnimationHandeler(m_TexturePath, new Point(maxWidth, maxHeight));
            //IDEL(1)
            m_AnimationHandeler.AddRow(2);
            //RUN
            m_AnimationHandeler.AddRow(8);
            //ATACK(1,2,3)
            m_AnimationHandeler.AddRow(7);
            //IDEL(2)
            m_AnimationHandeler.AddRow(4);
            //DEATH
            m_AnimationHandeler.AddRow(6);
        }
        private void SetAnimation(CurantMovment movment)
        {
            m_AnimationHandeler.SetAnimation((int)movment);
        }
        private void UpdateTexture(float elapsedSec, float animationDuration)
        {
            SetAnimation(GetAnimationType());

            m_AnimationHandeler.UpdateTexture(elapsedSec, animationDuration);
            m_SourceRect = m_AnimationHandeler.GetSourceRect();
            m_SourceRect.Width -= m_SourceRect.Width / 100 * 10;
            m_SourceRect.Height -= m_SourceRect.Height / 100 * 20;
            m_SourceRect.Y += m_SourceRect.Height / 100 * 20;
        }
        //SETTERS
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
        public void SetDirection(Vector2 velocty)
        {
            m_Direction = velocty;
        }
        //GETTERS
        private CurantMovment GetAnimationType()
        {
            Vector2 velocty = m_Velocity;
            if (m_Attack)
            {
                return CurantMovment.Attack;
            }
            if (velocty.X == 0 && velocty.Y == 0)
            {
                //stand stil
                return CurantMovment.Idel;
            }
            if (velocty.Y > 0)
            {
                return CurantMovment.IdelTwo;
            }
            if (velocty.Y < 0)
            {
                return CurantMovment.Idel;
            }
            if (velocty.X > 0 || velocty.X < 0)
            {
                return CurantMovment.Run;
            }
            return CurantMovment.Idel;
        }
    }
}
