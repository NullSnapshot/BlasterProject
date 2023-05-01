using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using System.Net;

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
        public Rectangle boundingBox { get; set; }

        public int immunityLength = 0;

        public bool cheatMode = false;

        private List<Rectangle> hitList = new List<Rectangle>();


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
            this.boundingBox = new Rectangle((int)this.ballPosition.X - ballTexture.Width / 2, (int)this.ballPosition.Y - ballTexture.Height / 2, ballTexture.Width, ballTexture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            if(immunityLength == 0)
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
            else
            {
                sb.Draw(
                this.ballTexture,
                this.ballPosition,
                null,
                Color.Gray,
                0f,
                new Vector2(this.ballTexture.Width / 2, this.ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }

        public void Update()
        {
            if(immunityLength != 0)
            {
                immunityLength--;
            }
            this.score += 1;
            this.ballPosition = this.usersMovements.getLocation();
            this.ballTexture = this.usersMovements.getTexture();
            this.ballSpeed = this.usersMovements.getSpeed();
            this.boundingBox = new Rectangle((int)this.ballPosition.X - ballTexture.Width / 2, (int)this.ballPosition.Y - ballTexture.Height / 2, ballTexture.Width, ballTexture.Height);
        }

        public bool CheckCollision(Rectangle otherBoundingBox)
        {
            if(immunityLength == 0 || cheatMode == true)
            {
                if (hitList.Contains(otherBoundingBox))
                {
                    return false;
                }
                else
                {
                    if (this.boundingBox.Intersects(otherBoundingBox))
                    {
                        hitList.Add(otherBoundingBox);
                        return true;
                    }

                    return false;
                }
            }

            return false;

        }

        public void TakeDamage(int amount)
        {
            if(cheatMode != true)
            {
                this.health -= amount;
                System.Diagnostics.Debug.WriteLine("New Health" + this.health.ToString());
                if (this.health <= 0)
                {
                    this.alive = false;
                    Dead();
                }
                else
                {
                    Vector2 StartingPosition = new Vector2(500, 1200);
                    immunityLength = 200;
                    this.usersMovements.updateLocation(StartingPosition);
                }
            }
        }

        private void Dead()
        {
            this.isVisible= false;
        }
    }
}
