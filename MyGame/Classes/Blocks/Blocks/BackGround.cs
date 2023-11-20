using Microsoft.Xna.Framework;
using MyUtils;
using System;

namespace MyBlocks
{
    internal class BackGround : Block
    {
        public BackGround()
           : base()
        {
            this.IsBlock(false);
        }
        public BackGround(RectangleF rect, int tileType)
            : base(rect)
        {
            this.IsBlock(true);
            this.SetColor(Color.Red);
            this.SetTexture($"Textures/Tiles/Ground/{tileType}tile.png"); //bv
            this.SetSourceRect(new Rectangle());
        }
    }
}
