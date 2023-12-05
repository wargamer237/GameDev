using MyUtils;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyCreature
{
    internal class AttackCreature : Creature
    {
        //Targeting
        //range
        protected float m_TargetingRange;
        protected float m_AttackRange;
        protected float m_AgroRange;
        //state of attack
        protected bool m_Attack;
        protected bool m_Targeting;
        //other
        protected PointF m_MyCenter;
        protected Vector2 m_TargetDirection;
        protected RectangleF m_AttackRect;
        //attack timers
        protected MyTimer m_AttackStartTimer;
        protected MyTimer m_AttackTimer;
        protected MyTimer m_GotHitTimer;
        //Attack
        protected float m_AttackStartDuration;
        protected float m_AttackDuration;
        protected bool m_AttackLethal;
        protected bool m_AttackHit;
        //constructor
        public AttackCreature() : base()
        {
            m_AttackStartTimer = new MyTimer(0);
        }
        public AttackCreature(RectangleF creatureRect) : base(creatureRect)
        {
            m_AgroRange = 600;
            m_TargetingRange = 200;
            m_AttackRange = 100;
            m_AttackStartTimer = new MyTimer(-1);
            m_AttackTimer = new MyTimer(-1);
            m_GotHitTimer = new MyTimer(-1);
        }
        protected void SetAttackTimers(float startAttack, float attack)
        {
            if (m_AttackStartDuration == startAttack && m_AttackDuration == attack) return;
            m_AttackStartDuration = startAttack;
            m_AttackDuration = attack;
            m_AttackStartTimer = new MyTimer(m_AttackStartDuration);
            m_AttackTimer = new MyTimer(m_AttackDuration);
        }
        public void DebugDraw()
        {
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(m_AttackRect.X - m_AttackRect.Width / 2, m_AttackRect.Y - m_AttackRect.Height / 2);
            if (m_AttackLethal) UtilsStatic.SetColor(Color.Red);
            else UtilsStatic.SetColor(Color.Green);

            UtilsStatic.DrawRect(new RectangleF(m_AttackRect.Width / 2, m_AttackRect.Height / 2, m_AttackRect.Width, m_AttackRect.Height));
            UtilsStatic.ResetColor();
            UtilsStatic.PopMatrix();
        }
        //functions
        public override void Update(float elapsedSec)
        {
            base.Update(elapsedSec);
        }
        public virtual bool GetAttack(Creature creature)
        {
            PointF creatureCenter = UtilsStatic.GetCenterRect(creature.GetRect());
            m_MyCenter = UtilsStatic.GetCenterRect(m_ColisionRect);
            //distance crom eatch center point
            float distance = Math.Abs(creatureCenter.X - m_MyCenter.X) + Math.Abs(creatureCenter.Y - m_MyCenter.Y);
            //is out of AgroRange
            if (distance >= m_AgroRange && m_Targeting) { m_Targeting = false; return false; }
            //if is in range witch direction
            m_TargetDirection = new Vector2(creatureCenter.X - m_MyCenter.X, creatureCenter.Y - m_MyCenter.Y);
            //if is in range?
            if (distance <= m_TargetingRange) m_Targeting = true;

            if (!m_Attack && distance <= m_AttackRange) m_Attack = true;
            //IF IN ATACKRECT THEN TRUE CHANGE LATER
            if (m_Attack && m_AttackLethal)
            {
                if (Colision.RectInRect(m_AttackRect, creature.GetRect()))
                {
                    m_AttackHit = true;
                    return true;
                }
            }

            return false;
        }
        protected virtual bool HandelTargeting()
        {
            if (!m_Targeting) return m_Targeting;
            //if target right
            if (m_TargetDirection.X > 0)
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
        protected virtual bool HandelAttack(RectangleF attackRect, PointF creatureCenter)
        {
            if (!m_Attack) return m_Attack;

            // Attack to the right
            if (m_TargetDirection.X < 0)
            {
                m_AttackRect.X = creatureCenter.X - attackRect.X - attackRect.Width;
                m_AttackRect.Y = creatureCenter.Y + attackRect.Y;
                m_AttackRect.Width = attackRect.Width;
                m_AttackRect.Height = attackRect.Height;
            }
            // Attack to the left
            if (m_TargetDirection.X > 0)
            {
                m_AttackRect.X = creatureCenter.X + attackRect.X;
                m_AttackRect.Y = creatureCenter.Y + attackRect.Y;
                m_AttackRect.Width = attackRect.Width;
                m_AttackRect.Height = attackRect.Height;
            }

            return m_Attack;
        }
        protected bool Attack(float elapsedSec)
        {
            if (!m_Attack) return m_Attack;
            if (m_AttackStartTimer.IsRun(elapsedSec)) return m_AttackLethal;

            if (m_AttackHit) m_AttackLethal = false;
            else m_AttackLethal = true;

            if (m_AttackTimer.IsRun(elapsedSec)) return m_AttackLethal;

            m_AttackLethal = false;
            m_AttackStartTimer.Reset();
            m_AttackTimer.Reset();
            SetAttackTimers(m_AttackStartDuration, m_AttackDuration);
            m_Attack = false;
            m_AttackHit = false;
            return m_AttackLethal;
        }
        public bool IsLeathalAttack()
        {
            return m_AttackLethal;
        }
    }
}
