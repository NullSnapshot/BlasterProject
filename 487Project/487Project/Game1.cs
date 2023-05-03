using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using BulletBlaster.Game.Controllers;


namespace BulletBlaster
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Dictionary<Keys, string> keyBindings = new Dictionary<Keys, string>()
            {
                { Keys.A, "Left" },
                { Keys.W, "Up" },
                { Keys.D, "Right" },
                { Keys.S, "Down" },
                { Keys.Left, "Left" },
                { Keys.Up, "Up" },
                { Keys.Right, "Right" },
                { Keys.Down, "Down" },
                { Keys.Space, "Space"},
            };

        GameController gameController;
        Reward healthReward;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Vector2 spritePosition;
        public float rotation;
        public Vector2 spriteVelocity;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 1400;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.gameController = new GameController(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //user.initilize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            gameController.LoadContent(Content, _graphics);
            //healthReward = new Reward(Content.Load<Texture2D>("health"), new Vector2(800, 500), new TimeSpan(0, 0, 10), new TimeSpan(0,0,15), user);
            
  

        }

        protected override void Update(GameTime gameTime)
        {
            gameController.update(gameTime);
            EntityManager.Update(gameTime);
            //healthReward.Update(gameTime);   

            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            gameController.DrawBackground(_spriteBatch);
            EntityManager.Draw(gameTime, _spriteBatch);
            gameController.Draw(gameTime, _spriteBatch);
            
            

            //healthReward.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}