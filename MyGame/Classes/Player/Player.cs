using Microsoft.Xna.Framework;
using MyUtils;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;


namespace MyPlayer
{
    internal class Player : HasAnimations
    {
        PointF m_StartPosition;
        Vector2 m_Velocity;
        Vector2 m_Direction;

        float m_Speed;
        float m_Gravity;

        string m_TexturePath;
        AnimationHandeler m_AnimationHandeler;

        RectangleF m_ColisionRect;
        RectangleF m_DrawRect;
        Rectangle m_SourceRect;//rect of cuting out texture for animations

        List<Vertexs> m_Vertexs;
        List<RectangleF> VerticxDebugRects;
        public Player(RectangleF playerRect)
        {
            Intelize();
            m_ColisionRect = playerRect;

            SetVertexs(IntelizeVertexs());
        }
        public Player()
        {
            Intelize();
            SetVertexs(IntelizeVertexs());
        }
        private void Intelize()
        {
            m_TexturePath = "Textures/Player/PlayerSheet3.png";
            VerticxDebugRects = new List<RectangleF>();
            m_Speed = 1200;
            m_Gravity = 200;
            // down right test
            m_StartPosition = new PointF(200, -300);
            m_DrawRect.X = m_StartPosition.X;
            m_DrawRect.Y = m_StartPosition.Y;

            m_DrawRect.Width = 300;
            m_DrawRect.Height = 300;
            SetColisonRect(m_DrawRect);

            SetAnimationTextures(13, 15);
        }
        private List<Vertexs> IntelizeVertexs()
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
                new RectangleF(v4.First.X, v4.First.Y, v4.Second.X - v4.First.X - with, v4.Second.Y - v4.First.Y));
            VerticxDebugRects.Add(
                new RectangleF(v5.First.X, v5.First.Y, v5.Second.X - v5.First.X, v5.Second.Y - v5.First.Y + with));
            VerticxDebugRects.Add(
                new RectangleF(v6.First.X, v6.First.Y, v6.Second.X - v6.First.X + with, v6.Second.Y - v6.First.Y));
            VerticxDebugRects.Add(
                new RectangleF(v7.First.X, v7.First.Y, v7.Second.X - v7.First.X, v7.Second.Y - v7.First.Y - with));
            VerticxDebugRects.Add(
                new RectangleF(v8.First.X, v8.First.Y, v8.Second.X - v8.First.X + with, v8.Second.Y - v8.First.Y));

