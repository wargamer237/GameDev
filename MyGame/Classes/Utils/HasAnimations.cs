using Microsoft.Xna.Framework;
using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    internal class HasAnimations
    {
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

        RectangleF m_DrawRect;
        Rectangle m_SourceRect;//rect of cuting out texture for animations
        AnimationHandeler m_AnimationHandeler;

        private void TextureUpdate(float elapsedSec, float animationDuration)
        {
            SetAnimation(GetAnimationType());

            m_AnimationHandeler.UpdateTexture(elapsedSec, animationDuration);
            m_SourceRect = m_AnimationHandeler.GetSourceRect();
        }
        private void SetAnimation(CurantMovment movment)
        {

        }
        private void SetAnimationTextures(int x, int y)
        {

        }
        private CurantMovment GetAnimationType()
        {
            return CurantMovment.Idel;
        }
    }
}
