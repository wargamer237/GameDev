using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyUtils;
using MyPlayer;
using MyBlocks;
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
            m_Creatures = new List<Creature>{ new Player(new RectangleF(200,-200,100,100)) };
            m_Creatures.Add(new Player(new RectangleF(190, -200, 100, 100)));

        }
        public void Draw()
        {
            for (int i = m_Creatures.Count -1; i >= 0; i--)
            {
                m_Creatures[i].Draw();
            }

        }
        public void Update(float elapsedSec)
        {
            bool realPlayer = true;
            for (int i = 0; i < m_Creatures.Count; i++)
            {
                Creature creature = m_Creatures[i];
                if(creature is Player player)
                {
                    if(realPlayer)
                    {
                        realPlayer = false;
                        m_Player = player;
                        m_FocuedTarget = player;
                        PlayerUpdate(ref m_Player, elapsedSec);
                    }
                    else
                    {
                        player.Update(elapsedSec);
                    }
                    creature = BlocksIntreactions(creature);
                }

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
