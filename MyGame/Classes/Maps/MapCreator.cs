using MyUtils;
using MyBlocks;
using System.Collections.Generic;

namespace MyMap
{
    public struct Grid
    {
        public int XChunck;
        public int YChunck;
        public int XBlock;
        public int YBlock;

        public int BlockSize;
    }
    internal class MapCreator
    {
        //LISTS
        Block[][][] m_MapBlocks;
        public MapCreator(int[][][] intedMap, Grid gridLayout)
        {
            GenrateMap(intedMap, gridLayout);
        }
        public MapCreator()
        {
            
        }
        public Block[][][] GetBlocks()
        {
            return m_MapBlocks;
        }
        //CONVERT THE 3D ARRAY TO 1D list
        public List<Block> GetListBlocks()
        {
            List<Block> listBlocks = new List<Block>();
            for (int chunck = 0; chunck < m_MapBlocks.Length; chunck++)
            {
                for (int row = 0; row < m_MapBlocks[chunck].Length; row++)
                {
                    for (int colum = 0; colum < m_MapBlocks[chunck][row].Length; colum++)
                    {
                        listBlocks.Add(m_MapBlocks[chunck][row][colum]);
                    }
                }
            }
            return listBlocks;
        }
        //START
        public void GenrateMap(int[][][] intedMap, Grid gridLayout)
        {
            //CREATE A ARRAY THAT CAN FIT THE MAP even if there are les chunks it makes a rectangle
            m_MapBlocks = CreatGridInArray(gridLayout);
            //array true blocks
            int chunckX = 0;
            int chunckY = 0;
            for (int chunck = 0; chunck < intedMap.Length; chunck++)
            {
                //CREATE THE MAP AND THEN ADD IT TO THIS CHUNCK
                m_MapBlocks[chunck] = AddChunck(m_MapBlocks[chunck], intedMap[chunck], gridLayout, chunckX, chunckY);
                //say with chunck its is dependen on GridLayout
                chunckX++;
                if(chunckX > gridLayout.XChunck)
                {
                    chunckX = 0;
                    chunckY++;
                }
            }  
        }
        private Block[][] AddChunck(Block[][] fillBlocks, int[][] intedBlocks, Grid gridLayout, int chunckX, int chunckY)
        {
            // Calculate the index of the chunk within the map CEEP IT
            /*int chunck = chunckY * gridLayout.XChunck + chunckX;*/
            // Iterate through the 2D array of tile types
            for (int row = 0; row < intedBlocks.Length; row++)
            {
                for (int colum = 0; colum < intedBlocks[row].Length; colum++)
                {
                    //see with kinde of block it woud be: enum BlockType
                    int tileType = Tileling.GetTileId(intedBlocks, row, colum, intedBlocks[row][colum]);
                    //create a rectangle for the block so it woud be right size if in CHUNCK
                    //x pos, y pos
                    //width and height.
                    RectangleF rect = CreatBlockRectangleF(gridLayout, colum, row, chunckX, chunckY);
                    //create the block. If cant create null
                    Block newBlock = AddBlock(rect, intedBlocks[row][colum], tileType);
                    //if exist add block to the map
                    if (newBlock != null)
                    {
                        fillBlocks[row][colum] = newBlock;
                    }
                }
            }
            return fillBlocks;
        }
        private Block AddBlock(RectangleF rect, int type, int tileType)
        {
            if (rect.Width == 0 || rect.Height == 0) return null;

            Block block = CreatBlock(rect, type, tileType);

            if (block == new Block()) return null;

             return block;
        }
        //CREAT BLOCK RECT DEPENDEN ON THE GRID. if its out off it then it wont be added
        private RectangleF CreatBlockRectangleF(Grid gridLayout, int colum, int row, int chunckX, int chunckY)
        {
            RectangleF rect = new RectangleF(0, 0, 0, 0);
            //IF TRY TO CREAT OUT OF RANGE OF THE CHUNCK THEN SEND A EMPTY BLOCK
            if (row > gridLayout.YBlock
            || row < 0
            || colum > gridLayout.XBlock
            || colum < 0)
                return rect;

            rect.X = colum * gridLayout.BlockSize + (chunckX * gridLayout.BlockSize * gridLayout.XBlock);
            rect.Y = row * gridLayout.BlockSize + (chunckY * gridLayout.BlockSize * gridLayout.YBlock);
            rect.Width = gridLayout.BlockSize;
            rect.Height = gridLayout.BlockSize;

            return rect;
        }
        private Block CreatBlock(RectangleF rect, int type, int tileType)
        {
            Block block = new Block(rect);
            switch (type)
            {
                case (int)BlockType.BackGround:
                    block = new BackGround(rect, tileType);
                    break;
                case (int)BlockType.Ground:
                    block = new Ground(rect, tileType);
                    break;
                case (int)BlockType.Platform:
                    Platform p = new Platform(rect, tileType);
                    p.SetPath(new PointF(0, 0), new PointF(3, 0), 2);
                    block = p;
                    break;
            }
            return block;
        }
        private Block[][][] CreatGridInArray(Grid gridLayout)
        {
            //NEW ARRAY
            Block[][][] newBlocks = new Block[gridLayout.YChunck * gridLayout.XChunck][][];
            //CREATE ARRAY
            for (int chunck = 0; chunck < newBlocks.Length; chunck++)
            {
                //ADD BlockY amount
                newBlocks[chunck] = new Block[gridLayout.YBlock][];
                for (int yBlock = 0; yBlock < newBlocks[chunck].Length; yBlock++)
                {
                    newBlocks[chunck][yBlock] = new Block[gridLayout.XBlock];
                }
            }
            //FILL ARRAY EMPTY BLOCKS 
            //--!MABY Change to background blocks later 
            //ADD EMPTY BLOCKS IN THE LIST
            int size = 200;
            for (int chunck = 0; chunck < newBlocks.Length; chunck++)
            {
                for (int row = 0; row < newBlocks[chunck].Length; row++)
                {
                    for (int colums = 0; colums < newBlocks[chunck][row].Length; colums++)
                    {
                        newBlocks[chunck][row][colums] = new Block(new RectangleF(0,0, size, size));
                    }
                }
            }

            return newBlocks;
        }
    }
}
