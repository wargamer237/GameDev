using Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MyUtils
{
    public struct Vertexs
    {
        public PointF First;
        public PointF Second;
        public bool Colided;
        public float Depth;
        public Vertexs()
        {
            First = new PointF();
            Second = new PointF();

            Depth = 0;
            Colided = false;
        }
        public Vertexs(PointF a, PointF b)
        {
            First = a;
            Second = b;

            Depth = 0;
            Colided = false;
        }
        public Vector2 GetVectorDirection(PointF a , PointF b)
        {
            return new Vector2(b.X - a.X, b.Y - a.Y);
        }
    }

    internal class MyColison
    {
        public static Vertexs VertexWithInRectangle(ref Vertexs vertex, RectangleF rectangle)
        {
            // Check if First or Second points of Vertexs are inside RectangleF.
            //--! MABY SWAP FALSE WITH TRUE for shorter code. ANSWERE: nhe
            if (UtilsStatic.Contains(rectangle, vertex.Second))//check if point in the block
            {
                vertex.Colided = true;
                vertex.Depth = GetVertexDepth(vertex.First,vertex.Second,rectangle);
                return vertex; // Collision detected.
            }
            // No collision detected.
            vertex.Colided = false;
            vertex.Depth = 0;
            return vertex;
        }
        public static float GetVertexDepth(PointF first, PointF second, RectangleF rect)
        {
            PointF center = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            float depth = 0;
            if (first.Y == second.Y)// is it horzontal
            {
                depth = VerticDepthCaluclate(first.X, second.X, rect.Width, center.X);
            }
            else if (first.X == second.X)// is it vertical
            {
               depth = VerticDepthCaluclate(first.Y, second.Y, rect.Height, center.Y);
            }            

            return depth;
        }
        private static float VerticDepthCaluclate(float firstV, float secondV, float rectV, float centerV)
        {
            float verticLength = MathF.Abs(firstV - secondV);
            float disctenceFromCenter = MathF.Abs(secondV - centerV);
            return verticLength - (rectV / 2 - disctenceFromCenter);
        }
        // Function to check collisions between a list of Vertexs and a list of RectangleF blocks.
        public static List<Vertexs> VertexsListCollisionWithRectangle(ref List<Vertexs> vertexsList, List<RectangleF> blockList)
        {
            // Loop through each Vertexs using its index as the identifier.
            for (int i = 0; i < vertexsList.Count; i++)
            {
                var vertexs = vertexsList[i];

                // Check for collisions with each RectangleF block.
                foreach (var block in blockList)
                {
                    VertexWithInRectangle(ref vertexs, block);
                    if (vertexs.Colided)
                    {
                        break;
                    }
                }
                //update vertex even if not hit. (depth and colsion rest
                vertexsList[i] = vertexs;
            }

            return vertexsList;
        }
        public static List<Vertexs> VertexsListCollisionWithRectangle(ref List<Vertexs> vertexsList, RectangleF block)
        {
            // Loop through each Vertexs using its index as the identifier.
            for (int i = 0; i < vertexsList.Count; i++)
            {
                Vertexs vertex = vertexsList[i];
                if (vertex.Colided == true) continue;
                // Check for collisions with each RectangleF block. 
                VertexWithInRectangle(ref vertex, block);

                //update vertex even if not hit. (depth and colsion rest
                vertexsList[i] = vertex;
            }

            return vertexsList;
        }
    }
}
