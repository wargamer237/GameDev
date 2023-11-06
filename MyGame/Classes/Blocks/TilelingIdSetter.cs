using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocks
{
    internal static class Tileling
    {
        public static int GetTileId(int[][] array, int row, int colum,int itemId)
        {
            return SetTileType(GetCrossBlocks(array, row, colum), GetCornersBlocks(array, row, colum), itemId);
        }
        private static int SetTileType(int[] arrayAround, int[] arrayCorners, int itemId)
        {
            for (int i = 0; i < arrayAround.Length; i++)
            {
                if (arrayAround[i] != itemId)
                {
                    arrayAround[i] = 0;
                }
            }
            for (int i = 0; i < arrayCorners.Length; i++)
            {
                if (arrayCorners[i] != itemId)
                {
                    arrayCorners[i] = 0;
                }
            }
            int id = GetHozontalVerticalTiles(arrayAround);
            //LEFT,TOP,RIGHT,BOTTOM
            switch (id)
            {
                case 1:
                    //tile type 42
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, false })) return 42;
                    //stay tile type 1
                    return 1;
                case 2:
                    //tile type 36
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, true })) return 36;
                    //tile type 33
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, false })) return 33;
                    //tile type 34
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, false, true })) return 34;
                    //stay tile type 2
                    return 2;
                case 3:
                    //tile type 43
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, true, false, true })) return 43;
                    return 3;
                case 4:
                    //tile type 35
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, true, false })) return 35;
                    //tile type 28
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, false, false })) return 28;
                    //tile type 29
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, false })) return 29;
                    //stay tile type 4
                    return 4;
                case 5:
                    //4 corners
                    //all of them
                    //tile type 39
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, true, true, true })) return 39;
                    //--! need 3 coners ELSE COUD BE VIUSAL BUG
                    //2 corners
                    //2 of eatch coners
                    //split side
                    //tile type 25
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, true, false })) return 25;
                    //tile type 26
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, false, true })) return 26;
                    //folowed up
                    //tile type 21
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, true, false, false })) return 21;
                    //tile type 22
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, true, false })) return 22;
                    //tile type 23
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, true })) return 23;
                    //tile type 24
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, true })) return 24;
                    //1corner:
                    //one of eacht coners
                    //tile type 17
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, false })) return 17;
                    //tile type 18
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, false, false })) return 18;
                    //tile type 19
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, true, false })) return 19;
                    //tile type 20
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, false, true })) return 20;

                    //stay tile type 5
                    return 5;
                case 6:
                    //tile type 37
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, true })) return 37;
                    //tile type 30
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, false, false, true })) return 30;
                    //tile type 27
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, false })) return 27;
                    //stay tile type 6
                    return 6;
                case 7:
                    //tile type 41
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, false, false })) return 41;
                    //stay tile type 7
                    return 7;
                case 8:
                    //tile type 38
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, true, false, false })) return 38;
                    //tile type 31
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, false })) return 31;
                    //tile type 32
                    if (ArrayCompareCorners(arrayCorners, new bool[] { false, true, false, false })) return 32;
                    //stay tile type 8
                    return 8;
                case 9:
                    //tile type 40
                    if (ArrayCompareCorners(arrayCorners, new bool[] { true, false, false, false })) return 40;
                    //stay tile type 9
                    return 9;
            }
            //IF DUS NOT CONTAIN A CORNER BLOCK
            return id;
        }
        private static bool ArrayCompare(int[] array, bool[] boolEmpty)
        {
            const int empty = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if ((array[i] != empty) == boolEmpty[i])
                {
                    return false;
                }
            }
            return true;
        }
        private static bool ArrayCompareCorners(int[] array, bool[] boolEmpty)
        {
            const int empty = 0;
            bool isOk = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (boolEmpty[i])
                {
                    if (array[i] == empty) isOk = true;
                    else return false;
                }
            }
            return isOk;
        }
        private static int GetHozontalVerticalTiles(int[] crossBlocks)
        {
            //LEFT,TOP,RIGHT,BOTTOM
            //tile type 1
            if (ArrayCompare(crossBlocks, new bool[] { true, true, false, false })) return 1;
            //tile type 2
            if (ArrayCompare(crossBlocks, new bool[] { false, true, false, false })) return 2;
            //tile type 3
            if (ArrayCompare(crossBlocks, new bool[] { false, true, true, false })) return 3;
            //tile type 4
            if (ArrayCompare(crossBlocks, new bool[] { true, false, false, false })) return 4;
            //tile type 5
            if (ArrayCompare(crossBlocks, new bool[] { false, false, false, false })) return 5;
            //tile type 6
            if (ArrayCompare(crossBlocks, new bool[] { false, false, true, false })) return 6;
            //tile type 7
            if (ArrayCompare(crossBlocks, new bool[] { true, false, false, true })) return 7;
            //tile type 8
            if (ArrayCompare(crossBlocks, new bool[] { false, false, false, true })) return 8;
            //tile type 9
            if (ArrayCompare(crossBlocks, new bool[] { false, false, true, true })) return 9;
            //tile type 10
            if (ArrayCompare(crossBlocks, new bool[] { true, true, true, true })) return 10;
            //tile type 11
            if (ArrayCompare(crossBlocks, new bool[] { true, true, false, true })) return 11;
            //tile type 12
            if (ArrayCompare(crossBlocks, new bool[] { false, true, false, true })) return 12;
            //tile type 13
            if (ArrayCompare(crossBlocks, new bool[] { false, true, true, true })) return 13;
            //tile type 14
            if (ArrayCompare(crossBlocks, new bool[] { true, true, true, false })) return 14;
            //tile type 15
            if (ArrayCompare(crossBlocks, new bool[] { true, false, true, false })) return 15;
            //tile type 16
            if (ArrayCompare(crossBlocks, new bool[] { true, false, true, true })) return 16;
            return -1;
        }
        private static int[] GetCornersBlocks(int[][] array, int row, int colum)
        {
            bool LEFT = colum - 1 >= 0;
            bool TOP = row - 1 >= 0;
            bool RIGHT = colum + 1 < array.Length;
            bool BOTTOM = row + 1 < array.Length;

            int[] blockCorners = new int[] { 0, 0, 0, 0 };//{ 0,1,2,3 }

            ////LEFT & TOP
            if (LEFT && TOP)
                blockCorners[0] = array[row - 1][colum - 1]; //LEFT & TOP
            //TOP & RIGHT
            if (TOP && RIGHT)
                blockCorners[1] = array[row - 1][colum + 1]; //TOP & RIGHTT
            ////RIGHT & BOTTTOM
            if (RIGHT && BOTTOM)
                blockCorners[2] = array[row + 1][colum + 1]; //RIGHT & BOTTOM
            ////BOTTOM & LEFT
            if (BOTTOM && LEFT)
                blockCorners[3] = array[row + 1][colum - 1]; //BOTTOM & LEFT

            return blockCorners;
        }
        private static int[] GetCrossBlocks(int[][] array, int row, int colum)
        {
            //LEFT,LEFT TOP, TOP, TOP RIGHT, RIGHT, RIGHT BOTTOM, BOTTOM, BOTTOM LEFT
            int[] blocksAround = new int[] { 0, 0, 0, 0 };//{ 0,1,2,3 }


            bool LEFT = colum - 1 >= 0;
            bool TOP = row - 1 >= 0;
            bool RIGHT = colum + 1 < array.Length;
            bool BOTTOM = row + 1 < array.Length;

            ////LEFT
            if (LEFT)
                blocksAround[0] = array[row][colum - 1]; //LEFT
            ////TOP
            if (TOP)
                blocksAround[1] = array[row - 1][colum]; //TOP
            ////RIGHT
            if (RIGHT)
                blocksAround[2] = array[row][colum + 1]; //RIGHT
            ////BOTTTOM
            if (BOTTOM)
                blocksAround[3] = array[row + 1][colum]; //BOTTOM

            return blocksAround;
        }
    }
}
