using Microsoft.Xna.Framework;
using MyClass.MyUtils;
using MyClass.MyMap;
using System.Collections.Generic;
using MyCreature;
using MyMenu;
//DIT PROJECT IS GEMAAT DOOR: BART BRITS : 2ITSOF3 : s142840 :student van AP-Hogeschool
namespace MyGame
{
    internal class Main
    {
        MapHandeler m_MapHandeler;
        CreatureManger m_CreatureManger;
        PointF m_CampPoint;
        MainMenu m_MainMenu;
        MyTimer m_Timer = new MyTimer(2.5f);
        bool m_TimerScreen;
        public void Initialize()
        {
            m_MainMenu = new MainMenu(UtilsStatic.ScreenSize);
            m_MapHandeler = new MapHandeler();
            m_CreatureManger = new CreatureManger();
        }
        public void Draw()
        {
            m_MainMenu.Draw();
            if (m_MainMenu.GetVisibleState()) return;
            UtilsStatic.NewPush();
            UtilsStatic.PushTranslate(-m_CampPoint.X + UtilsStatic.ScreenSize.Width/2, -m_CampPoint.Y + UtilsStatic.ScreenSize.Height / 2);
            m_MapHandeler.DrawMap();
            m_CreatureManger.Draw();
            UtilsStatic.ResetColor();
            UtilsStatic.PopMatrix();
        }
        public void Update(float elapsedSec)
        {
            //KEYBINDS + UI
            m_Timer.IsRun(elapsedSec);
            if (m_MainMenu.GetVisibleState())
            {
                int indexMap = m_MainMenu.Update();
                if (indexMap < 0) return;
                Map map = new Map((MapTypes)indexMap);
                m_CreatureManger = new CreatureManger();
                m_MapHandeler = new MapHandeler();
                m_MapHandeler.SetMap(map.IntedMap, map.GridLayout);
                m_CreatureManger.IntelizeCreatures(m_MapHandeler.GetCreatures());
               
                m_MainMenu.SetVisibiltyMenu(false);
            }
            //you could add a screen when m_TimerScreen is off or on and use won and lose vars to shor right thing
            if((m_CreatureManger.LostGame() || m_CreatureManger.WonGame()) && !m_Timer.IsRun() && !m_TimerScreen)
            {
                m_Timer.Reset();
                m_TimerScreen = true;
            }
            if ((m_CreatureManger.WonGame() && !m_Timer.IsRun()) || (m_CreatureManger.LostGame() && !m_Timer.IsRun()))
            {
                m_MainMenu.SetVisibiltyMenu(true);
                m_TimerScreen = false;
            }
            //GAME
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
    }
}
