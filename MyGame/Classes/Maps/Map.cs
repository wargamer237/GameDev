
using Blocks;

namespace MyMap
{
    /*  
     *  public enum BlockType
     *   {
     *     Nothing = 0,
     *     Ground = 1,
     *     Platform = 2,//moving
     *     BackGround = 3,
     *   }
     */
    public enum MapTypes
    {
        TestMap = -1,
        Tutorial = 0
    }
    internal class Map
    {
        protected string m_Name;
        protected int[][][] m_IntedMap;
        Grid m_GridLayout;
        public string Name
        {
            get { return m_Name; }
        }
        public int[][][] IntedMap
        {
            get { return m_IntedMap; }
        }
        public Grid GridLayout
        {
            get { return m_GridLayout; }
        }
        public Map()
        {
            SetMap();
        }
        private void SetMap()
        {
            Grid gridLayout = new Grid();
            gridLayout.XChunck = 5;
            gridLayout.YChunck = 5;
            gridLayout.XBlock = 5;
            gridLayout.YBlock = 5;
            gridLayout.BlockSize = 200;
            m_GridLayout = gridLayout;
            m_IntedMap = InitializeMap(gridLayout);
            int[][] chunck1 = {
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 0, 0, 1, 1 },
                new int[] { 1, 0, 3, 3, 1 },
                new int[] { 1, 0, 1, 1, 1 },
                new int[] { 1, 1, 1, 0, 0 }
            };
            int[][] chunck2 = {
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 0, 3, 3, 1 },
                new int[] { 0, 1, 3, 3, 1 },
                new int[] { 1, 1, 3, 3, 1 },
                new int[] { 0, 1, 0, 3, 1 }
            };
          /* int[][] chunck2 = {
                new int[] { 3, 3, 3, 3, 3 },
                new int[] { 3, 0, 0, 0, 3 },
                new int[] { 3, 0, 0, 0, 3 },
                new int[] { 3, 0, 0, 0, 3 },
                new int[] { 3, 3, 3, 3, 3 }
            };*/

            AddChunck(chunck1, gridLayout, 0, 0);
           AddChunck(chunck2, gridLayout, 1, 1);
        }
        private void AddChunck(int[][] values, Grid gridLayout, int x, int y)
        {
            // Check if 'x' and 'y' are within the boundaries of the grid layout
            if (x > 0 && x < gridLayout.XChunck && y < 0 && y < gridLayout.YChunck) return;

            int chunck = y * gridLayout.XChunck + x + y; //dont know why but i have to add y beacuse there is exponential shit to the left some how
            m_IntedMap[chunck] = values;
        }

        private int[][][] InitializeMap(Grid gridLayout)
        {
            int[][][] newIntArray = new int[gridLayout.YChunck * gridLayout.XChunck][][];

            // Create the array
            for (int chunck = 0; chunck < newIntArray.Length; chunck++)
            {
                newIntArray[chunck] = new int[gridLayout.YBlock][];

                for (int yBlock = 0; yBlock < newIntArray[chunck].Length; yBlock++)
                {
                    newIntArray[chunck][yBlock] = new int[gridLayout.XBlock];
                }
            }

            return newIntArray;
        }
    }
}
