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
            m_Creatures.Add(new Robot(new RectangleF(170, 400, 250, 250)));
            m_Creatures.Add(new Player(new RectangleF(190, -200, 100, 100)));
            m_Creatures.Add(new Spike(new RectangleF(400,800,100,100)));
            m_Creatures.Add(new Spike(new RectangleF(500,800,100,100)));
            m_Creatures.Add(new Spike(new RectangleF(600,800,100,100)));
            m_Creatures.Add(new Spike(new RectangleF(700,800,100,100)));
            m_Creatures.Add(new Spike(new RectangleF(800,800,100,100)));
            m_Creatures.Add(new Spike(new RectangleF(900,800,100,100)));
            m_Creatures.Add(new Bird(new RectangleF(200,200,100,100)));
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
                    if (tCreature is AttackCreature attack)
                    {
                        attack.DebugDraw();
                    }
                    tCreature.Draw();

                }
            }
        }
        public void Update(float elapsedSec)
        {
            PlayerUpdate(ref m_Player, elapsedSec);

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
