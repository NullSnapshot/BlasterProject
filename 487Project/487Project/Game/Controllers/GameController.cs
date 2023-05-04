using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BulletBlaster.Code.Entities.Behaviors.Mob;
using BulletBlaster.Game.config;
using BulletBlaster.Game.Controllers.WaveManagement;
using BulletBlaster.Game.Entities.User;
using BulletBlaster.Game.Entities.Behaviors.Bullet;
using BulletBlaster.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BulletBlaster.Game.Entities.Bullet.Patterns;
using BulletBlaster.Game.Entities;

namespace BulletBlaster.Game.Controllers
{
    internal class GameController
    {
        // User related variables
        InputInterceptor inputInterceptor;
        UserEntity User;
        UserControlledBehavior UserIntents;

        //UI related Variables
        Texture2D background;
        Texture2D uiForeground;
        Button easyModeButton;
        SpriteFont testFont;
        SideBar sideBar;
        LevelConfig levelConfig;

        Game1 game;

        // Level Director
        LevelDirector levelDirector;

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
                { Keys.LeftShift, "Slowmode" },
                { Keys.F1, "Debug" }
            };

        public GameController(Game1 game)
        {
            this.game = game;
        }


        public void LoadContent(ContentManager Content, GraphicsDeviceManager _graphics)
        {
            Config.LoadConfig();
            Config.SaveConfig();
            string json = File.ReadAllText("../../../" + Config.LevelName +".json");
            levelConfig = JsonSerializer.Deserialize<LevelConfig>(json);
            

            // Generate user
            this.UserIntents = new UserControlledBehavior(levelConfig.player.player_speed, 
                new Vector2(levelConfig.player.position.x, levelConfig.player.position.y));

            PlayerBulletPattern playerPattern = new PlayerBulletPattern(levelConfig.player.attackPatterns[0],
                Content.Load<Texture2D>(levelConfig.player.attackPatterns[0].bullet_sprite));

            User = new UserEntity(
                Content.Load<Texture2D>(levelConfig.player.player_sprite),
                Content.Load<Texture2D>("slowmode-pip"),
                this.UserIntents,
                new List<BulletPattern>(){ playerPattern },
                levelConfig.player.maxHealth);

            EntityManager.RegisterUser(User);
            inputInterceptor = new InputInterceptor("Keyboard", keyBindings, levelConfig.player, this.UserIntents);
            

            // Generate UI
            background = Content.Load<Texture2D>(levelConfig.phases[0].background);
            uiForeground = Content.Load<Texture2D>("ui-foreground");
            testFont = Content.Load<SpriteFont>("Fonts/test");
            sideBar = new SideBar(testFont, User, 10000);

            easyModeButton = new Button(Content.Load<Texture2D>("godbutton"), Content.Load<SpriteFont>("Fonts/test"))
            {
                Position = new Vector2(1100, 500),
                Text = "Easy Mode",
            };
            easyModeButton.Click += toggleEasyMode;


            levelDirector = new LevelDirector(levelConfig.phases, Content);

        }

        internal void toggleEasyMode(object sender, EventArgs e)
        {
            User.cheatMode = !User.cheatMode;
        }

        public void update(GameTime gameTime)
        {
            inputInterceptor.Update(gameTime);
            EntityManager.Update(gameTime);
            sideBar.update();
            easyModeButton.Update(gameTime);
            levelDirector.Update(gameTime);

            //Exit code
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            if(Math.Floor((double)gameTime.TotalGameTime.Seconds) % 2 == 0)
            {
                ScoreSystem.addPoints(1);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(uiForeground, new Rectangle(0, 0, uiForeground.Width, uiForeground.Height), Color.White);
            sideBar.draw(spriteBatch);
            easyModeButton.Draw(gameTime, spriteBatch);
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
        }
    }
}
