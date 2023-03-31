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

        UserMovement usersMovements;

        int health = 10;
        bool alive = false;


        public UserSprite(Texture2D ballTexture, GraphicsDeviceManager _graphics, UserMovement movement)
        {
            this.ballTexture = ballTexture;
            //this.ballPosition = new Vector2(500, 1200);
            //this.ballSpeed = 100f;
            this.usersMovements = movement;
            movement.setTexture(this.ballTexture);
            this.ballPosition = movement.getLocation();
            this.ballSpeed = movement.getSpeed();
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

        //public void Update(KeyboardState kstate, GameTime gameTime)
        public void Update()
        {
            this.ballPosition = this.usersMovements.getLocation();
            this.ballTexture = this.usersMovements.getTexture();
            this.ballSpeed = this.usersMovements.getSpeed();
        }

        public void TakeDamage(int amount)
        {
            this.health -= amount;
            if(this.health <= 0)
            {
                this.alive = true;
            }
        }
    }
}
