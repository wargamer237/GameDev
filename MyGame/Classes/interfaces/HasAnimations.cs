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
        private void IntelizeAnimations(int maxWidth, int maxHeight)
        {

        }
        private void TextureUpdate(float elapsedSec, float animationDuration)
        {

        }
        private void SetAnimation(animation movment)
        {

        }
        private animation GetAnimationType()
        {
            throw new Exception();
        }
    }
}
