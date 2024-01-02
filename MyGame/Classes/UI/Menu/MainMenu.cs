using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MyUtils;
using MyMap;
using System;
using Microsoft.Xna.Framework.Input;
namespace MyMenu
{
    internal class MainMenu
    {
        RectangleF m_Screen;
        List<RectangleF> m_MapButtons;
        List<Color> m_Colors;
        string[] m_ButtonNames;
        RectangleF m_Exit;
        bool m_MenuVisible;
        int m_SelectedMap;
        public MainMenu(RectangleF screen)
        {
            m_MenuVisible = true;
            m_Screen = screen;
            CreateButtons(Enum.GetValues(typeof(MapTypes)).Length, Enum.GetNames(typeof(MapTypes)));
        }
        private void CreateButtons(int amount,string[] names)
        {
            m_ButtonNames = names;
            float w = 400, h = 100, m = 20;
            float startY = m_Screen.Height / 2 - (amount * h + (amount - 1) * m) / 2;

            m_MapButtons = new List<RectangleF>();
            m_Colors = new List<Color>();
            
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                float x = m_Screen.Width / 2 - w / 2; // Center horizontally
                float y = startY + i * (h + m); // Adjust 'someSpacing' if needed

                Color randomColor = new Color(
                     (byte)random.Next(128),
                     (byte)random.Next(128),
                     (byte)random.Next(128));

                m_Colors.Add(randomColor);
                m_MapButtons.Add(new RectangleF(x, y, w, h));
            }

        }
        public bool GetVisibleState()
        {
            return m_MenuVisible;
        }
        private int MousePressedButton(List<RectangleF> rects)
        {
            int i = -1;
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (RectangleF rect in rects)
                {
                    i++;
                    if (Colision.PointInRect(new PointF(mouseState.Position.X, mouseState.Position.Y), rect))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public void Draw()
        {
            if (!m_MenuVisible) return;
            int i = -1;
            foreach (RectangleF item in m_MapButtons)
            {
                i++;

                UtilsStatic.NewPush();

                UtilsStatic.PushTranslate(item.X - item.Width / 2, item.Y - item.Height / 2);
                UtilsStatic.SetColor(m_Colors[i]);
                UtilsStatic.DrawRect(new RectangleF(item.Width / 2, item.Height / 2, item.Width, item.Height));
                UtilsStatic.PopMatrix();
            }
        }
        public int Update()
        {
            if (!m_MenuVisible) return -1;
            int map = MousePressedButton(m_MapButtons);
            if(map >= 0)
            {
                switch (map)
                {
                    case 0:
                        return (int)MapTypes.TestMap;
                    case 1:
                        return (int)MapTypes.Tutorial;
                    case 2:
                        return (int)MapTypes.Level1;
                    default:
                        return -1;
                        break;
                }
            }
            return -1;
        }
        public void SetVisibiltyMenu(bool visible)
        {
            m_MenuVisible = visible;
        }
        public void DrawNames() 
        {

        }

        public void ChoseStage()
        {
          
        }
    }
}
