using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using MyClass.MyUtils;
using System.IO;
namespace MyGame
{
    public class Game1 : Game
    {
        private Main m_Main;
        GraphicsDeviceManager m_Intelize;
        SpriteBatch m_Graph;
        SoundEffect m_SoundEffect;
        public static bool CloseGame;
        public Game1()
        {
            CloseGame = false;
            m_Intelize = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Window.Title = "TestZone";
            m_Main = new Main();

            UtilsStatic.SetScreen(m_Intelize, m_Graph);
            //conecting screen with drawing functions
            UtilsStatic.SetScreenSize(1800, 900);
            m_Main.Initialize();
        }

        protected override void LoadContent()
        {
            m_Graph = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || CloseGame || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
            //My Update
            float elapsedSec = gameTime.ElapsedGameTime.Milliseconds;
            elapsedSec /= 1000;
            m_Main.Update(elapsedSec);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //MY Draw MAIN
            m_Main.Draw();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}