using Microsoft.Xna.Framework;
using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    internal interface HasAnimations
    {
        enum CurantMovment {non }

        private void TextureUpdate(float elapsedSec, float animationDuration)
        {
        }
        private void SetAnimation(CurantMovment movment)
        {
        }
        private void SetAnimationTextures(int x, int y)
        {
        }
        private CurantMovment GetAnimationType()
        {
            return CurantMovment.non;
        }
    }
}
