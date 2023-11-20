using MyBlocks;
using Microsoft.Xna.Framework;
using MyUtils;

namespace MyBlocks
{
    internal class Ground : Block, ICollidable
    {
        public Ground() 
            : base()
        {
            this.IsBlock(false);
        }
        public Ground(RectangleF rect, int tileType) 
            : base(rect)
        {
            this.IsBlock(true);
            this.SetColor(Color.Yellow);
            this.SetTexture($"Textures/Tiles/Ground/{tileType}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
        }
    }
}