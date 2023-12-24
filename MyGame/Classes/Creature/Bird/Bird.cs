using Microsoft.Xna.Framework;
using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCreature
{
    internal class Bird: AttackCreature, HasAnimations<Bird.CurantMovment>
    {
        enum CurantMovment
        {
            Fly,
            Attack,
            Death
        }
        AnimationHandeler m_AnimationHandeler;
        float m_VerticalMax;

        MyTimer m_AttackCd;
        //INTELIZE
        protected override void Intelize()
        {
            m_TexturePath = "Textures/Creatures/Bird/Bird.png";
            m_VerticxDebugRects = new List<RectangleF>();
            m_Speed = 400;
            m_Resistents = m_Speed / 100 * 50;
            m_MaxSpeed = m_Speed * 0.6f;
            m_Gravity = 0;
            m_Health = 1;
            //attack
            m_AttackStartDuration = 0.8f;
            m_AttackDuration = 2;
            m_AttackStartTimer = new MyTimer(m_AttackStartDuration);
            m_AttackTimer = new MyTimer(m_AttackDuration);
            m_AttackCd = new MyTimer(3);
            //agro 
            m_AgroRange = 10000;
            m_TargetingRange = 1000;
            m_AttackRange = 0;
            //m_CooldownTimer = new MyTimer(3);
            //animation
            IntelizeAnimations(8, 4);
        }
        //CONSTRUCTOR
        public Bird()
        {

        }
        public Bird(RectangleF creatureRect): base(creatureRect)
        {
            Intelize();
        }
        private RectangleF GetAttackRect()
        {
            return m_ColisionRect;
        }

        //overide
        public override bool GetAttack(Creature creature)
        {
            if (m_AttackCd.IsRun()) return false;
            base.GetAttack(creature);
            m_AttackRect = GetAttackRect();
            m_AttackLethal = true;
            if (Colision.RectInRect(m_AttackRect, creature.GetRect()))
            {
                //if hit set cd
                m_AttackCd.Reset();
                m_AttackLethal = false;
                m_AttackHit = true;
                m_Velocity.X = m_Velocity.X/ 4;
                m_Velocity.Y = m_Velocity.Y / 4;
                return true;
            }
            return false;
        }
        protected override bool HandelTargeting()
        {
            if (!m_Targeting) return m_Targeting;
            base.HandelTargeting();
            //if target right
            if (m_TargetDirection.Y > 0)
            {
                if (m_Speed < 0) m_Gravity = -m_Speed;
                else m_Gravity = m_Speed;
            }
            //if target left
            else if (m_TargetDirection.Y < 0)
            {
                if (m_Speed > 0) m_Gravity = -m_Speed;
                else m_Gravity = m_Speed;
            }
            return m_Targeting;
        }
        //UPDATE
        public override void Update(float elapsedSec)
        {
            float agroBoost = 1;
            if (HandelTargeting())
            {
                agroBoost = 2;
                if (base.HandelAttack(GetAttackRect(), m_MyCenter))
                {
                    agroBoost = 0;
                    base.Attack(elapsedSec);
                } 
            }
            m_AttackCd.IsRun(elapsedSec);
            m_Velocity.X += m_Speed * elapsedSec;
            m_Velocity.X = base.SpeedLimit(m_Velocity.X, m_MaxSpeed * agroBoost);
            //m_Velocity.Y = base.SpeedLimit(m_Velocity.Y, m_MaxSpeed * agroBoost);
            if (m_Dead) return;
            //Gravity standard
            m_Velocity.Y += m_Gravity * elapsedSec;
            m_Velocity.Y = SpeedLimit(m_Velocity.Y, m_MaxSpeed);

            //Check colisions
           // UpdateColision(ref m_DrawRect);
            UpdateRects(elapsedSec);
            TextureUpdate(elapsedSec, 2 * elapsedSec * 5);
            m_Gravity = 0;
        }
        //IHAVEANIMATIONS
        private void IntelizeAnimations(int maxWidth, int maxHeight)
        {
            m_AnimationHandeler = new AnimationHandeler(m_TexturePath, new Point(maxWidth, maxHeight));
            //FLAY LIKE A EAGLE
            m_AnimationHandeler.AddRow(8);
            //get hit (not using)
            m_AnimationHandeler.AddRow(-1);
            //Attack
            m_AnimationHandeler.AddRow(8);
            //Death
            m_AnimationHandeler.AddRow(4);
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
                case CurantMovment.Fly:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Fly);
                    break;
                case CurantMovment.Attack:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Attack);
                    break;
                case CurantMovment.Death:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Death);
                    break;
                default:
                    break;
            }

        }
        private CurantMovment GetAnimationType()
        {
            /*if (m_CooldownTimer.IsRun()) return CurantMovment.Wait;
            if (m_Attack) return CurantMovment.Stab;
            return CurantMovment.Wait;*/
            return CurantMovment.Fly;
        }
    }
}
