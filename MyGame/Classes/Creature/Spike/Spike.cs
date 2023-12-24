using Microsoft.Xna.Framework;
using MyUtils;
using System;
using System.Collections.Generic;

namespace MyCreature
{
    internal class Spike : AttackCreature , HasAnimations<Spike.CurantMovment>
    {
        enum CurantMovment
        {
            Stab = 0,
            Wait = 1
        }
        public enum SpikeDirection
        {
            left,
            top,
            right,
            bottom
        }
        SpikeDirection m_Drection;
        AnimationHandeler m_AnimationHandeler;
        bool m_Cooldown;
        MyTimer m_CooldownTimer;
        public Spike()
        {

        }
        public Spike(RectangleF creatureRect) : base(new RectangleF(creatureRect.X, creatureRect.Y, creatureRect.Width, creatureRect.Height))
        {
            Intelize();
            SetColisonRect(m_DrawRect);
        }
        public Spike(RectangleF creatureRect, SpikeDirection direction) : base(new RectangleF(creatureRect.X, creatureRect.Y, creatureRect.Width, creatureRect.Height))
        {
            Intelize();
        }
        //INTELIZE
        protected override void Intelize()
        {
            m_TexturePath = "Textures/Creatures/Spikes/Spike.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 0;
            m_Resistents = 0;
            m_MaxSpeed = 0;
            m_Gravity = 100;
            //attack
            m_AttackStartDuration = 0.8f;
            m_AttackDuration = 2;
            m_AttackStartTimer = new MyTimer(m_AttackStartDuration);
            m_AttackTimer = new MyTimer(m_AttackDuration);
            m_CooldownTimer = new MyTimer(3);
            //animation
            IntelizeAnimations(10, 2);
        }
        //spike macanic
        //OVERIDES
        public override bool GetAttack(Creature creature)
        {
            RectangleF creatureRect = creature.GetRect();
            RectangleF agroRect = m_ColisionRect;
            //is out of AgroRange
            if (!Colision.RectInRect(creatureRect, m_AttackRect))
            {
                return false;
            }
            //if is in range witch direction
            m_Attack = true;

            //IF IN ATACKRECT THEN TRUE CHANGE LATER
            if (m_Attack && m_AttackLethal)
            {
                if (Colision.RectInRect(creature.GetRect(), m_AttackRect))
                {
                    m_AttackHit = true;
                    return true;
                }
            }

            return false;
        }
        protected override bool HandelTargeting()
        {
            if (!m_CooldownTimer.IsRun() && m_Cooldown)
            {
                m_Cooldown = false;
                m_CooldownTimer.Reset();
            }

            return false;
        }
        //UPDATE
        public override void Update(float elapsedSec)
        {

            HandelTargeting();
            if (m_CooldownTimer.IsRun(elapsedSec)) m_Attack = false;
            if (m_Attack)
            {
                base.Attack(elapsedSec);
                if (!m_Attack)
                {
                    m_Cooldown = true;
                }
            }
            base.Update(elapsedSec);
            TextureUpdate(elapsedSec, 0.3f);
        }
       
        //IHAVEANIMATIONS
        private void IntelizeAnimations(int maxWidth, int maxHeight)
        {
            m_AnimationHandeler = new AnimationHandeler(m_TexturePath, new Point(maxWidth, maxHeight));
            //IDEL(1)
            m_AnimationHandeler.AddRow(10);
            m_AnimationHandeler.AddRow(2);
        }
        private void TextureUpdate(float elapsedSec, float animationDuration)
        {
            CurantMovment animation = GetAnimationType();
            SetAnimation(animation);
            m_AnimationHandeler.UpdateTexture(elapsedSec, animationDuration);


            m_SourceRect = m_AnimationHandeler.GetSourceRect();
            m_SourceRect.Width -= m_SourceRect.Width / 100 * 10;
            m_SourceRect.Height -= m_SourceRect.Height / 100 * 20;
            m_SourceRect.Y += m_SourceRect.Height / 100 * 20;
        }
        private void SetAnimation(CurantMovment movment)
        {
            switch (movment)
            {
                case CurantMovment.Wait:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Wait);
                    break;
                case CurantMovment.Stab:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Stab,0,5);
                    break;
                default:
                    break;
            }

        }
        private CurantMovment GetAnimationType()
        {
            if(m_CooldownTimer.IsRun()) return CurantMovment.Wait;
            if (m_Attack) return CurantMovment.Stab;
            return CurantMovment.Wait;

        }
        //SETTERS
        protected override void SetColisonRect(RectangleF drawRect)
        {
            float marginH = drawRect.Height / 100 *40;
            float width = drawRect.Width / 1f;
            float height = drawRect.Height / 1f;

            m_ColisionRect.X = drawRect.X;
            m_ColisionRect.Y = drawRect.Y + (drawRect.Height - height) - marginH;
            m_ColisionRect.Width = width;
            m_ColisionRect.Height = height;

            m_AttackRect = m_ColisionRect;
        }
    }
}
