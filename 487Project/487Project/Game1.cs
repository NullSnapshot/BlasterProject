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

        Vector2 StartingPosition = new Vector2(500, 1200);
        float StartingSpeed = 100f;

        InputInterceptor inputInterceptor;

        UserSprite user;
        Texture2D background;
        UserMovement userMovements;

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

        SpriteFont testFont;
        SideBar sideBar;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 1400;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            // TODO: use this.Content to load your game content here
            // user.ballTexture = Content.Load<Texture2D>("ball");
            userMovements = new UserMovement(StartingPosition, StartingSpeed);
            user = new UserSprite(Content.Load<Texture2D>("Coug"), _graphics, userMovements, 10);
            background = Content.Load<Texture2D>("tempoverlay");
            testFont = Content.Load<SpriteFont>("Fonts/test");
            sideBar = new SideBar(testFont, user, 10000);
            inputInterceptor = new InputInterceptor("Keyboard", keyBindings, userMovements, Content.Load<Texture2D>("ball"));
            midBoss = new MidBoss(Content.Load<Texture2D>("ball"), new Vector2(800, 300), 150, Content.Load<Texture2D>("bullet"));
            finalBoss = new FinalBoss(Content.Load<Texture2D>("ball"), new Vector2(800, 600), 100, 100, Content.Load<Texture2D>("bullet"));

        }

        float spawn = 0;
        float spawnTwo = 0;

        float vanish = 0;

        protected override void Update(GameTime gameTime)
        {
            inputInterceptor.Update(gameTime);
            user.Update();
            sideBar.update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTwo += (float)gameTime.ElapsedGameTime.TotalSeconds;
            vanish += (float)gameTime.ElapsedGameTime.TotalSeconds;
            LoadEnemies();
            LoadEnemiesTwo();
            midBoss.Update(gameTime);
            finalBoss.Update(gameTime);
            Bullets.UpdateBullets(bullets);

            foreach (Bullets bullet in bullets)
            {
                // Check for collisions with the user
                if (user.CheckCollision(bullet.BoundingBox))
                {
                    // Collision detected, remove bullet and damage user
                    bullet.isVisible = false;
                    user.TakeDamage(1);
                }

                // Check for collisions with enemies
                foreach (Enemies enemy in enemies)
                {
                    if (bullet.BoundingBox.Intersects(enemy.BoundingBox))
                    {
                        // Collision detected, remove bullet and enemy
                        bullet.isVisible = false;
                        enemy.isVisible = false;
                    }
                }

                // Check for collisions with enemiesTwo
                foreach (EnemiesTwo enemyTwo in enemiesTwo)
                {
                    if (bullet.BoundingBox.Intersects(enemyTwo.BoundingBox))
                    {
                        // Collision detected, remove bullet and enemyTwo
                        bullet.isVisible = false;
                        enemyTwo.isVisible = false;
                    }
                }
                // Check for collisions with midBoss
                if (bullet.BoundingBox.Intersects(midBoss.BoundingBox))
                {
                    // Collision detected, remove bullet and damage midBoss
                    bullet.isVisible = false;
                    midBoss.TakeDamage(1);
                }

                // Check for collisions with finalBoss
                if (bullet.BoundingBox.Intersects(finalBoss.BoundingBox))
                {
                    // Collision detected, remove bullet and damage finalBoss
                    bullet.isVisible = false;
                    finalBoss.TakeDamage(1);
                }
            }
            // Update enemies
            foreach (Enemies enemy in enemies)
            {
                enemy.Update(_graphics.GraphicsDevice, gameTime);

                // Check for collisions with the user
                if (user.CheckCollision(enemy.BoundingBox))
                {
                    // Collision detected, remove enemy and damage user
                    enemy.isVisible = false;
                    user.TakeDamage(1);
                }
            }

            // Remove any enemies that are no longer visible
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            // Update enemiesTwo
            foreach (EnemiesTwo enemyTwo in enemiesTwo)
            {
                enemyTwo.Update(_graphics.GraphicsDevice, gameTime);

                // Check for collisions with the user
                if (user.CheckCollision(enemyTwo.BoundingBox))
                {
                    // Collision detected, remove enemyTwo and damage user
                    enemyTwo.isVisible = false;
                    user.TakeDamage(1);
                }
            }

            // Remove any enemiesTwo that are no longer visible
            for (int i = 0; i < enemiesTwo.Count; i++)
            {
                if (!enemiesTwo[i].isVisible)
                {
                    enemiesTwo.RemoveAt(i);
                    i--;
                }
            }

            // Update midBoss
            midBoss.Update(gameTime);

            // Check for collisions with the user
            if (user.CheckCollision(midBoss.BoundingBox))
            {
                // Collision detected, damage user
                user.TakeDamage(1);
            }

            // Update finalBoss
            finalBoss.Update(gameTime);

            // Check for collisions with the user
            if (user.CheckCollision(finalBoss.BoundingBox))
            {
                // Collision detected, damage user
                user.TakeDamage(1);
            }

            if (user.health <= 0)
            {
                // User has lost
                Exit();
            }
            else if (vanish >= 120 && enemies.Count == 0 && enemiesTwo.Count == 0 && !midBoss.isVisible && !finalBoss.isVisible)
            {
                // User has won
                Exit();
            }

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
            _spriteBatch.Draw(background, new Rectangle(0, 0, 1600, 1400), Color.White);
            sideBar.draw(_spriteBatch);
            user.Draw(_spriteBatch);
            inputInterceptor.Draw(_spriteBatch);
            foreach(Enemies enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            foreach(EnemiesTwo enemy in enemiesTwo)
            {
                enemy.Draw(_spriteBatch);
            }
            if (vanish >= 35 && vanish < 60)
            {
                midBoss.Draw(_spriteBatch);
            }
            if (vanish >= 90 && vanish < 110)
            {
                finalBoss.Draw(_spriteBatch);
            }
            foreach(Bullets bullet in bullets)
            {
                bullet.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }
    }
}