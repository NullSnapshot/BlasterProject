using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    internal class InputInterceptor
    {
        PlayerConfig config;
        string inputType;
        UserMovement userPosition;
        public InputInterceptor(String inputType, Dictionary<Keys, string> config, UserMovement userPosition) 
        {
            this.config = new PlayerConfig(config);
            this.inputType = inputType;
            this.userPosition = userPosition;
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
        }

        // NEEDS WORK: Mouse is allowed to go outside of boundaries which is something we don't want
        private void updateMouse(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();

            Vector2 tempV = new Vector2(state.X, state.Y);
            Texture2D texture = userPosition.getTexture();
            userPosition.updateLocation(tempV);
        }
    }
}
