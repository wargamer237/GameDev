using Microsoft.Xna.Framework;
using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    internal interface HasAnimations <animation>
    {
        private void TextureUpdate(float elapsedSec, float animationDuration)
        {

        }
        private void SetAnimation(animation movment)
        {

        }
        private void SetAnimationTextures(int x, int y)
        {

        }
        private animation GetAnimationType()
        {
            throw new Exception();
        }
    }
}
