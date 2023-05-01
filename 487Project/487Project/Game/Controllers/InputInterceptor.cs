using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class InputInterceptor
    {
        PlayerConfig config;
        string inputType;
        UserMovement userPosition;
        public Texture2D bulletTexture;
        List<Bullets> bullets = new List<Bullets>();
        public InputInterceptor(String inputType, Dictionary<Keys, string> config, PlayerConfig playerConfig, UserMovement userPosition, Texture2D bullet) 
        {
            this.config = playerConfig;
            this.config.BindKeys(config);
            this.inputType = inputType;
            this.userPosition = userPosition;
            this.bulletTexture = bullet;
        }



        public void Update(GameTime gameTime)
        {
            if(this.inputType == "Keyboard")
            {
                updateKeyboard(gameTime);
            }

            if(this.inputType == "Mouse")
            {
                updateMouse(gameTime);
            }
        }

        private void updateKeyboard(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            List<string> movement = config.lookUpKeys(kstate.GetPressedKeys());

            Vector2 tempV = userPosition.getLocation();
            float tempSpeed = userPosition.getSpeed();
            Texture2D texture = userPosition.getTexture();

            foreach (string direction in movement)
            {
                System.Diagnostics.Debug.WriteLine(direction);
                if (direction == "Up")
                {
                    tempV.Y -= tempSpeed * 6 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    userPosition.updateLocation(tempV);
                }

                if (direction == "Down")
                {
                    tempV.Y += tempSpeed * 6 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    userPosition.updateLocation(tempV);
                }

                if (direction == "Left")
                {
                    tempV.X -= tempSpeed * 6 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    userPosition.updateLocation(tempV);
                }

                if (direction == "Right")
                {
                    tempV.X += tempSpeed * 6 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    userPosition.updateLocation(tempV);
                }

                if (direction == "Space")
                {
                    Vector2 velocity = new Vector2(0, -6);
                    Vector2 startPosition = new Vector2(userPosition.getLocation().X + (texture.Height / 2) - (bulletTexture.Height / 2), userPosition.getLocation().Y + velocity.Y);
                    Bullets bullet = new Bullets(bulletTexture);
                    this.bullets.Add(bullet);
                    Bullets.ShootBullets(bullets, bulletTexture, startPosition, velocity + new Vector2(0, -6f), 3);
                }

                if (tempV.X > 1013 - texture.Width / 2)
                {
                    tempV.X = 1013 - texture.Width / 2;
                    userPosition.updateLocation(tempV);
                }
                else if (tempV.X < 89 + texture.Width / 2)
                {
                    tempV.X = 89 + texture.Width / 2;
                    userPosition.updateLocation(tempV);
                }


                if (tempV.Y > 1266 - texture.Height / 2)
                {
                    tempV.Y = 1266 - texture.Height / 2;
                    userPosition.updateLocation(tempV);
                }
                else if (tempV.Y < 104 + texture.Height / 2)
                {
                    tempV.Y = 104 + texture.Height / 2;
                    userPosition.updateLocation(tempV);
                }
            }
            Bullets.UpdateBullets(bullets);
        }

        // NEEDS WORK: Mouse is allowed to go outside of boundaries which is something we don't want
        private void updateMouse(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            Vector2 tempV = new Vector2(state.X, state.Y);
            Texture2D texture = userPosition.getTexture();

            // left side
            if (tempV.X <= 122)
            {
                tempV.X = 122;
            }
            // right side
            if (tempV.X + texture.Width > 1050)
            {
                tempV.X = 1050 - texture.Width;
            }
            // bottom screen
            if (tempV.Y > 1230)
            {
                tempV.Y = 1230;
            }
            // top screen
            if (tempV.Y + texture.Height < 210)
            {
                tempV.Y = 210 - texture.Height;
            }
            userPosition.updateLocation(tempV);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
