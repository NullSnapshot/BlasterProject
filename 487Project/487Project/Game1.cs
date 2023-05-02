using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using mainProgram;

namespace MainProgram
{
    public class Game1 : Game
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

        List<Enemies> enemies = new List<Enemies>();
        List<EnemiesTwo> enemiesTwo = new List<EnemiesTwo>();
        Random random = new Random();

        MidBoss midBoss;

        FinalBoss finalBoss;

        List<Bullets> bullets = new List<Bullets>();
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
            // TODO: use this.Content to load your game content here
            // user.ballTexture = Content.Load<Texture2D>("ball");
            //healthReward = new Reward(Content.Load<Texture2D>("health"), new Vector2(800, 500), new TimeSpan(0, 0, 10), new TimeSpan(0,0,15), user);
            midBoss = new MidBoss(Content.Load<Texture2D>("ball"), new Vector2(800, 300), 150, Content.Load<Texture2D>("bullet"));
            finalBoss = new FinalBoss(Content.Load<Texture2D>("ball"), new Vector2(800, 600), 100, 100, Content.Load<Texture2D>("bullet"));
            
  

        }

        float spawn = 0;
        float spawnTwo = 0;

        float vanish = 0;

        protected override void Update(GameTime gameTime)
        {
            gameController.update(gameTime);
            EntityManager.Update(gameTime);
            //healthReward.Update(gameTime);   

            base.Update(gameTime);
           
        }

        public void Shoot()
        {
            Bullets newBullet = new Bullets(Content.Load<Texture2D>("bullet"));
            newBullet.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f + spriteVelocity;
            newBullet.position = spritePosition + newBullet.velocity * 5;
            newBullet.isVisible = true;

            if(bullets.Count < 40)
            {
                bullets.Add(newBullet);
            }
        }   

        public void LoadEnemies()
        {
            int randX = random.Next(100, 1000);
            if (vanish < 90) 
            {
                if (vanish < 35 || vanish >= 60)
                {
                    if (spawn >= 1)
                    {
                        spawn = 0;
                        if(enemies.Count < 5)
                        {
                            enemies.Add(new Enemies(Content.Load<Texture2D>("ball"), new Vector2(randX, 105), Content.Load<Texture2D>("bullet")));
                        }
                    }
                }
            }

            for(int i = 0;  i < enemies.Count; i++)
            {
                if(!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }
        public void LoadEnemiesTwo()
        {
            int randY = random.Next(105, 600);
            if (vanish < 90) 
            {
                if (vanish < 35 || vanish >= 60)
                {
                    if (spawnTwo >= 1)
                    {
                        spawnTwo = 0;
                        if(enemiesTwo.Count < 5)
                        {
                            enemiesTwo.Add(new EnemiesTwo(Content.Load<Texture2D>("ball"), new Vector2(110, randY), Content.Load<Texture2D>("bullet")));
                        }
                    }
                }
            }

            for(int i = 0;  i < enemiesTwo.Count; i++)
            {
                if(!enemiesTwo[i].isVisible)
                {
                    enemiesTwo.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            gameController.draw(gameTime, _spriteBatch);
            EntityManager.Draw(gameTime, _spriteBatch);

            //healthReward.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}