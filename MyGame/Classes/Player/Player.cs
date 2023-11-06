using Blocks;
using Microsoft.Xna.Framework;
using MyUtils;
using System.Collections.Generic;

namespace MyPlayer
{
    internal class Player
    {
        PointF m_StartPosition;
        Vector2 m_Velocity;
        Vector2 m_Direction;

        float m_Speed;
        float m_Gravity;

        RectangleF m_Colision;
        RectangleF m_SourceRect;
        List<Vertexs> m_Vertexs;
        List<RectangleF> VerticxDebugRects;
        public Player()
        {
            VerticxDebugRects = new List<RectangleF>();
            m_Speed = 600;
            m_Gravity = 600;
            // down right test
            m_StartPosition = new PointF(200, -200);
            m_Colision.X = m_StartPosition.X;
            m_Colision.Y = m_StartPosition.Y;
            m_Colision.Width = 50;
            m_Colision.Height = 50;

            SetVertexs(IntelizeVertexs());
        }

        public void SetVelocty(Vector2 velocty)
        {
            m_Direction = velocty;
        }
        public void Draw()
        {
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(m_Colision.X, m_Colision.Y);

            UtilsStatic.DrawRect(new RectangleF(0,0, m_Colision.Width, m_Colision.Height));          
            UtilsStatic.PopMatrix();
            foreach (RectangleF item in VerticxDebugRects)
            {
                UtilsStatic.SetColor(Color.Yellow);
                UtilsStatic.DrawRect(item);
            }
        }
        public void Update(float elapsedSec)
        {
            //if up timer jump for 2s or so do no you see
            m_Velocity.Y += m_Gravity * elapsedSec;
            m_Velocity.X += m_Direction.X * m_Speed * elapsedSec;
            m_Velocity.Y += m_Direction.Y * (m_Speed + m_Gravity) * elapsedSec;
            UpdateColision();
            m_Colision.X += m_Velocity.X * elapsedSec;
            m_Colision.Y += m_Velocity.Y * elapsedSec;
            m_SourceRect = m_Colision;
        }

        //jump, sprint, esc
        private void UpdateMovment()
        {

        }

