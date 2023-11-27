using Microsoft.Xna.Framework;
using MyCreature;
using MyUtils;
using System;
using System.Collections.Generic;



namespace MyCreature
{
    internal class Robot : Creature, HasAnimations<Robot.CurantMovment>
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
        //jump
        bool m_Jump;
        int m_JumpingHeight;
        bool m_IsJumping;
        float m_JumpMultiplier;
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
        public void DebugDraw()
        {
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(m_AttackRect.X - m_AttackRect.Width / 2, m_AttackRect.Y - m_AttackRect.Height / 2);
            if(m_AttackIsLethal)UtilsStatic.SetColor(Color.Red);
            else UtilsStatic.SetColor(Color.Green);
            UtilsStatic.DrawRect(new RectangleF(m_AttackRect.Width / 2, m_AttackRect.Height / 2, m_AttackRect.Width, m_AttackRect.Height));
            UtilsStatic.ResetColor();
            UtilsStatic.PopMatrix();
        }
            //CONSTRUCTOR
        public Robot(RectangleF playerRect) : base(playerRect)
        {
            m_WaitTimer = new MyTimer(m_MaxWait);
            m_WaitTimer.Reset();
            m_AttackStartDuration = 0.8f;
            m_AttackDuration = 1;
            m_AttackStartTimer = new MyTimer(m_AttackStartDuration);
            m_AttackTimer = new MyTimer(m_AttackDuration);
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
            m_TexturePath = "Textures/Creatures/Robot/Robot1.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 100;
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
            m_IsJumping = false;
            m_JumpMultiplier = 0;
            IntelizeAnimations(8, 5);
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
        //Targeting
        float m_TargetingRange;
        float m_AttackRange;
        float m_AgroRange;
        bool m_Attack;
        bool m_Targeting;
        Vector2 m_TargetDirection;
        public bool SettTarget(Creature creature)
        {
            PointF creatureCenter = UtilsStatic.GetCenterRect(creature.GetRect());
            PointF myCenter = UtilsStatic.GetCenterRect(m_ColisionRect);
            m_AgroRange = 300;
            m_TargetingRange = 200;
            m_AttackRange = 100;
            //distance crom eatch center point
            float distance = Math.Abs(creatureCenter.X - myCenter.X) + Math.Abs(creatureCenter.Y - myCenter.Y);
            //is out of AgroRange
            if (distance >= m_AgroRange && m_Targeting) { m_Targeting = false; return false; }
            //if is in range witch direction
            m_TargetDirection = new Vector2(creatureCenter.X - myCenter.X, creatureCenter.Y - myCenter.Y);
            //if is in range?
            if (distance <= m_TargetingRange) m_Targeting = true;

            if (!m_Attack && distance <= m_AttackRange) m_Attack = true;
            //IF IN ATACKRECT THEN TRUE CHANGE LATER
            if (m_Attack && m_AttackIsLethal)
            {
                if (Colision.RectInRect(m_AttackRect,creature.GetRect()))
                {
                    m_AtackHit = true;
                    return true;
                }
            }

            return false;
        }
        private bool HandelTargeting()
        {
            if (!m_Targeting) return m_Targeting;
            //if target right
            if(m_TargetDirection.X > 0)
            {
                if (m_Speed < 0) m_Speed = -m_Speed;
                m_LookRight = true;
            }
            //if target left
            else if (m_TargetDirection.X < 0)
            {
                if (m_Speed > 0) m_Speed = -m_Speed;
                m_LookRight = false;
            }
            return m_Targeting;
        }
        RectangleF m_AttackRect = new RectangleF(0,0,0,0);
        private bool HandelAttack()
        {
            if (!m_Attack) return m_Attack;
            float wMargin = m_ColisionRect.Width / 100 * 20;
            RectangleF atackSize = new RectangleF(
                0, 0, m_ColisionRect.Width/2 + wMargin, m_ColisionRect.Height *1.5f);
            
            // Attack to the right
            if (m_TargetDirection.X < 0)
            {
                m_AttackRect.X = m_ColisionRect.X - atackSize.Width + wMargin;
                m_AttackRect.Y = m_ColisionRect.Y + (m_ColisionRect.Height - atackSize.Height);
                m_AttackRect.Width = atackSize.Width;
                m_AttackRect.Height = atackSize.Height;
            }
            // Attack to the left
            if (m_TargetDirection.X > 0)
            {
                m_AttackRect.X = m_ColisionRect.X + m_ColisionRect.Width - wMargin;
                m_AttackRect.Y = m_ColisionRect.Y + (m_ColisionRect.Height - atackSize.Height);
                m_AttackRect.Width = atackSize.Width;
                m_AttackRect.Height = atackSize.Height;
            }

            return m_Attack;
        }
        MyTimer m_AttackStartTimer;
        MyTimer m_AttackTimer;
        float m_AttackStartDuration;
        float m_AttackDuration;
        bool m_AttackIsLethal;
        bool m_AtackHit;
        private bool Attack(float elapsedSec)
        {
            if (!m_Attack) return m_Attack;
            if (m_AttackStartTimer.IsRun(elapsedSec)) return m_AttackIsLethal;
            
            if (m_AtackHit) m_AttackIsLethal = false;
            else m_AttackIsLethal = true;

            if (m_AttackTimer.IsRun(elapsedSec)) return m_AttackIsLethal;

            m_AttackIsLethal = false;
            m_AttackStartTimer.Reset();
            m_AttackTimer.Reset();
            m_Attack = false;
            m_AtackHit = false;
            return m_AttackIsLethal;
        }
        //UPDATES
        public override void Update(float elapsedSec)
        {
            float agroBoost = 1;
            if (HandelTargeting())
            {
                agroBoost = 2;
                if (HandelAttack())
                {
                    agroBoost = 0;
                    if (Attack(elapsedSec))
                    {
                        
                    }
                }
            }


            StayOnPlatform(m_Vertexs, elapsedSec);
            if (m_Waking) m_Velocity.X += m_Speed * elapsedSec * agroBoost;
            
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
            float left = drawRect.Width / 100 * 10;
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
