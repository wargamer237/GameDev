using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace MyUtils//DrawingClasses
{
    public struct PointF
    {
        public float X;
        public float Y;
        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(PointF a, PointF b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PointF a, PointF b)
        {
            return !(a == b);
        }
    }
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Width == b.Width)
            {
                return a.Height == b.Height;
            }

            return false;
        }

        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }
    }
    public static class UtilsStatic
    {
        // INTELIZE VARS
        //TEXTURE VARS
        //TEXTRUES THAT ARE CUNRATY USED FOR DRAWING A OBJ
        private static Texture2D m_ColorTexture;
        private static Texture2D m_CurantTexture;
        //LIST OF ALL EXISTING AND USED TEXTURES reduce coppyes of a texture
        private static List<Texture2D> m_ListTextures;
        private static List<string> m_ListTexturePaths;
        //GRAFICAL CLASSES FROM MONOGAME
        private static GraphicsDeviceManager m_intelize;
        private static SpriteBatch m_Graph;
        //COLORS
        private static Color m_Color;
        private static Color m_ResetColor;
        //SCREAN VARS
        private static RectangleF m_Screen;
        private static bool m_ScreenOnlyDraw;
        public static RectangleF ScreenSize
        {
            get { return m_Screen; }
            set
            {
                m_Screen.X = value.X;
                m_Screen.Y = value.Y;
                m_Screen.Width = value.Width;
                m_Screen.Width = value.Height;
            }
        }
        public static bool ScreenOnlyDraw
        {
            get { return m_ScreenOnlyDraw; }
            set { m_ScreenOnlyDraw = value; }
        } //SETTER screen drawing type
        //CONSTRUTORS 
        static UtilsStatic()
        {
            m_ResetColor = Color.White;
            m_ListTextures = new List<Texture2D>();
            m_ListTexturePaths = new List<string>();
            m_ScreenOnlyDraw = false;
            PopMatrix();
        }

        public static void SetScreen(GraphicsDeviceManager intelize, SpriteBatch graph)
        {
            m_Graph = graph;
            m_intelize = intelize;
            m_Screen = new RectangleF();
            m_Screen.Height = m_intelize.PreferredBackBufferHeight;
            m_Screen.Width = m_intelize.PreferredBackBufferWidth;
            Intelize();
        }
        public static void SetScreenSize(int width, int height)
        {
            m_intelize.PreferredBackBufferWidth = width;
            m_intelize.PreferredBackBufferHeight = height;
            m_Screen.Width = width;
            m_Screen.Height = height;
            m_intelize.ApplyChanges();
        }
        //PRIAVE FUNCTIONS
        //INTELIZE VARIABLES
        private static void Intelize()
        {
            m_Start = true;
            m_ColorTexture = new Texture2D(m_intelize.GraphicsDevice, 1, 1);
            m_ColorTexture.SetData(new[] { m_ResetColor });

            m_CurantVectors = new SRTVector();
            m_ListVectors = new VectorsListMagment();
            m_MTransformations = new Matrix();

            m_CurantVectors = new SRTVector();
        }
        public static SpriteBatch GetGraph()
        {
            return m_Graph;
        }
        // POP MATRIX, PUSH MATRIX
        //var for the stack SRT
        static Matrix m_MTransformations;
        static VectorsListMagment m_ListVectors;
        static SRTVector m_CurantVectors;
        static SRTVector m_TotaalVectors;

        static bool m_Start;
        static bool m_UpdatedTotaalVectors;
        static int m_Depth = 0;
        //This function updates the SRT matrix used for transformations if
        //the current vectors are different from the previous ones.
        public static void SetTransformationsMatrix()
        {
            if (m_Start) m_Start = false;

            m_MTransformations = GetSRTMatrix(m_TotaalVectors);
        }
        //This function Setts the total SRT vector
        //based on the calculated SRT vecotors
        //This function returns a matrix for scale, rotation, and translation transformations.
        private static Matrix GetSRTMatrix(SRTVector vectors)
        {
            Matrix scaleM = Matrix.CreateScale(vectors.Scale);
            Matrix rotateM = Matrix.CreateFromYawPitchRoll(vectors.Rotate.X, vectors.Rotate.Y, vectors.Rotate.Z);
            Matrix translateM = Matrix.CreateTranslation(vectors.Translate);
            Matrix totaal = scaleM * rotateM * translateM;
            // Return the SRT transformation matrix
            return totaal;
        }
        private static void SetTotaalSRTVector()
        {
            if (!m_UpdatedTotaalVectors) return;
            m_UpdatedTotaalVectors = false;
            m_TotaalVectors = GetTotaalSRTVector();
        }
        //This function calculates the total SRT vector
        //based on all previous and current transformations.
        private static SRTVector GetTotaalSRTVector()
        {
            SRTVector totaalSRT = new SRTVector();
            List<SRTVector> vectors = m_ListVectors.GetList();

            for (int i = 0; i < vectors.Count; i++)
            {
                SRTVector vector = new SRTVector(vectors[i]);

                AddRotationTranslate(ref vector.Translate, vector.Rotate.Z);
                totaalSRT.Translate += vector.Translate;
            }
            SRTVector curantVector = new SRTVector(m_CurantVectors);

            AddRotationTranslate(ref curantVector.Translate, curantVector.Rotate.Z);
            totaalSRT += curantVector;
            totaalSRT.Scale = Vector3.One;
            return totaalSRT;
        }
        //This function adds a this.Rotation to a this.Translation of a vector.
        public static void AddRotationTranslate(ref Vector3 vector, float rad)
        {
            float newX = vector.X * MathF.Cos(rad) - vector.Y * MathF.Sin(rad);
            float newY = vector.Y * MathF.Cos(rad) + vector.X * MathF.Sin(rad);
            vector.X = newX;
            vector.Y = newY;
            vector.Z = 0;
        }
        //public static FUNCTION TO ADD TRANSFORAMTIONS
        //This function adds a new transformation. 
        public static void NewPush()
        {
            ++m_Depth;
            m_UpdatedTotaalVectors = true;
            if (m_Depth == 1) return;
            m_ListVectors.Add(m_CurantVectors.Scale, m_CurantVectors.Rotate, m_CurantVectors.Translate);
            m_CurantVectors = new SRTVector();
        }
        //This function sets the scale of the current transformation.
        private static void PushScale(float x, float y)
        {
            //NOT WORKING!!!
            //m_UpdatedTotaalVectors = true;
            //m_CurantVectors.Scale = new Vector3(x, y, 1);
            //m_CurantVectors.Scale = new Vector3(x, y, 1);
        }
        //This function adds to the rotation of the current transformation in degrees.
        public static void PushRotation(float deg)
        {
            m_UpdatedTotaalVectors = true;
            //deg * (MathF.PI / 180), - is for couter clock wise (left)
            PushRotationRad(MathHelper.ToRadians(-deg));
        }
        //This function adds to the rotation of the current transformation in radians.
        public static void PushRotationRad(float rad)
        {
            m_UpdatedTotaalVectors = true;
            m_CurantVectors.Rotate += new Vector3(0, 0, rad);
        }
        //This function sets the translation of the current transformation.
        public static void PushTranslate(float x, float y = 0)
        {
            m_UpdatedTotaalVectors = true;
            m_CurantVectors.Translate += new Vector3(x, y, 0);
        }
        // This function ends the current transformation and set to the last transformation.
        // And then it pops the last transformation vectors from the list.
        public static void PopMatrix()
        {
            m_UpdatedTotaalVectors = true;
            --m_Depth;
            if (m_Depth < 0) m_Depth = 0;
            m_CurantVectors = new SRTVector();
            if (m_Depth == 0)
            {
                m_ScreenOnlyDraw = false;
                return;
            }
            m_CurantVectors = m_ListVectors.GetAll();
            m_ListVectors.Pop();
        }
        //LIST MAGMENT

        // COLOR MANGEMENT
        //This function sets the drawing color.
        public static void SetColor(Color color)
        {
            m_Color = color;
        }
        //This function sets the drawing color based on RGB and alpha values.
        public static void SetColor(int r, int g, int b, int a)
        {
            m_Color = new Color(r, g, b, a);
        }
        //This function resets the drawing color to its original value.
        public static Color ResetColor()
        {
            return m_Color = m_ResetColor;
        }
        //DRAW
        //This function begins drawing using the current transformations
        private static void StartDraw()
        {
            SetTransformationsMatrix();
            m_Graph.Begin(SpriteSortMode.Texture,
                null, null, null, null, null,
                m_MTransformations);
        }
        //This function ends the drawing.
        private static void StopDraw()
        {
            m_Graph.End();
        }
        //This function draws a rectangle with or without a texture using the current transformations.
        private static void Draw(RectangleF rect, string path)
        {
            SetTotaalSRTVector();
            //if (!IsInScreen(rect)) return;
            Texture2D texture = m_ColorTexture;
            if (path != "")
            {
                SetTexture(path);
                texture = m_CurantTexture;
            }

            StartDraw();
            DrawClass.DrawRect(rect, m_Color, m_Graph, texture);
            StopDraw();
        }
        private static void Draw(RectangleF rect, string path, Rectangle sourceRect, SpriteEffects flipDirection )
        {
            SetTotaalSRTVector();
            //if (!IsInScreen(rect)) return;
            Texture2D texture = m_ColorTexture;
            if (!string.IsNullOrEmpty(path))
            {
                SetTexture(path);
                texture = m_CurantTexture;
            }

            StartDraw();
            DrawClass.DrawRect(rect, m_Color, m_Graph, texture, sourceRect, flipDirection);
            StopDraw();
        }
        //DRAW RECTANGLE WITH COLOR
        //This function draws a rectangle without a texture but with color using the current transformations.
        public static void DrawRect(RectangleF rect)
        {
            Draw(rect, "");
        }
        //DRAW TEXTURE
        //This function draws a rectangle with a texture using the current transformations.
        public static void DrawTexture(RectangleF rect, string path)
        {
            Draw(rect, path);
        }
        public static void DrawTexture(RectangleF rect, Rectangle sourceRect, string path, SpriteEffects flipDirection = SpriteEffects.None)
        {
            Draw(rect, path, sourceRect, flipDirection);
        }
        //This function sets the current texture to the given path if it has not been loaded yet.
        private static void SetTexture(string path)
        {
            int index = -1;
            if (m_ListTexturePaths.Count != 0)
            {
                index = TextureExists(path);
            }
            if (index == -1)
            {
                m_ListTexturePaths.Add(path);
                m_CurantTexture = DrawClass.GetTexture(path, m_Graph);
                m_ListTextures.Add(m_CurantTexture);
            }
            else
            {
                m_CurantTexture = m_ListTextures[index];
            }
        }
        //check if a texture exist in the list or not
        private static int TextureExists(string path)
        {
            for (int i = 0; i < m_ListTexturePaths.Count; i++)
            {
                if (m_ListTexturePaths[i] == path)
                {
                    return i;
                }
            }
            return -1;
        }
        //DRAW ONLY IN SCREEN : MABY UPDATE
        //Check If Blocks are in Screen IF its m_ScreenOnlyDraw is true
        /*public static bool IsInScreen(RectangleF rect)
        {
            SetTotaalSRTVector();
            Vector3 translate = m_TotaalVectors.Translate;
            rect.X += translate.X;
            rect.Y += translate.Y;
            
            // GET CENTER OF THE RECT AND THEN ADD THE TRANSLATION 

            RectangleF border = new RectangleF(
                -rect.Width, -rect.Height,
                m_Screen.Width + rect.Width * 3, m_Screen.Height + rect.Height * 3
            );

            if (Colision.PointInRect(GetCenterRect(rect), border))
            {
                return true;
            }
            return false;
        }*/

        //GET POINT INFROMATION 
        //CENTER OF ALL POINTS
        public static PointF GetCenterOfPoints(List<Vector2> vertexes)
        {
            if (vertexes == null || vertexes.Count == 0) throw new ArgumentException("List of points cannot be null or empty");

            float totalX = 0;
            float totalY = 0;

            foreach (Vector2 point in vertexes)
            {
                totalX += point.X;
                totalY += point.Y;
            }

            float centerX = totalX / vertexes.Count;
            float centerY = totalY / vertexes.Count;

            return new PointF(centerX, centerY);
        }
        public static PointF GetCenterRect(RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2); ;
        }

        // Custom Contains method for RectangleF to check if a point is inside the rectangle.
        public static bool Contains(this RectangleF rect, PointF point)
        {
            return point.X >= rect.X && point.X <= (rect.X + rect.Width) && point.Y >= rect.Y && point.Y <= (rect.Y + rect.Height);
        }
        public static Vector2 GetDirection(PointF p1, PointF p2)
        {
            return GetDirection(p1.X, p1.Y, p2.X, p2.Y);
        }
        public static Vector2 GetDirection(Vector2 p1, Vector2 p2)
        {
            return GetDirection(p1.X, p1.Y, p2.X, p2.Y);
        }
        public static Vector2 GetDirection(float x1, float y1, float x2, float y2)
        {
            float vx = x2 - x1;
            float vy = y2 - y1;

            return new Vector2(vx, vy);
        }
    }
}