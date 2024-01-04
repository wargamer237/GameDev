using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyClass.MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCreature
{
    public enum Creatures
    {
        Player = 6,
        Robot = 7,
        Bird = 8,
        Spike = 9,
        Goal = 10
    }
    internal class Creature
    {
        //location
        protected PointF m_StartPosition;
        protected Vector2 m_Velocity;
        protected Vector2 m_ExternVelocity;
        //speed
        protected float m_Speed;
        protected float m_MaxSpeed;
        protected float m_Resistents;
        protected float m_Gravity;
        //texture
        protected string m_TexturePath;
        protected bool m_LookRight;
        protected RectangleF m_DrawRect; // drawing size
        protected Rectangle m_SourceRect; // cut out
        //COLLISIONS VARS
        protected RectangleF m_ColisionRect;
        protected List<Vertexs> m_Vertexs;
        protected List<RectangleF> m_VerticxDebugRects;
        //Hp stats
        protected int m_Health;
        protected bool m_Dead;
        protected float m_DeathDuration;
        protected bool m_GotHit;
        //GETTERS SETTERS VARS
        //location

        public Creature() 
        {
            Intelize();
            SetColisonRect(m_DrawRect);
            IntelizeVertexs();
        }
        public Creature(RectangleF creatureRect) 
        {
            m_Health = 5;
            m_Dead = false;
            Intelize();
            m_DrawRect = creatureRect;
            m_StartPosition.X = creatureRect.X;
            m_StartPosition.Y = creatureRect.Y;
            SetColisonRect(m_DrawRect);
            IntelizeVertexs();
        }
        //INTELIZE
        virtual protected void Intelize()
        {

            m_VerticxDebugRects = new List<RectangleF>();
            // down right test
            m_StartPosition = new PointF(200, -300);
            m_DrawRect.X = m_StartPosition.X;
            m_DrawRect.Y = m_StartPosition.Y;

            m_DrawRect.Width = 100;
            m_DrawRect.Height = 100;
            m_LookRight = true;
        }
        protected List<Vertexs> IntelizeVertexs()
        {
            // Player values
            float pWidth = m_ColisionRect.Width;
            float pHeight = m_ColisionRect.Height;
            float vertexsLength = 10; // length of a vertic
            float spred = 5; //distence from the corcenrs of a rectangle
            PointF pCenter = new PointF(m_ColisionRect.X + pWidth / 2, m_ColisionRect.Y + pHeight / 2);

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
            // Add the created Vertexs to the list
            vertexsList.Add(v1);
            vertexsList.Add(v2);
            vertexsList.Add(v3);
            vertexsList.Add(v4);
            vertexsList.Add(v5);
            vertexsList.Add(v6);
            vertexsList.Add(v7);
            vertexsList.Add(v8);

            m_VerticxDebugRects.Clear();
            float with = 2;
            m_VerticxDebugRects.Add(
                new RectangleF(v1.First.X, v1.First.Y, v1.Second.X - v1.First.X, v1.Second.Y - v1.First.Y - with));
            m_VerticxDebugRects.Add(
                new RectangleF(v2.First.X, v2.First.Y, v2.Second.X - v2.First.X - with, v2.Second.Y - v2.First.Y));
            m_VerticxDebugRects.Add(
                new RectangleF(v3.First.X, v3.First.Y, v3.Second.X - v3.First.X, v3.Second.Y - v3.First.Y + with));
            m_VerticxDebugRects.Add(
                new RectangleF(v4.First.X, v4.First.Y, v4.Second.X - v4.First.X - with, v4.Second.Y - v4.First.Y));
            m_VerticxDebugRects.Add(
                new RectangleF(v5.First.X, v5.First.Y, v5.Second.X - v5.First.X, v5.Second.Y - v5.First.Y + with));
            m_VerticxDebugRects.Add(
                new RectangleF(v6.First.X, v6.First.Y, v6.Second.X - v6.First.X + with, v6.Second.Y - v6.First.Y));
            m_VerticxDebugRects.Add(
                new RectangleF(v7.First.X, v7.First.Y, v7.Second.X - v7.First.X, v7.Second.Y - v7.First.Y - with));
            m_VerticxDebugRects.Add(
                new RectangleF(v8.First.X, v8.First.Y, v8.Second.X - v8.First.X + with, v8.Second.Y - v8.First.Y));

            SetVertexs(vertexsList);
            return vertexsList;
        }
        
        //DRAW
        public virtual void Draw()
        {
            SpriteEffects directions = SpriteEffects.None;
            if (!m_LookRight)
                directions = SpriteEffects.FlipHorizontally;
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(m_DrawRect.X - m_DrawRect.Width / 2, m_DrawRect.Y - m_DrawRect.Height / 2);
            /* DEBUG /
            UtilsStatic.SetColor(Color.Red);
            UtilsStatic.DrawRect(new RectangleF(m_DrawRect.Width / 2, m_DrawRect.Height / 2, m_DrawRect.Width, m_DrawRect.Height));          
            UtilsStatic.SetColor(Color.Black);
            //*/
            UtilsStatic.DrawTexture(new RectangleF(m_DrawRect.Width / 2, m_DrawRect.Height / 2, m_DrawRect.Width, m_DrawRect.Height)
                , m_SourceRect, m_TexturePath, directions);

            UtilsStatic.PopMatrix();

            //DEBUG VERTIX
            foreach (RectangleF item in m_VerticxDebugRects)
            {
                UtilsStatic.SetColor(Color.Yellow);
                UtilsStatic.DrawRect(item);
                UtilsStatic.ResetColor();
            }
        }
        //MACANICS
        protected float SpeedLimit(float velocity, float limit, float directionSpeed = 0)
        {
            limit = MathF.Max(limit, limit * MathF.Abs(directionSpeed));
            if (velocity > limit)
            {
                velocity = limit;
            }
            else if (velocity < -limit)
            {
                velocity = -limit;
            }
            return velocity;
        }
        //UPDATE
        public virtual void Update(float elapsedSec) 
        {
            if (m_Dead) return;
            //Gravity standard
            m_Velocity.Y += m_Gravity * elapsedSec;
            m_Velocity.Y = SpeedLimit(m_Velocity.Y, MathF.Abs(m_Gravity));
            
            //Check colisions
            UpdateColision(ref m_DrawRect);
            UpdateRects(elapsedSec);
        }
        /// <summary>
        /// Its working!!.
        /// it check colison of the vertics and 
        /// if it hit somthing and the directions of the velocty is the same 
        /// then it applys the efect of hiting a wal (snap the wall in 2 steps)
        /// if you change direction it will check that sinde IF THERE IS COLISION
        /// (ps: all vertics are always checking for colison: see maphandeler)
        /// </summary>
        protected void UpdateColision(ref RectangleF rect)
        {
            float horizontalDepth = 0;
            float verticalDepth = 0;
            foreach (Vertexs vertex in m_Vertexs)
            {
                if (!vertex.Colided) continue; // Skip non-colliding vertexes

                PointF First = vertex.First;
                PointF Second = vertex.Second;
                Vector2 direction = vertex.GetVectorDirection();
                if (direction.Y == 0) // Horizontal Vertex
                {
                    if (horizontalDepth < vertex.Depth) horizontalDepth = vertex.Depth;

                    //LEFT
                    if (direction.X > 0)
                    {

                        if (m_ExternVelocity.X < 0)
                        {
                            if (m_Velocity.X > 0)
                            {
                                m_Velocity.X = 0;
                                horizontalDepth = 0;
                            }
                            m_ExternVelocity.X -= horizontalDepth;

                            continue;
                        }
                        else if (m_ExternVelocity.X > 0)
                        {
                            m_Velocity.X = -m_ExternVelocity.X;
                            horizontalDepth = 0;
                            continue;
                        }
                        // right side: so move last bit to right
                        if (m_Velocity.X > 0)
                        {
                            m_Velocity.X = horizontalDepth;
                        }
                        else if (m_ExternVelocity.X > 0)
                        {
                            m_ExternVelocity.X = horizontalDepth;
                        }
                        else
                        {
                            horizontalDepth = 0;
                        }

                    }
                    else // right side: so move last bit to left
                    {
                        if (m_ExternVelocity.X > 0)
                        {
                            if (m_Velocity.X < 0)
                            {
                                m_Velocity.X = 0;
                                horizontalDepth = 0;
                            }
                            m_ExternVelocity.X += horizontalDepth;
                            continue;
                        }
                        else if (m_ExternVelocity.X < 0)
                        {
                            m_Velocity.X = -m_ExternVelocity.X;
                            horizontalDepth = 0;
                            continue;
                        }
                        // left side: so move last bit to left
                        if (m_Velocity.X < 0)
                        {
                            m_Velocity.X = -horizontalDepth;
                        }
                        else if (m_ExternVelocity.X < 0)
                        {
                            m_ExternVelocity.X = -horizontalDepth;
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

                    if (direction.Y > 0)
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
            rect.X += horizontalDepth / 2;
            rect.Y += verticalDepth / 2;
        }
        protected void UpdateRects(float elapsedSec)
        {
            Vector2 totaalVelocty =
                new Vector2(
                m_Velocity.X * elapsedSec + m_ExternVelocity.X * elapsedSec,
                m_Velocity.Y * elapsedSec + m_ExternVelocity.Y * elapsedSec);
            m_DrawRect.X += totaalVelocty.X;
            m_DrawRect.Y += totaalVelocty.Y;
            m_ExternVelocity = new Vector2(0, 0);
            if (totaalVelocty.X > 0) m_LookRight = true;
            if (totaalVelocty.X < 0) m_LookRight = false;
            SetColisonRect(m_DrawRect);
        }
        //OTHER PUBLIC 
        public void SetHit(bool hit)
        {
            if (!hit) { m_GotHit = false; return; }
            if (m_Health > 0)
                m_Health--;
            m_GotHit = true;
            if (m_Health == 0) m_Dead = true;
        }
        //SETTERS
        public void SetExternVelocty(Vector2 externVelocity)
        {
            m_ExternVelocity = externVelocity;
        }
        public void SetVertexs(List<Vertexs> vertexs)
        {
            m_Vertexs = vertexs;
        }
        protected virtual void SetColisonRect(RectangleF drawRect)
        {
            float marginH = drawRect.Height / 100 * 5;
            float width = drawRect.Width / 1.9f;
            float height = drawRect.Height / 1.3f;

            m_ColisionRect.X = drawRect.X + (drawRect.Width - width) / 2;
            m_ColisionRect.Y = drawRect.Y + (drawRect.Height - height) - marginH;
            m_ColisionRect.Width = width;
            m_ColisionRect.Height = height;
        }
        //GETTERS
        public RectangleF GetRect()
        {
            return m_ColisionRect;
        }
        public List<Vertexs> GetVertexs()
        {
            return m_Vertexs;
        }
        public List<Vertexs> GetNewVertexs()
        {
            return IntelizeVertexs();
        }
        public virtual bool GetDeathState()
        {
            return m_Dead;
        }
    }
}