            return vertexsList;
        }
        //jump, sprint, esc
        enum CurantMovment
        {
            Idel = 0,
            Run = 1,
            AtackOne = 2,
            AtackTwo = 3,
            AtackThree = 4,
            Jump = 5,
            Fall = 6
        }
       
        //DRAW
        public void Draw()
        {
            SpriteEffects directions = SpriteEffects.None;
            if (m_Velocity.X < 0)
                directions = SpriteEffects.FlipHorizontally;
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(m_DrawRect.X - m_DrawRect.Width/2, m_DrawRect.Y - m_DrawRect.Height/2);
            /**/UtilsStatic.SetColor(Color.Red);
            UtilsStatic.DrawRect(new RectangleF(m_DrawRect.Width / 2, m_DrawRect.Height / 2, m_DrawRect.Width, m_DrawRect.Height));          
            UtilsStatic.SetColor(Color.Black);//*/
            UtilsStatic.DrawTexture(new RectangleF(m_DrawRect.Width/2, m_DrawRect.Height/2, m_DrawRect.Width, m_DrawRect.Height)
                , m_SourceRect, m_TexturePath, directions);
           
            UtilsStatic.PopMatrix();
            
            //DEBUG VERTIX
            foreach (RectangleF item in VerticxDebugRects)
            {
                UtilsStatic.SetColor(Color.Yellow);
                UtilsStatic.DrawRect(item);
                UtilsStatic.ResetColor();
            }
        }
        //UPDATE
        public void Update(float elapsedSec)
        {
            TextureUpdate(elapsedSec, 0.25f);
            //if up timer jump for 2s or so do no you see
            m_Velocity.Y += m_Gravity * elapsedSec;
            m_Velocity.X += m_Direction.X * m_Speed * elapsedSec;
            m_Velocity.Y += m_Direction.Y * (m_Speed + m_Gravity) * elapsedSec;
            UpdateColision(ref m_DrawRect);
            m_DrawRect.X += m_Velocity.X * elapsedSec;
            m_DrawRect.Y += m_Velocity.Y * elapsedSec;
 
            SetColisonRect(m_DrawRect);
        }
        private void TextureUpdate(float elapsedSec, float animationDuration)
        {
            SetAnimation(GetAnimationType());

            m_AnimationHandeler.UpdateTexture(elapsedSec, animationDuration);
            m_SourceRect = m_AnimationHandeler.GetSourceRect();
            m_SourceRect.Width -= m_SourceRect.Width / 100 * 10;
            m_SourceRect.Height -= m_SourceRect.Height / 100 * 20;
            m_SourceRect.Y += m_SourceRect.Height / 100 * 20;
        }
        
        /// <summary>
        /// Its working!!. it check colison of the vertics and 
        /// if it hit somthing and the directions of the velocty is the same 
        /// then it applys the efect of hiting a wal (snap the wall in 2 steps)
        /// if you change direction it will check that sinde IF THERE IS COLISION
        /// (ps: all vertics are always checking for colison: see maphandeler)
        /// </summary>
        private void UpdateColision(ref RectangleF rect) 
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
            rect.X += horizontalDepth / 2;
            rect.Y += verticalDepth / 2;
        }

        //SETTERS
        private void SetAnimation(CurantMovment movment)
        {
            switch (movment)
            {
                case CurantMovment.Idel:
                    // Code to handle idle state
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Idel);
                    break;
                case CurantMovment.Run:
                    // Code to handle running
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Run);
                    break;
                case CurantMovment.AtackOne:
                    // Code for attack one
                    break;
                case CurantMovment.AtackTwo:
                    // Code for attack two
                    break;
                case CurantMovment.AtackThree:
                    // Code for attack three
                    break;
                case CurantMovment.Jump:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Jump, 0, 2, 4);
                    break;
                case CurantMovment.Fall:
                    m_AnimationHandeler.SetAnimation((int)CurantMovment.Fall, 0, 1, 3);
                    break;
                default:
                    // Code to handle an undefined movement
                    break;
            }
            m_AnimationHandeler.SetAnimation((int)movment);
        }
        private void SetColisonRect(RectangleF drawRect)
        {
            float marginW = 0;
            float width = drawRect.Width / 1.9f;
            float height = drawRect.Height / 1.6f;
            if (m_Velocity.X < 0) marginW = -marginW;
            m_ColisionRect.X = drawRect.X + (drawRect.Width - width) / 2 - marginW;
            m_ColisionRect.Y = drawRect.Y + (drawRect.Height - height);
            m_ColisionRect.Width = width;
            m_ColisionRect.Height = height;
        }
        private void SetAnimationTextures(int x, int y)
        {
            m_AnimationHandeler = new AnimationHandeler(m_TexturePath, new Point(x, y));
            //IDEL
            m_AnimationHandeler.AddRow(13);
            //RUN
            m_AnimationHandeler.AddRow(8);
            //ATACK(1,2,3)
            m_AnimationHandeler.AddRow(10);
            m_AnimationHandeler.AddRow(10);
            m_AnimationHandeler.AddRow(10);
            //JUMP
            m_AnimationHandeler.AddRow(6);
            //FALL
            m_AnimationHandeler.AddRow(4);
            //OTHER / not used
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            m_AnimationHandeler.AddRow(-1);
            //DEATH
            m_AnimationHandeler.AddRow(9);
        }
        public void SetVelocty(Vector2 velocty)
        {
            m_Direction = velocty;
        }
        public void SetVertexs(List<Vertexs> vertexs)
        {
            m_Vertexs = vertexs;
        }
        //GETTERS
        private CurantMovment GetAnimationType()
        {
            Vector2 velocty = m_Velocity;
            if (velocty.X == 0 && velocty.Y == 0)
            {
                //stand stil
                return CurantMovment.Idel;
            }
            if (velocty.Y > 0)
            {
                return CurantMovment.Fall;
            }
            if (velocty.Y < 0)
            {
                return CurantMovment.Jump;
            }
            if (velocty.X > 0 || velocty.X < 0)
            {
                return CurantMovment.Run;
            }
            return CurantMovment.Idel;
        }
        public RectangleF GetRect()
        {
            return m_ColisionRect;
        }
        public List<Vertexs> GetVertexs()
        {
            return IntelizeVertexs();
        }
    }
}
