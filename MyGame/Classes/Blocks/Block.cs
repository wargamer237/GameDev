using MyUtils;//HAS MY RectF :)
using Microsoft.Xna.Framework; //HAS NO RectF :'(
using MyBlocks;

namespace MyBlocks
{
    public enum BlockType
    {
        Nothing = 0,
        Ground = 1,
        Platform = 2,
        BackGround = 3,
    }
    public enum TileType
    {
        LeftTop = 1,
        CenterTop = 2,
        RightTop = 3,
        LeftCenter = 4,
        CenterCenter = 5,
        RightCenter = 6,
        LeftBottom = 7,
        CenterBottom = 8,
        RightBottom = 9
    }
    public class Block
    {
        protected RectangleF m_Rect;
        protected Color m_Color;
        protected string m_TexturePath;
        protected string m_Tile;
        protected Rectangle m_SrtRect;
        protected bool m_IsBlock;
        public Block()
        {
        }
        public Block(RectangleF rect)
        {
            m_Rect = rect;
            this.IsBlock(false);
            this.SetColor(Color.Yellow);
            this.SetTexture("");
        }
        public Block(RectangleF rect, Rectangle sourceRect, int tileType)
        {
            m_Rect = rect;
            this.IsBlock(false);
            this.SetColor(Color.Transparent);
            this.SetSourceRect(sourceRect);
            this.SetTexture("");
        }
        public virtual void Draw()
        {
            if (!m_IsBlock) return;

            //UtilsStatic.NewPush();

            UtilsStatic.PushTranslate(m_Rect.X, m_Rect.Y);
            
            UtilsStatic.SetColor(m_Color);
            UtilsStatic.DrawTexture(new RectangleF(0, 0, m_Rect.Width, m_Rect.Height), m_TexturePath);
            UtilsStatic.ResetColor();
            UtilsStatic.PushTranslate(-m_Rect.X, -m_Rect.Y);//coment OR THIS 
            //UtilsStatic.PopMatrix();
        }
        /* --!CHECK
        MOVE OBJECT
        public void Update(float elapsedSec)
        {
            //FUTURE BART ISUE XD.
        }*/
        public RectangleF GetRect()
        {
            return m_Rect;
        }
        //SETTERS
        protected void SetColor(Color color)
        {
            m_Color = color;
        }
        protected void SetTexture(string path)
        {
            m_TexturePath = path;
        }
        public void SetSourceRect(Rectangle Sourse)
        {
            m_SrtRect = Sourse;
        }
        //YES OR NO FUNCTIONS
        protected void IsBlock(bool isBlock)
        {
            m_IsBlock = isBlock;
        }
    }
}