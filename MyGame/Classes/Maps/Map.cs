using System.Collections.Generic;
using System.Diagnostics;

namespace MyClass.MyMap
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
        TestMap = 0,
        Tutorial = 1,
        Level1 = 2
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
        public Map(MapTypes map)
        {
            PickMap(map);
        }
        private void PickMap(MapTypes map = MapTypes.TestMap)
        {
            switch (map)
            {
                case MapTypes.TestMap:
                    SetMap(5, 5, 5, 5, 100);
                    TestMap();
                    break;
                case MapTypes.Tutorial:
                    SetMap(4, 5, 5, 5, 100);
                    Tutorial();
                    break;
                case MapTypes.Level1:
                    SetMap(4, 5, 5, 5, 100);
                    Level1();
                    break;
                default:
                    break;
            }
        }
        private void SetMap(int xchunk, int ychunk, int xblock, int yblock, int blockSize)
        {
            Grid gridLayout = new Grid();
            gridLayout.XChunck = xchunk;
            gridLayout.YChunck = ychunk;
            gridLayout.XBlock = xblock;
            gridLayout.YBlock = yblock;
            gridLayout.BlockSize = blockSize;
            m_GridLayout = gridLayout;
            m_IntedMap = InitializeMap(gridLayout);

        }
        private void AddChunck(int[][] values, Grid gridLayout, int x, int y)
        {
            // Check if 'x' and 'y' are within the boundaries of the grid layout
            if (x < 0 || x > gridLayout.XChunck || y < 0 || y >= gridLayout.YChunck) return;

            int chunck = y * gridLayout.XChunck + x;
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
        private void Tutorial()
        {
            List<int[][]> mapList = new List<int[][]>();
            //chunk 1
            int[][] chunck = new int[][]{
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 0, 1, 2, 2 },
                new int[] { 1, 6, 1, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 2
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 2, 2, 0, 0, 0 },
                new int[] { 0, 0, 9, 9, 9 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);

            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 9, 9, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);

            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 2, 2, 2, 0, 0 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 0, 0, 0, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //ROW 2
            //chunk 1-2
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 7, 0 },
                new int[] { 0, 0, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 0, 0, 0 },
                new int[] { 1, 1, 0, 7, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 0, 8, 1, 1 }
            };
            mapList.Add(chunck);
            //NEW ROW
            //chunk 1-2
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 7, 0, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 0, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1},
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 }
            };
            mapList.Add(chunck);
            //NEW ROW
            //chunk 1
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 1, 1, 1 },
                new int[] { 0, 1, 1, 1, 0 },
                new int[] { 10, 1, 1, 1, 0 }
            };
          
            mapList.Add(chunck);
            //chunk 2
            chunck = new int[][]{
                new int[] { 0, 0, 0, 7, 0},
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 1, 1, 1, 0, 0 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 7, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //CREATE MAP
            AddToList(mapList);
        }

        private void Level1()
        {
            List<int[][]> mapList = new List<int[][]>();
            //chunk 1
            int[][] chunck = new int[][]{
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 9, 0 },
                new int[] { 1, 0, 1, 2, 2 },
                new int[] { 1, 6, 1, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 2
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 9, 0, 0, 0 },
                new int[] { 2, 2, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);

            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 7, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);

            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 8 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 2, 2, 2, 0, 0 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 0, 0, 0, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //ROW 2
            //chunk 1-2
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 7, 0 },
                new int[] { 0, 0, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 0, 0, 0 },
                new int[] { 1, 1, 0, 7, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 0, 8, 1, 1 }
            };
            mapList.Add(chunck);
            //NEW ROW
            //chunk 1-2
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 8, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 9, 9, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 0, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 },
                new int[] { 1, 1, 1, 0, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 1, 1, 1, 1, 1},
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 0, 0, 1, 1 },
                new int[] { 0, 1, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 }
            };
            mapList.Add(chunck);
            //NEW ROW
            //chunk 1
            chunck = new int[][]{
                new int[] {  0, 0, 0, 0, 0},
                new int[] {  0, 0, 0, 1, 1 },
                new int[] {  0, 1, 1, 1, 1 },
                new int[] {  0, 1, 1, 1, 9 },
                new int[] { 10, 1, 1, 1, 1 }
            };

            mapList.Add(chunck);
            //chunk 2
            chunck = new int[][]{
                new int[] { 0, 7, 0, 7, 0},
                new int[] { 2, 2, 2, 2, 2 },
                new int[] { 2, 2, 2, 2, 2 },
                new int[] { 9, 9, 9, 9, 9 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            mapList.Add(chunck);
            //chunk 3
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 2, 2, 2, 0, 0 },
                new int[] { 2, 2, 2, 0, 0 },
                new int[] { 9, 9, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 4
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //chunk 5
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 7, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            //CREATE MAP
            AddToList(mapList);
        }
        private void TestMap()
        {
            List<int[][]> mapList = new List<int[][]>();

            mapList.AddRange(FirstRow());
            
            int[][] chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 8 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 9, 9, 9, 9 },
                new int[] { 1, 1, 1, 1, 1 }
            };

            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 7, 0, 0 },
                new int[] { 0, 0, 2, 0, 2 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 2, 2, 2, 2, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 0, 6, 0, 0, 1 },
                new int[] { 2, 2, 2, 2, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 2, 2, 2, 0, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 1 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 1, 1, 1, 1 }
            };
            mapList.Add(chunck);


            AddToList(mapList);
        }
        private void AddToList(List<int[][]> mapList)
        {
            int y = 0;
            for (int i = 0; i < mapList.Count; i++)
            {
                for (int x = 0; x < m_GridLayout.XChunck; x++)
                {
                    if (mapList.Count == i) return;
                    AddChunck(mapList[i], m_GridLayout, x, y);
                    i++;
                }
                y++;
            }
        }
        private List<int[][]> FirstRow()
        {
            List<int[][]> mapList = new List<int[][]>();
            int[][] chunck = {
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 1, 0, 1, 0 },
                new int[] { 1, 1, 0, 1, 1 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 1, 0, 1, 1, 0 },
                new int[] { 1, 1, 1, 1, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0, 1 },
                new int[] { 1, 0, 1, 1, 1 },
                new int[] { 1, 1, 1, 1, 0 },
                new int[] { 0, 1, 0, 0, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 }
            };
            mapList.Add(chunck);
            chunck = new int[][]{
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0, 0 },
                new int[] { 1, 1, 0, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 },
                new int[] { 0, 1, 1, 0, 0 }
            };
            mapList.Add(chunck);
            return mapList;
        }
    }
}