        /// <summary>
        /// Its working!!. it check colison of the vertics and 
        /// if it hit somthing and the directions of the velocty is the same 
        /// then it applys the efect of hiting a wal (snap the wall in 2 steps)
        /// if you change direction it will check that sinde IF THERE IS COLISION
        /// (ps: all vertics are always checking for colison: see maphandeler)
        /// </summary>
        private void UpdateColision() 
        {
            float horizontalDepth = 0;
            float verticalDepth = 0;
            foreach (Vertexs vertex in m_Vertexs)
            {
                if (!vertex.Colided) continue; // Skip non-colliding vertexes

                PointF First = vertex.First;
                PointF Second = vertex.Second;

                if (First.Y == Second.Y) // Horizontal Vertex
                {
                    if (horizontalDepth < vertex.Depth) horizontalDepth = vertex.Depth;

                    if (First.X < Second.X)
                    {
                        // right side: so move last bit to right
                        if (m_Velocity.X > 0)
                        {
                            m_Velocity.X = horizontalDepth;
                        }
                        else
                        {
                            horizontalDepth = 0;
                        }

                    }
                    else // left side: so move last bit to left
                    {
                        if (m_Velocity.X < 0)
                        {
                            horizontalDepth = -horizontalDepth;
                            m_Velocity.X = horizontalDepth;
                        }
                        else
                        {
                            horizontalDepth = 0;
                        }
                    }
                }
                else // Vertexal Vertex
                {
                    if (verticalDepth < vertex.Depth) verticalDepth = vertex.Depth;

                    if (First.Y < Second.Y)
                    { // down side: so move last bit to down

                        if (m_Velocity.Y > 0)
                        {
                            m_Velocity.Y = verticalDepth;
                        }
                        else
                        {
                            verticalDepth = 0;
                        }
                    }
                    else // up side: so move last bit to up
                    {
                        if (m_Velocity.Y < 0)
                        {
                            verticalDepth = -verticalDepth;
                            m_Velocity.Y = verticalDepth;
                        }
                        else
                        {
                            verticalDepth = 0;
                        }
                    }
                }
            }
            m_Colision.X += horizontalDepth / 2;
            m_Colision.Y += verticalDepth / 2;
        }
        public List<Vertexs> GetVertexs()
        {
            return IntelizeVertexs();
        }
        public void SetVertexs(List<Vertexs> vertexs)
        {
            m_Vertexs = vertexs;
        }
        private List<Vertexs> IntelizeVertexs()
        {
            // Player values
            float pWidth = m_Colision.Width;
            float pHeight = m_Colision.Height;
            float vertexsLength = 10; // length of a vertic
            float spred = 5; //distence from the corcenrs of a rectangle
            PointF pCenter = new PointF(m_Colision.X + pWidth / 2, m_Colision.Y + pHeight / 2);

            // Define the four corners of the player's bounding box
            PointF topLeft = new PointF(pCenter.X - pWidth / 2, pCenter.Y - pHeight / 2);
            PointF topRight = new PointF(pCenter.X + pWidth / 2, pCenter.Y - pHeight / 2);
            PointF bottomLeft = new PointF(pCenter.X - pWidth / 2, pCenter.Y + pHeight / 2);
            PointF bottomRight = new PointF(pCenter.X + pWidth / 2, pCenter.Y + pHeight / 2);

            // Create Vertexs List
            List<Vertexs> vertexsList = new List<Vertexs>();

            // Create Vertexs extending from the player's corners + spred from the corner to center
            //topLeft
            Vertexs v1 = new Vertexs(new PointF(topLeft.X, topLeft.Y + spred),
                new PointF(topLeft.X - vertexsLength, topLeft.Y + spred)); // Extend left from topLeft
            Vertexs v2 = new Vertexs(new PointF(topLeft.X + spred, topLeft.Y),
                new PointF(topLeft.X + spred, topLeft.Y - vertexsLength)); // Extend up from topLeft
            //topRight
            Vertexs v3 = new Vertexs(new PointF(topRight.X, topRight.Y + spred),
                new PointF(topRight.X + vertexsLength, topRight.Y + spred)); // Extend right from topRight
            Vertexs v4 = new Vertexs(new PointF(topRight.X - spred, topRight.Y),
                new PointF(topRight.X - spred, topRight.Y - vertexsLength)); // Extend up from topRight
            //bottomRight
            Vertexs v5 = new Vertexs(new PointF(bottomRight.X, bottomRight.Y - spred),
                new PointF(bottomRight.X + vertexsLength, bottomRight.Y - spred)); // Extend right from bottomRight
            Vertexs v6 = new Vertexs(new PointF(bottomRight.X - spred, bottomRight.Y),
                new PointF(bottomRight.X - spred, bottomRight.Y + vertexsLength)); // Extend down from bottomRight
            //bottomLeft
            Vertexs v7 = new Vertexs(new PointF(bottomLeft.X, bottomLeft.Y - spred),
                new PointF(bottomLeft.X - vertexsLength, bottomLeft.Y - spred)); // Extend left from bottomLeft
            Vertexs v8 = new Vertexs(new PointF(bottomLeft.X + spred, bottomLeft.Y),
                new PointF(bottomLeft.X + spred, bottomLeft.Y + vertexsLength)); // Extend down from bottomLeft
/*

            // Create Vertexs extending from the player's corners
            //topLeft
            Vertexs v1 = new Vertexs(topLeft,
                new PointF(topLeft.X - vertexsLength, topLeft.Y)); // Extend left from topLeft
            Vertexs v2 = new Vertexs(topLeft,
                new PointF(topLeft.X, topLeft.Y - vertexsLength)); // Extend up from topLeft
            //topRight
            Vertexs v3 = new Vertexs(topRight,
                new PointF(topRight.X + vertexsLength, topRight.Y)); // Extend right from topRight
            Vertexs v4 = new Vertexs(topRight, 
                new PointF(topRight.X, topRight.Y - vertexsLength)); // Extend up from topRight
            //bottomRight
            Vertexs v5 = new Vertexs(bottomRight,
                new PointF(bottomRight.X + vertexsLength, bottomRight.Y)); // Extend right from bottomRight
            Vertexs v6 = new Vertexs(bottomRight,
                new PointF(bottomRight.X, bottomRight.Y + vertexsLength)); // Extend down from bottomRight
            //bottomLeft
            Vertexs v7 = new Vertexs(bottomLeft, 
                new PointF(bottomLeft.X - vertexsLength, bottomLeft.Y)); // Extend left from bottomLeft
            Vertexs v8 = new Vertexs(bottomLeft, 
                new PointF(bottomLeft.X, bottomLeft.Y + vertexsLength)); // Extend down from bottomLeft
*/
            // Add the created Vertexs to the list
            vertexsList.Add(v1);
            vertexsList.Add(v2);
            vertexsList.Add(v3);
            vertexsList.Add(v4);
            vertexsList.Add(v5);
            vertexsList.Add(v6);
            vertexsList.Add(v7);
            vertexsList.Add(v8);

            VerticxDebugRects.Clear();
            float with = 2;
            VerticxDebugRects.Add(
                new RectangleF(v1.First.X, v1.First.Y, v1.Second.X - v1.First.X, v1.Second.Y - v1.First.Y - with));
            VerticxDebugRects.Add(
                new RectangleF(v2.First.X, v2.First.Y, v2.Second.X - v2.First.X - with, v2.Second.Y - v2.First.Y));
            VerticxDebugRects.Add(
                new RectangleF(v3.First.X, v3.First.Y, v3.Second.X - v3.First.X, v3.Second.Y - v3.First.Y + with));
            VerticxDebugRects.Add(
                new RectangleF(v4.First.X, v4.First.Y, v4.Second.X - v4.First.X - with, v4.Second.Y - v4.First.Y ));
            VerticxDebugRects.Add(
                new RectangleF(v5.First.X, v5.First.Y, v5.Second.X - v5.First.X , v5.Second.Y - v5.First.Y + with));
            VerticxDebugRects.Add(
                new RectangleF(v6.First.X, v6.First.Y, v6.Second.X - v6.First.X + with, v6.Second.Y - v6.First.Y ));           
            VerticxDebugRects.Add(
                new RectangleF(v7.First.X, v7.First.Y, v7.Second.X - v7.First.X , v7.Second.Y - v7.First.Y - with));           
            VerticxDebugRects.Add(
                new RectangleF(v8.First.X, v8.First.Y, v8.Second.X - v8.First.X + with, v8.Second.Y - v8.First.Y ));

            return vertexsList;
        }
    }
}
