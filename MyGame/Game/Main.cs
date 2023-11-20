using Microsoft.Xna.Framework;
using MyUtils;
using MyMap;
using MyPlayer;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MyHandelers;
using MyCreature;
namespace MyGame
{
    internal class Main
    {
        MapHandeler m_MapHandeler;
        CreatureManger m_CreatureManger;
        PointF m_CampPoint;
        public void Initialize()
        {
            m_MapHandeler = new MapHandeler();
            Map map = new Map();
            m_CreatureManger = new CreatureManger();
            m_MapHandeler.SetMap(map.IntedMap, map.GridLayout);
        }
        public void Draw()
        {
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(-m_CampPoint.X + UtilsStatic.ScreenSize.Width/2, -m_CampPoint.Y + UtilsStatic.ScreenSize.Height / 2);
            m_MapHandeler.DrawMap();
            m_CreatureManger.Draw();
            UtilsStatic.ResetColor();
            UtilsStatic.PopMatrix();
        }
        public void Update(float elapsedSec)
        {
            CheckKeyboardInput(elapsedSec);

            Vector2 externVelocty = new Vector2();
            m_MapHandeler.Update(elapsedSec);


            List<Creature> creatures = m_CreatureManger.GetCreatures();
            foreach (var creature in creatures)
            {
                creature.SetVertexs(m_MapHandeler.CheckMapColison(creature.GetNewVertexs(), ref externVelocty));
            }
            m_CreatureManger.SetCreatures(creatures);

            m_CreatureManger.Update(elapsedSec);
            m_CampPoint = m_CreatureManger.GetCameraTranslation();

        }
        float t = 0;
        public void CheckKeyboardInput(float elapsedSec)
        {
            if (t != 0) t += 2 * elapsedSec;
            if (t >= 0 || t == 0) t = 0;
            else return;
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                t+= 2*elapsedSec;
            }
            /*
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                elapsedSec *= 2;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                tx -= speed * elapsedSec;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                tx += speed * elapsedSec;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ty -= speed * elapsedSec;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ty += speed * elapsedSec;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                r += speed * elapsedSec * speedup;
            }
            // Add more key checks as needed...*/
        }
    }
}
