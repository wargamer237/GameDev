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
            m_Player = new Player(new RectangleF(200, -200, 100, 100));
            m_FocuedTarget = m_Player;
            m_Creatures = new List<Creature>{ m_Player };
            m_Creatures.Add(new Robot(new RectangleF(170, -200, 150, 100)));
            m_Creatures.Add(new Player  (new RectangleF(190, -200, 100, 100)));
        }
        public void Draw()
        {
            m_Player.Draw();
            for (int i = m_Creatures.Count -1; i >= 0; i--)
            {
                if(m_Creatures[i] is Robot r)
                {
                    r.DebugDraw();
                }
                m_Creatures[i].Draw();
            }
        }
        public void Update(float elapsedSec)
        {
            PlayerUpdate(ref m_Player, elapsedSec);

            for (int i = 0; i < m_Creatures.Count; i++)
            {
                Creature creature = m_Creatures[i];
                
                if(creature != m_Player)
                {
                    creature.Update(elapsedSec);
                }
                if (creature is Robot r)
                {
                    r.SettTarget(m_Player);
                    m_FocuedTarget = r;
                }
                creature = BlocksIntreactions(creature);

                m_Creatures[i] = creature;
            }
        }
       
        public void PlayerUpdate(ref Player player, float elapsedSec)
        {
            m_PlayerManger.Update(ref player, elapsedSec);
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
