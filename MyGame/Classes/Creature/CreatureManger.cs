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
        bool m_PlayerDied;
        bool m_PlayerWin;
        public CreatureManger()
        {
            m_Player = new Player(new RectangleF(1100, 200, 100, 100));
            m_FocuedTarget = m_Player;
            m_Creatures = new List<Creature>{ m_Player };
        }

        public void Draw()
        {
            DrawCreatures<Spike>();
            DrawCreatures<Bird>();
            DrawCreatures<Robot>();
            DrawCreatures<Player>();
        }
        void DrawCreatures<T>() where T : Creature
        {
            foreach (Creature creature in m_Creatures)
            {
                if (creature is T tCreature)
                {
                    if (creature is Player) continue;
                    if (tCreature is AttackCreature attack)
                    {
                       // attack.DebugDraw();
                    }
                    tCreature.Draw();
                }
            }
            m_Player.Draw();
        }
        public void Update(float elapsedSec)
        {
            PlayerUpdate(ref m_Player, elapsedSec);

            for (int i = 0; i < m_Creatures.Count; i++)
            {
                Creature creature = m_Creatures[i];
                creature = BlocksIntreactions(creature);

                if (creature is Player) continue;
                creature.Update(elapsedSec);
                if (creature is AttackCreature imAttacker)
                {
                    m_Player.SetHit(imAttacker.GetAttack(m_Player));
                }

                if(creature is Goal goal)
                {
                    if (Colision.RectInRect(goal.GetRect(), m_Player.GetRect()))
                    {
                        m_PlayerWin = true;
                        m_Player = new Player();
                    }
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
        }
        public void PlayerUpdate(ref Player player, float elapsedSec)
        {
            m_PlayerManger.Update(ref player, elapsedSec);
            bool Dead = player.GetDeathState();
            if (Dead)
            {
                m_Creatures.Remove(player);
                m_PlayerDied = true;
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
        public bool WonGame()
        {
            return m_PlayerWin;
        }
        public bool LostGame()
        {
            return m_PlayerDied;
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
        public void IntelizeCreatures(List<Creature> c)
        {
            m_Creatures = c;
            foreach (Creature creature in m_Creatures)
            {
                if (creature is Player p)
                {
                    m_Player = p;
                    m_FocuedTarget = m_Player;
                    return;
                }
            }
        }
    }
}
