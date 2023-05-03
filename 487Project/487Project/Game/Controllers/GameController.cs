using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using mainProgram;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;

namespace MainProgram
{
    internal class GameController
    {
        // User related variables
        InputInterceptor inputInterceptor;
        UserEntity user;
        UserMovement userMovements;

        //UI related Variables
        Texture2D background;
        Texture2D uiForeground;
        Button easyModeButton;
        SpriteFont testFont;
        SideBar sideBar;

        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();
        List<Bullets> bullets = new List<Bullets>();
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
            };

        public GameController(Game1 game) 
        {
            this.game = game;
        }


        public void LoadContent(ContentManager Content, GraphicsDeviceManager _graphics)
        {
            string json = File.ReadAllText("../../../TeamBlaster.json");
            levelConfig = JsonSerializer.Deserialize<LevelConfig>(json);

            // Generate user sprite
            userMovements = new UserMovement(new Vector2(levelConfig.player.position.x, levelConfig.player.position.y), 
                levelConfig.player.player_speed);
            //user = new UserEntity(Content.Load<Texture2D>(levelConfig.player.player_sprite), _graphics, userMovements, levelConfig.player.maxHealth);
            user = new UserEntity(
                Content.Load<Texture2D>(levelConfig.player.player_sprite), 
                new UserControlledBehavior(userMovements), 
                levelConfig.player.maxHealth);
            inputInterceptor = new InputInterceptor("Keyboard", keyBindings, levelConfig.player, userMovements, Content.Load<Texture2D>("ball"));
            EntityManager.RegisterUser(user);

            // Generate UI
            background = Content.Load<Texture2D>(levelConfig.phases[0].background);
            uiForeground = Content.Load<Texture2D>("ui-foreground");
            testFont = Content.Load<SpriteFont>("Fonts/test");
            sideBar = new SideBar(testFont, user, 10000);

            easyModeButton = new Button(Content.Load<Texture2D>("godbutton"), Content.Load<SpriteFont>("Fonts/test"))
            {
                Position = new Vector2(1100, 500),
                Text = "Easy Mode",
            };
            easyModeButton.Click += toggleEasyMode;


            this.levelDirector = new LevelDirector(levelConfig.phases, Content);

        }

        internal void toggleEasyMode(object sender, EventArgs e)
        {
            user.cheatMode = !user.cheatMode;
        }

        public void update(GameTime gameTime)
        {
            inputInterceptor.Update(gameTime);
            EntityManager.Update(gameTime);
            sideBar.update();
            easyModeButton.Update(gameTime);
            this.levelDirector.Update(gameTime);

            //Exit code
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(uiForeground, new Rectangle(0, 0, uiForeground.Width, uiForeground.Height), Color.White);
            sideBar.draw(spriteBatch);
            easyModeButton.Draw(gameTime, spriteBatch);
            inputInterceptor.Draw(spriteBatch);
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
        }
    }
}
