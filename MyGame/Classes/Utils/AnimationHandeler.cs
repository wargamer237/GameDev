using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MyUtils
{
    internal class AnimationHandeler
    {
        string m_Path;
        Point m_MaxTextures;
        Rectangle m_SourseRect;
        Rectangle m_TextureSize;
        List<int> m_RowsTexture;
        //TEXTURE POSTIONS IN GRID AND CONDITIONS
        float m_CurantTime;
        int m_CurantRow;
        int m_CurantColum;
        int m_MinRange;
        int m_MaxRange;
        public AnimationHandeler(string path, Point grindXY, int margin = 0)
        {
            m_CurantRow = -1;
            m_Path = path;
            Texture2D texture = DrawClass.GetTexture(m_Path, UtilsStatic.GetGraph());
            m_TextureSize = new Rectangle(0, 0, texture.Width, texture.Height);
            m_MaxTextures = grindXY;
            m_RowsTexture = new List<int>();
            SetSourceRectSize();
        }
        void SetSourceRectSize()
        {
            m_SourseRect.Width = (int)m_TextureSize.Width / m_MaxTextures.X;
            m_SourseRect.Height = (int)m_TextureSize.Height / m_MaxTextures.Y;
        }
        void UpdateSourceRectPos()
        {
            m_SourseRect.X = m_SourseRect.Width * m_CurantColum;
            m_SourseRect.Y = m_SourseRect.Height * m_CurantRow;
        }
        public void AddRow(int animationCount)
        {
            animationCount--;
            if(m_RowsTexture.Count > 0 && m_CurantRow > 0 && m_CurantRow < m_RowsTexture.Count)
            {
                if (m_RowsTexture[m_CurantRow] <= 0)
                {
                    animationCount = -1;
                }
                if (animationCount > m_RowsTexture[m_CurantRow])
                {
                    animationCount = m_RowsTexture[m_CurantRow];
                }
            }
            
            m_RowsTexture.Add(animationCount);
        }

        public bool UpdateTexture(float elapsedSec, float animationDuration)
        {
            // if Row has no animations : NOT USING
            if (m_MaxRange == -1) return true;
            m_CurantTime += 2 * elapsedSec;
            //time did not run out
            if (m_CurantTime < animationDuration) return false;
            m_CurantTime = 0;
            //reset
            if (m_MaxRange == m_CurantColum) m_CurantColum = m_MinRange;
            m_CurantColum++;

            UpdateSourceRectPos();
            return true;
        }
        public void SetAnimation(int row, int startingColum = 0, int columMin = 0, int columMax = -1)
        {
            //if animation in use
            if (m_CurantRow == row) return;
            m_CurantRow = row;
            //ROWS
            m_CurantTime = 0;
            m_CurantRow = row;
            //COLUM
            //if to low Value
            //then closests to the boundery [min = 0][max = MaxAnimations]
            if (columMin < 0) columMin = 0;
            if (columMax < 0 || columMax > m_RowsTexture[m_CurantRow]) columMax = m_RowsTexture[m_CurantRow];

            m_CurantColum = startingColum;
            m_MinRange = columMin;
            m_MaxRange = columMax;
            
            UpdateSourceRectPos();
        } 
        public Rectangle GetSourceRect()
        {
            return m_SourseRect;
        }
    }
}
