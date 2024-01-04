using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyClass.MyUtils;
using MyClass.MyBlocks;
using MyClass.MyBlocks.Blocks;
using MyClass.MyBlocks.BlocksInterfaces;
using MyCreature;
namespace MyClass.MyMap
{
    internal class MapHandeler
    {
        MapCreator m_MapCreator;
        BackGround m_Backgroun;
        List<Ground> m_Border;

        public MapHandeler()
        {
            m_MapCreator = new MapCreator();  
        }

        public void SetMap(int[][][] intedMap, Grid gridLayout)
        {
            float w = gridLayout.BlockSize * gridLayout.XBlock * (gridLayout.XChunck +1) - gridLayout.BlockSize;
            float h = gridLayout.BlockSize * gridLayout.YBlock * gridLayout.YChunck;
            m_Backgroun = new BackGround(new RectangleF(0, 0, gridLayout.BlockSize, gridLayout.BlockSize),new RectangleF(0,0,w,h));
            BuildBorder(new RectangleF(0, 0, w, h), gridLayout.BlockSize);
            m_MapCreator.GenrateMap(intedMap, gridLayout);
        }
        public List<Creature> GetCreatures()
        {
            return m_MapCreator.GetListCreatures();
        }
        private void BuildBorder(RectangleF mapSize, float blockSize)
        {
            float borderSize = 10;
            float maxWidth = (mapSize.Width + borderSize * blockSize)/2 -blockSize;  // Set initial maxWidth based on map size
            float maxHeight = mapSize.Height + borderSize * blockSize;

            float x = -blockSize * borderSize, y = -blockSize * borderSize, w = blockSize, h = blockSize;
            m_Border = new List<Ground>();

            while (true)
            {
                m_Border.Add(new Ground(new RectangleF(x, y, w, h), 10));
                m_Border.Add(new Ground(new RectangleF( mapSize.Width - (x + blockSize) , y, w, h), 10));
                x += blockSize;

                if (x >= maxWidth)
                {
                    if (y >= maxHeight) return;
                    x = -borderSize * blockSize;
                    y += blockSize;

                    // Update maxWidth based on the current row (top or bottom)
                    if (y >= mapSize.Y && y <= mapSize.Height)
                    {
                        maxWidth = 0;
                    }
                    else
                    {
                        maxWidth = (mapSize.Width + borderSize * blockSize)/2 - blockSize*2 -blockSize;
                    }
                }
            }
        }


        public void DrawMap()
        {
            foreach (Ground item in m_Border)
            {
                item.Draw();
            }
            m_Backgroun.DrawBackground();
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
            blocks.AddRange(m_Border);
            Vertexs[] vertexsOb = vertexsObj.ToArray();
            for (int i = 0; i < vertexsObj.Count; i++)
            {
                vertexsOb[i].Colided = false;
                vertexsObj[i] = vertexsOb[i];
            }

            foreach (Block block in blocks)
            {
                if(block is ICollidable)
                {
                    bool colided =
                        MyColison.VertexsListCollisionWithRectangle(ref vertexsObj, block.GetRect(), block);
                }
            }

            return vertexsObj;
        }

    }
}
