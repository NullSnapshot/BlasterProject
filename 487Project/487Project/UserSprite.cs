using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;

namespace MainProgram
{
    public class UserSprite
    {
        public Texture2D ballTexture { get; set; }
        public Vector2 ballPosition { get; set; }
        public float ballSpeed { get; set; }

        UserMovement usersMovements;

        public bool isVisible = true;

        public int health { get; set;}
        public bool alive { get; set; }
        public int score { get; set; }


        public UserSprite(Texture2D ballTexture, GraphicsDeviceManager _graphics, UserMovement movement, int health)
        {
            this.ballTexture = ballTexture;
            this.usersMovements = movement;
            movement.setTexture(this.ballTexture);
            this.ballPosition = movement.getLocation();
            this.ballSpeed = movement.getSpeed();
            this.health = health;
            this.alive = true;
            this.score = 0;
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

        public void Update()
        {
            this.score += 1;
            this.ballPosition = this.usersMovements.getLocation();
            this.ballTexture = this.usersMovements.getTexture();
            this.ballSpeed = this.usersMovements.getSpeed();
        }

        public void TakeDamage(int amount)
        {
            this.health -= amount;
            if(this.health <= 0)
            {
                this.alive = false;
                Dead();
            }
        }

        private void Dead()
        {
            this.isVisible= false;
        }
    }
}
