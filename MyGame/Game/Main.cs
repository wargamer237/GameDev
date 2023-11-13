using Microsoft.Xna.Framework;
using MyUtils;
using MyHandelers;
using MyMap;
using MyPlayer;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
namespace MyGame
{
    internal class Main
    {
        MapHandeler m_MapHandeler;
        Player m_Player;
        PlayerManger m_PlayerManger;
        List<Player> m_LPlayers;
        public void Initialize()
        {
            m_MapHandeler = new MapHandeler();
            Map map = new Map();
            m_Player = new Player();
            m_PlayerManger = new PlayerManger();
            m_LPlayers = new List<Player>();
            m_MapHandeler.SetMap(map.IntedMap, map.GridLayout);
        }
        public void Draw()
        {
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(-m_Player.GetRect().X + UtilsStatic.ScreenSize.Width/2, -m_Player.GetRect().Y + UtilsStatic.ScreenSize.Height / 2);
            m_MapHandeler.DrawMap();
            m_Player.Draw();
            //for (int i = m_LPlayers.Count -1; i >= 0; i--)
            for (int i = 0; i < m_LPlayers.Count; i++)
            {
                if (i % 2 == 0)
                {
                    UtilsStatic.SetColor(Color.Red);
                }
                if (i % 3 == 0)
                {
                    UtilsStatic.SetColor(Color.Peru);
                }
                if (i % 4 == 0)
                {
                    UtilsStatic.SetColor(Color.Orange);
                }
                m_LPlayers[i].Draw();
            }
            UtilsStatic.ResetColor();
            UtilsStatic.PopMatrix();
        }
        public void Update(float elapsedSec)
        {
            CheckKeyboardInput(elapsedSec);

            m_MapHandeler.Update(elapsedSec);
            m_Player.SetVertexs(m_MapHandeler.CheckMapColison(m_Player.GetVertexs()));
            m_Player.SetDirection(m_PlayerManger.GetDirection());
            m_Player.Update(elapsedSec);

            foreach (Player p in m_LPlayers)
            {
                p.SetVertexs(m_MapHandeler.CheckMapColison(p.GetVertexs()));
                p.Update(elapsedSec);
            }

        }
        float t = 0;
        public void CheckKeyboardInput(float elapsedSec)
        {
            if (t != 0) t += 2 * elapsedSec;
            if (t >= 1 || t == 0) t = 0;
            else return;
            KeyboardState keyboardState = Keyboard.GetState();
            m_PlayerManger.KeyEvents(keyboardState);
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                t+= 2*elapsedSec;
               m_LPlayers.Add(new Player(m_Player.GetRect()));
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
