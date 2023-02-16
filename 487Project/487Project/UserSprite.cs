using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MainProgram
{
    public class UserSprite
    {
        public Texture2D ballTexture { get; set; }
        public Vector2 ballPosition { get; set; }
        public float ballSpeed { get; set; }

        public UserSprite(Texture2D ballTexture, GraphicsDeviceManager _graphics)
        {
            this.ballTexture = ballTexture;
            this.ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            this.ballSpeed = 100f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                this.ballTexture,
                this.ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(this.ballTexture.Width / 2, this.ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public void move(KeyboardState kstate, GameTime gameTime, GraphicsDeviceManager _graphics)
        {

            Vector2 tempV = this.ballPosition;

            if (kstate.IsKeyDown(Keys.Up))
            {
                tempV.Y -= this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.ballPosition = tempV;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                tempV.Y += this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.ballPosition = tempV;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                tempV.X -= this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.ballPosition = tempV;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                tempV.X += this.ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.ballPosition = tempV;
            }

            if (this.ballPosition.X > _graphics.PreferredBackBufferWidth - this.ballTexture.Width / 2)
            {
                tempV.X = _graphics.PreferredBackBufferWidth - this.ballTexture.Width / 2;
                this.ballPosition = tempV;
            }
            else if (this.ballPosition.X < this.ballTexture.Width / 2)
            {
                tempV.X = this.ballTexture.Width / 2;
                this.ballPosition = tempV;
            }

            if (this.ballPosition.Y > _graphics.PreferredBackBufferHeight - this.ballTexture.Height / 2)
            {
                tempV.Y = _graphics.PreferredBackBufferHeight - this.ballTexture.Height / 2;
                this.ballPosition = tempV;
            }
            else if (this.ballPosition.Y < this.ballTexture.Height / 2)
            {
                tempV.Y = this.ballTexture.Height / 2;
                this.ballPosition = tempV;
            }
        }

    }
}
