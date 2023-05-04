using BulletBlaster.Code.Entities.Behaviors.Mob;
using BulletBlaster.Game.config;
using BulletBlaster.Game.Entities;
using BulletBlaster.Game.Entities.User;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace BulletBlaster.Game.Controllers
{
    internal class InputInterceptor
    {
        private PlayerConfig config;
        private string inputType;
        private UserEntity User;
        private UserControlledBehavior UserIntent;
        private double LastDebugKeyPress = 0;

        public InputInterceptor(string inputType, Dictionary<Keys, string> config, PlayerConfig playerConfig, UserControlledBehavior userBehavior)
        {
            this.config = playerConfig;
            this.config.BindKeys(config);
            this.inputType = inputType;
            this.User = EntityManager.GetPlayer();
            this.UserIntent = userBehavior;
        }

        public void Update(GameTime gameTime)
        {
            if (this.inputType == "Keyboard")
            {
                updateKeyboard(gameTime);
            }

            if (this.inputType == "Mouse")
            {
                updateMouse(gameTime);
            }
        }

        private void updateKeyboard(GameTime gameTime)
        {

            var kstate = Keyboard.GetState();

            List<string> movement = config.lookUpKeys(kstate.GetPressedKeys());

            // Don't run user inputs if the user hasn't loaded yet.
            if (this.User == null)
                return;
            Vector2 tempV = this.User.Position;
            int speed = (int)this.User.MaxSpeed;
            if(movement.Contains("Slowmode"))
            {
                speed /= 2;
                this.User.drawHitbox = true;
            }
            else
            {
                this.User.drawHitbox = false;
            }
            if(movement.Contains("Debug"))
            {
                if(gameTime.TotalGameTime.Seconds - this.LastDebugKeyPress > 1)
                {
                    Config.DebugMode = !Config.DebugMode;
                    this.LastDebugKeyPress = gameTime.TotalGameTime.Seconds;
                }
                    
            }
            foreach (string direction in movement)
            {
                //System.Diagnostics.Debug.WriteLine(direction);
                if (direction == "Up")
                {
                    tempV = EntityTools.DeltaMove(tempV, gameTime, y:-speed);
                }

                if (direction == "Down")
                {
                    tempV = EntityTools.DeltaMove(tempV, gameTime, y: speed);
                }

                if (direction == "Left")
                {
                    tempV = EntityTools.DeltaMove(tempV, gameTime, x: -speed);
                }

                if (direction == "Right")
                {
                    tempV = EntityTools.DeltaMove(tempV, gameTime, x: speed);
                }

                if (direction == "Space")
                {
                    this.User.Shoot(gameTime);
                }
                
            }
            this.UpdateTargetPosition(tempV);
        }

        // NEEDS WORK: Mouse is allowed to go outside of boundaries which is something we don't want
        private void updateMouse(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            Vector2 tempV = new Vector2(state.X, state.Y);
            this.UpdateTargetPosition(tempV);

            if(state.LeftButton.Equals(true))
            {
                this.User.Shoot(gameTime);
            }

        }

        private void UpdateTargetPosition(Vector2 inputPosition)
        {
            // First make sure the inputted position will be in bounds.
            MobEntity player = EntityManager.GetPlayer();
            Vector2 safePosition = EntityTools.GetNearestInBoundsLocation(inputPosition, player);
            this.UserIntent.SetTargetPosition(safePosition);
        }
    }
}
