using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyUtils;
using MyBlocks;
using MyPlayer;
using System.Numerics;
namespace MyCreature
{
    internal class CreatureManger
    {
        PlayerManger m_PlayerManger = new PlayerManger();
        Player m_Player;
        Creature m_FocuedTarget;
        List<Creature> m_Creatures;
        public CreatureManger()
        {
            m_Player = new Player(new RectangleF(100, 200, 100, 100));
            m_FocuedTarget = m_Player;
            m_Creatures = new List<Creature>{ m_Player };
            m_Creatures.Add(new Robot(new RectangleF(170, -200, 100, 100)));
            m_Creatures.Add(new Player  (new RectangleF(190, -200, 100, 100)));
            m_Creatures.Add(new Spike(new RectangleF(100,200,100,100)));
        }
        public void Draw()
        {
            m_Player.Draw();
            for (int i = m_Creatures.Count -1; i >= 0; i--)
            {
                if(m_Creatures[i] is AttackCreature r)
                {
                    r.DebugDraw();
                }
                if(m_Creatures[i] is Spike s)
                {
                    s.Draw();
                }
                m_Creatures[i].Draw();
            }
        }
        public void Update(float elapsedSec)
        {
            PlayerUpdate(ref m_Player, elapsedSec);
            bool playerAttacking = m_Player.IsLeathalAttack();
            for (int i = 0; i < m_Creatures.Count; i++)
            {
                Creature creature = m_Creatures[i];
                creature = BlocksIntreactions(creature);

                if (creature == m_Player) continue;
                creature.Update(elapsedSec);
                if (creature is AttackCreature imAttacker)
                {
                    m_Player.SetHit(imAttacker.GetAttack(m_Player));

                }
                PlayerAttacks(ref creature);
                m_Creatures[i] = creature;
                CreatureDeaths(creature);
            }
        }
        public void PlayerAttacks(ref Creature creature)
        {
            if (!m_Player.IsLeathalAttack()) return;
            creature.SetHit(m_Player.GetAttack(creature));
        }
        public void CreatureDeaths(Creature creature)
        {
            bool Dead = creature.GetDeathState();
            if (!Dead) return;
            m_Creatures.Remove(creature);
            creature = new Robot(new RectangleF(190, 0, 100, 100));
            m_Creatures.Add(creature);

        }
        public void PlayerUpdate(ref Player player, float elapsedSec)
        {
            m_PlayerManger.Update(ref player, elapsedSec);
            bool Dead = player.GetDeathState();
            if (Dead)
            {
                m_Creatures.Remove(player);
                player = new Player(new RectangleF(0, 0, 100, 100));
                m_Creatures.Add(player);
                m_FocuedTarget = player;
            }
        }
        public Creature BlocksIntreactions(Creature creature)
        {
            List<Vertexs> vertexs = creature.GetVertexs();

            foreach (Vertexs vertic in vertexs)
            {
                if (!vertic.Colided) continue;
                if (vertic.Block is IMovable moveBlock)
                {
                    creature.SetExternVelocty(moveBlock.GetVelocty());
                }
            }
            return creature;
        }
        public PointF GetCameraTranslation()
        {
            RectangleF r = m_FocuedTarget.GetRect();
            return new PointF(r.X + r.Width/2, r.Y + r.Height / 2);
        }
        public List<Creature> GetCreatures()
        {
            return m_Creatures;
        }
        public void SetCreatures(List<Creature> c)
        {
            m_Creatures = c;
        }
    }
}
