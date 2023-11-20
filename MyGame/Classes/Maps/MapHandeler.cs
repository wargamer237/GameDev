using MyBlocks;
using MyMap;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyUtils;


namespace MyHandelers
{
    internal class MapHandeler
    {
        MapCreator m_MapCreator;

        public MapHandeler()
        {
            m_MapCreator = new MapCreator();
        }

        public void SetMap(int[][][] intedMap, Grid gridLayout)
        {
            m_MapCreator.GenrateMap(intedMap, gridLayout);
        }
        public void DrawMap()
        {
            Block[][][] blocks = m_MapCreator.GetBlocks();
            for (int chunck = 0; chunck < blocks.Length; chunck++)
            {
                for (int row = 0; row < blocks[chunck].Length; row++)
                {
                    for (int colum = 0; colum < blocks[chunck][row].Length; colum++)
                    {
                        blocks[chunck][row][colum].Draw();
                    }
                }
            }
        }
        public void Update(float elapsedSec)
        {
            List<Block> blocks = m_MapCreator.GetListBlocks();

            foreach (Block block in blocks)
            {
                if (block is IMovable movableBlock)
                {
                    movableBlock.Update(elapsedSec);
                }
            }
        }

        public List<Vertexs> CheckMapColison(List<Vertexs> vertexsObj, ref Vector2 externVelocty)
        {
            List<Block> blocks = m_MapCreator.GetListBlocks();
            Vertexs[] vertexsOb = vertexsObj.ToArray();
            for (int i = 0; i < vertexsObj.Count; i++)
            {
                vertexsOb[i].Colided = false;
                vertexsObj[i] = vertexsOb[i];
            }
            Platform p = new Platform();
            foreach (Block block in blocks)
            {
                if(block is ICollidable)
                {
                    bool colided =
                        MyColison.VertexsListCollisionWithRectangle(ref vertexsObj, block.GetRect(), block);
                    /*if (block is IMovable movableBlock)
                    {
                        if (colided)
                        {
                            externVelocty = movableBlock.GetVelocty();
                        }
                    }*/
                }
            }

            return vertexsObj;
        }

    }
}
