using Blocks;
using MyMap;
using System.Collections.Generic;
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
            return;
            foreach (IMovable block in blocks)
            {
                block.Update(elapsedSec);
            }
            foreach (IInteractable block in blocks)
            {
                //DO SOMTHINGVertexsListCollisionWithRectangle
            }
        }

        public List<Vertexs> CheckMapColison(List<Vertexs> vertexsObj)
        {
            List<Block> blocks = m_MapCreator.GetListBlocks();
            Vertexs[] vertexsOb = vertexsObj.ToArray();
            for (int i = 0; i < vertexsObj.Count; i++)
            {
                vertexsOb[i].Colided = false;
                vertexsObj[i] = vertexsOb[i];
            }

            foreach (Block block in blocks)
            {
                if(block is Ground)
                {
                    MyColison.VertexsListCollisionWithRectangle(ref vertexsObj, block.GetRect());
                }              
            }

            return vertexsObj;
        }

    }
}
